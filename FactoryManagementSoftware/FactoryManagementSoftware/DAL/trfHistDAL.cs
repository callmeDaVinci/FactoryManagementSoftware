using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;

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

        public string Balance { get; } = "balance";

        #endregion

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        //Tool tool = new Tool();
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
                String sql = @"SELECT * FROM tbl_trf_hist";

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable Select()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code";

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable SelectWithBalance()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result, balance
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code";

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable Select(string trfID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code WHERE tbl_trf_hist.trf_hist_id = @trfID";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@trfID", trfID);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
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

        public int GetLastInsertedID()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            int ID = -1;
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT MAX(trf_hist_id) FROM tbl_trf_hist ";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);

                ID = Convert.ToInt32(dt.Rows[0][0] == DBNull.Value ? -1 : dt.Rows[0][0]);

            }
            catch (Exception ex)
            {
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

           

            return ID;
        }
        #endregion

        #region Insert Data in Database

        public bool Insert(trfHistBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            

            try
            {
                String sql = @"INSERT INTO tbl_trf_hist 
                            (trf_hist_item_code, 
                            trf_hist_from, 
                            trf_hist_to, 
                            trf_hist_qty, 
                            trf_hist_unit, 
                            trf_hist_trf_date, 
                            trf_hist_note, 
                            trf_hist_added_date, 
                            trf_hist_added_by, 
                            trf_result, 
                            trf_hist_from_order,
                            balance) 
                            VALUES 
                            (@trf_hist_item_code, 
                            @trf_hist_from, 
                            @trf_hist_to, 
                            @trf_hist_qty, 
                            @trf_hist_unit, 
                            @trf_hist_trf_date, 
                            @trf_hist_note, 
                            @trf_hist_added_date, 
                            @trf_hist_added_by, 
                            @trf_result, 
                            @trf_hist_from_order,
                            @balance)";

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
                cmd.Parameters.AddWithValue("@balance", u.balance);
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public int InsertAndGetPrimaryKey(trfHistBLL u)
        {
            int ID = -1;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(myconnstrng);


            try
            {
                String sql = @"INSERT INTO tbl_trf_hist 
                            (trf_hist_item_code, 
                            trf_hist_from, 
                            trf_hist_to, 
                            trf_hist_qty, 
                            trf_hist_unit, 
                            trf_hist_trf_date, 
                            trf_hist_note, 
                            trf_hist_added_date, 
                            trf_hist_added_by, 
                            trf_result, 
                            trf_hist_from_order,balance) 
                            VALUES 
                            (@trf_hist_item_code, 
                            @trf_hist_from, 
                            @trf_hist_to, 
                            @trf_hist_qty, 
                            @trf_hist_unit, 
                            @trf_hist_trf_date, 
                            @trf_hist_note, 
                            @trf_hist_added_date, 
                            @trf_hist_added_by, 
                            @trf_result, 
                            @trf_hist_from_order,@balance)
                            SELECT SCOPE_IDENTITY();";

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
                cmd.Parameters.AddWithValue("@balance", u.balance);

                conn.Open();

                //int rows = cmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                ID = Convert.ToInt32(dt.Rows[0][0] == DBNull.Value ? -1 : dt.Rows[0][0]);

                int rows = dt.Rows.Count;


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
            return ID;
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
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool NoteUpdate(trfHistBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_trf_hist SET trf_hist_note=@trf_hist_note, trf_hist_updated_date=@trf_hist_updated_date, trf_hist_updated_by=@trf_hist_updated_by WHERE trf_hist_id=@trf_hist_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@trf_hist_id", u.trf_hist_id);
                cmd.Parameters.AddWithValue("@trf_hist_note", u.trf_hist_note);
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
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool DeliveredDateUpdate(trfHistBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_trf_hist SET trf_hist_trf_date=@trf_hist_trf_date, trf_hist_updated_date=@trf_hist_updated_date, trf_hist_updated_by=@trf_hist_updated_by WHERE trf_hist_id=@trf_hist_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@trf_hist_id", u.trf_hist_id);
                cmd.Parameters.AddWithValue("@trf_hist_trf_date", u.trf_hist_trf_date);
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
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result
                                FROM tbl_trf_hist  
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_code LIKE '%" + keywords + "%'" +
                                "OR tbl_item.item_name LIKE '%" + keywords + "%' " +
                                "ORDER BY tbl_trf_hist.trf_hist_id DESC";

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

        public DataTable SearchByID(string trfID)
        {
            string Passed = "Passed";
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            WHERE trf_hist_id = @trfID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@trfID", trfID);
;
                //AND tbl_trf_hist.trf_result = @Passed
                //for executing command
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);

                itemDAL dalItem = new itemDAL();
                dt.AcceptChanges();
                //foreach (DataRow row in dt.Rows)
                //{
                //    string itemCode = row["trf_hist_item_code"].ToString();

                //    if (!dalItem.getCatName(itemCode).Equals("Part"))
                //    {
                //        row.Delete();
                //    }
                //}
                //dt.AcceptChanges();
            }
            catch (Exception ex)
            {
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

        public DataTable SearchIncludeID(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_item.item_cat,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result, tbl_trf_hist.balance
                                FROM tbl_trf_hist  
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_code LIKE '%" + keywords + "%'" +
                                "OR tbl_item.item_name LIKE '%" + keywords + "%' " +
                                "OR tbl_trf_hist.trf_hist_id LIKE '%" + keywords + "%' " +
                                 "OR tbl_trf_hist.trf_hist_note LIKE '%" + keywords + "%' " +
                                "ORDER BY tbl_trf_hist.trf_hist_id DESC";

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

        public DataTable codeLikeSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result, tbl_trf_hist.balance
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_code LIKE '%" + keywords + "%' " +
                                "ORDER BY tbl_trf_hist.trf_hist_trf_date DESC";

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

        public DataTable codeSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result, tbl_trf_hist.balance
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_code  = @keywords 
                                ORDER BY tbl_trf_hist.trf_hist_trf_date DESC";

     

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@keywords", keywords);

                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
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

        public DataTable keywordRangeSearch(string keywords, int fromPast)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE (tbl_item.item_code LIKE '%" + keywords + "%' " +
                                "OR tbl_item.item_name LIKE '%" + keywords + "%' )" +
                                " AND tbl_trf_hist.trf_hist_added_date >= DATEADD(day, @fromPast, GetDate()) " +
                                "ORDER BY tbl_trf_hist.trf_hist_id DESC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@fromPast", fromPast * -1);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
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

        public DataTable codeRangeSearchOrderByTrfDate(string keywords, int fromPast)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result,tbl_trf_hist.balance
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_code =@keywords" +
                                " AND tbl_trf_hist.trf_hist_trf_date >= DATEADD(day, @fromPast, GetDate()) " +
                                "ORDER BY tbl_trf_hist.trf_hist_trf_date DESC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@keywords", keywords);
                cmd.Parameters.AddWithValue("@fromPast", fromPast * -1);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
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

        public DataTable codeRangeSearch(string keywords, int fromPast)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result,tbl_trf_hist.balance
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_code =@keywords" +
                                " AND tbl_trf_hist.trf_hist_added_date >= DATEADD(day, @fromPast, GetDate()) " +
                                "ORDER BY tbl_trf_hist.trf_hist_id DESC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@keywords", keywords);
                cmd.Parameters.AddWithValue("@fromPast", fromPast * -1);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
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

        public DataTable SBBPageRangeUsageSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            string Production = "Production";
            string Assembly = "Assembly";

            try
            {
                //sql query to get data from database
                String sql = @"SELECT  
                            tbl_trf_hist.trf_result,
                            tbl_trf_hist.trf_hist_item_code,
                            tbl_trf_hist.trf_hist_qty,
                            tbl_trf_hist.trf_hist_from,
                            tbl_trf_hist.trf_hist_to
                            FROM tbl_trf_hist 
                            WHERE trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND ((trf_hist_from=@Production OR trf_hist_to=@Production) OR
                            trf_hist_to=@Assembly)
                            ORDER BY trf_hist_item_code ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@Production", Production);
                cmd.Parameters.AddWithValue("@Assembly", Assembly);

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

        public DataTable rangeAllItemProductionSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            string Production = "Production";
            string Assembly = "Assembly";

            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_fac
                            ON tbl_trf_hist.trf_hist_to=tbl_fac.fac_name
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND (tbl_trf_hist.trf_hist_from=@Production OR tbl_trf_hist.trf_hist_from=@Assembly)
                            ORDER BY tbl_trf_hist.trf_hist_item_code ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@Production", Production);
                cmd.Parameters.AddWithValue("@Assembly", Assembly);

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

        public DataTable rangeItemProductionSearch(string start, string end, string customer)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            string Production = "Production";
            string Assembly = "Assembly";

            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_fac
                            ON tbl_trf_hist.trf_hist_to=tbl_fac.fac_name
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND (tbl_trf_hist.trf_hist_from=@Production OR tbl_trf_hist.trf_hist_from=@Assembly)
                            ORDER BY tbl_trf_hist.trf_hist_item_code ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@Production", Production);
                cmd.Parameters.AddWithValue("@Assembly", Assembly);
                
                //for executing command
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);

                itemCustDAL dalItemCust = new itemCustDAL();

                dt.AcceptChanges();
                foreach (DataRow row in dt.Rows)
                {
                    string itemCode = row["trf_hist_item_code"].ToString();

                    if(!dalItemCust.ifItemUnderThisCustomer(itemCode,customer))
                    {
                        row.Delete();
                    }
                }
                dt.AcceptChanges();

            }
            catch (Exception ex)
            {
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

        public DataTable rangeItemToAllCustomerSearch(string start, string end)
        {
            string Passed = "Passed";
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_cust
                            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start AND @end 
                            AND tbl_trf_hist.trf_result =@Passed
                            ORDER BY tbl_cust.cust_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@Passed", Passed);
                //AND tbl_trf_hist.trf_result = @Passed
                //for executing command
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);

                itemDAL dalItem = new itemDAL();
                dt.AcceptChanges();
                foreach (DataRow row in dt.Rows)
                {
                    string itemCode = row["trf_hist_item_code"].ToString();

                    if (!dalItem.getCatName(itemCode).Equals("Part"))
                    {
                        row.Delete();
                    }
                }
                dt.AcceptChanges();
            }
            catch (Exception ex)
            {
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

        public DataTable rangeItemToAllCustomerSearch(string start, string end, string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_cust
                            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND tbl_trf_hist.trf_hist_item_code=@itemCode";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@itemCode", itemCode);

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

        public DataTable rangeToAllCustomerSearchByMonth(string month, string year)
        {
            
            bool isNumeric = int.TryParse(month, out int n);

            if(!isNumeric)
            {
                month = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month.ToString();
            }

            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_cust
                            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                            WHERE MONTH(tbl_trf_hist.trf_hist_trf_date) = @month AND YEAR(tbl_trf_hist.trf_hist_trf_date) = @year";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);

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

        public DataTable rangeItemToAllCustomerSearchByMonth(string month, string year, string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_cust
                            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                            WHERE MONTH(tbl_trf_hist.trf_hist_trf_date) = @month AND YEAR(tbl_trf_hist.trf_hist_trf_date) = @year
                            AND tbl_trf_hist.trf_hist_item_code=@itemCode";

                
                   

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@itemCode", itemCode);

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

        public DataTable rangeItemToCustomerSearch(string customer)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            WHERE tbl_trf_hist.trf_hist_to=@customer  
                            ORDER BY tbl_trf_hist.trf_hist_item_code ASC , tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

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


        public DataTable ItemDeliveredRecordSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_cust
                            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                            WHERE tbl_trf_hist.trf_hist_trf_date
                            BETWEEN @start 
                            AND @end
                            ORDER BY tbl_trf_hist.trf_hist_item_code ASC , tbl_trf_hist.trf_hist_trf_date ASC";


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

        public DataTable ItemDeliveredRecordSearch(string customer , string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            WHERE tbl_trf_hist.trf_hist_to=@customer  AND tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end
                            ORDER BY tbl_trf_hist.trf_hist_item_code ASC , tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@customer", customer);
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

        public DataTable rangeItemToCustomerTransferDataOnlySearch(string customer, string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            WHERE trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND trf_hist_to=@customer  ORDER BY trf_hist_item_code ASC , trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
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

        public DataTable rangeItemToCustomerSearch(string customer, string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            INNER JOIN tbl_cust
                            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND tbl_trf_hist.trf_hist_to=@customer  ORDER BY tbl_trf_hist.trf_hist_item_code ASC , tbl_trf_hist.trf_hist_trf_date ASC";

                //String sql = @"SELECT * FROM tbl_trf_hist 
                //            INNER JOIN tbl_cust
                //            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                //            WHERE tbl_trf_hist.trf_hist_trf_date 
                //            BETWEEN @start AND @end 
                //            AND tbl_trf_hist.trf_result =@Passed
                //            ORDER BY tbl_cust.cust_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
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

        public DataTable rangeItemToCustomerSearch(string customer, string start, string end, string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            WHERE trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND trf_hist_item_code=@itemCode 
                            AND trf_hist_to=@customer";

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

        public DataTable ItemToCustomerDateSearch(string customer, string month, string year, string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            WHERE MONTH(trf_hist_trf_date)=@month 
                            AND YEAR(trf_hist_trf_date)=@year  
                            AND trf_hist_item_code=@itemCode 
                            AND trf_hist_to=@customer";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
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

        public DataTable ItemInSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            string Production = "Production";
            string Assembly = "Assembly";
            string Part = "Part";
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_fac
                            ON tbl_trf_hist.trf_hist_to=tbl_fac.fac_name
                            INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND (tbl_trf_hist.trf_hist_from=@Production OR tbl_trf_hist.trf_hist_from=@Assembly)
                            AND tbl_item.item_cat = @Part
                            ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@Production", Production);
                cmd.Parameters.AddWithValue("@Assembly", Assembly);
                cmd.Parameters.AddWithValue("@Part", Part);
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
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable ItemInSearch(string start, string end, string customer)
        {
            DataTable dt = new DataTable();
            if (customer.ToUpper().Equals("ALL"))
            {
                dt = ItemInSearch(start, end);
            }
            else
            {
                Tool tool = new Tool();
                //static methodd to connect database
                SqlConnection conn = new SqlConnection(myconnstrng);
                //to hold the data from database
                


                string Production = "Production";
                string Assembly = "Assembly";
                string Part = "Part";

                try
                {
                    //sql query to get data from database
                    String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_fac
                            ON tbl_trf_hist.trf_hist_to=tbl_fac.fac_name
                            INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            INNER JOIN tbl_item_cust          
                            ON tbl_item.item_code = tbl_item_cust.item_code
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND (tbl_trf_hist.trf_hist_from=@Production OR tbl_trf_hist.trf_hist_from=@Assembly)
                            AND tbl_item.item_cat = @Part
                            AND tbl_item_cust.cust_id = @customerID
                            ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    cmd.Parameters.AddWithValue("@Production", Production);
                    cmd.Parameters.AddWithValue("@Assembly", Assembly);
                    cmd.Parameters.AddWithValue("@Part", Part);
                    cmd.Parameters.AddWithValue("@customerID", tool.getCustID(customer));

                    //for executing command
                    //getting data from database
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //database connection open
                    conn.Open();
                    //fill data in our database
                    adapter.Fill(dt);

                    //itemCustDAL dalItemCust = new itemCustDAL();

                    //dt.AcceptChanges();
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    string itemCode = row["trf_hist_item_code"].ToString();

                    //    if (!dalItemCust.ifItemUnderThisCustomer(itemCode, customerID))
                    //    {
                    //        row.Delete();
                    //    }
                    //}
                    //dt.AcceptChanges();

                }
                catch (Exception ex)
                {
                    tool = new Tool();
                    tool.saveToText(ex);
                }
                finally
                {
                    //closing connection
                    conn.Close();
                }
            }
            
            return dt;
        }

        public DataTable ItemToAllCustomerSearch(string start, string end)
        {
            string Passed = "Passed";
            string cat = "Part";
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_cust
                            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                            INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start AND @end 
                            AND tbl_trf_hist.trf_result =@Passed AND tbl_item.item_cat = @cat
                            ORDER BY tbl_cust.cust_name ASC, tbl_item.item_name ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@Passed", Passed);
                cmd.Parameters.AddWithValue("@cat", cat);
                //AND tbl_trf_hist.trf_result = @Passed
                //for executing command
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);

                //itemDAL dalItem = new itemDAL();
                //dt.AcceptChanges();
                //foreach (DataRow row in dt.Rows)
                //{
                //    string itemCode = row["trf_hist_item_code"].ToString();

                //    if (!dalItem.getCatName(itemCode).Equals("Part"))
                //    {
                //        row.Delete();
                //    }
                //}
                //dt.AcceptChanges();
            }
            catch (Exception ex)
            {
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

        public DataTable ItemToAllCustomerSearch(string itemCode)
        {
            string Passed = "Passed";
            string cat = "Part";
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_cust
                            ON tbl_trf_hist.trf_hist_to=tbl_cust.cust_name
                            INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            WHERE tbl_trf_hist.trf_result =@Passed AND tbl_item.item_cat = @cat AND tbl_item.item_code =@itemCode
                            ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@Passed", Passed);
                cmd.Parameters.AddWithValue("@cat", cat);
                //AND tbl_trf_hist.trf_result = @Passed
                //for executing command
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);

                //itemDAL dalItem = new itemDAL();
                //dt.AcceptChanges();
                //foreach (DataRow row in dt.Rows)
                //{
                //    string itemCode = row["trf_hist_item_code"].ToString();

                //    if (!dalItem.getCatName(itemCode).Equals("Part"))
                //    {
                //        row.Delete();
                //    }
                //}
                //dt.AcceptChanges();
            }
            catch (Exception ex)
            {
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

        public DataTable ItemToCustomerAllTimeSearch(string customer)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_item
                        ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                        WHERE tbl_trf_hist.trf_hist_to=@customer   
                        ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

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

        public DataTable ItemToCustomerAllTimeSearch(string customer, string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_item
                        ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                        WHERE tbl_trf_hist.trf_hist_to=@customer  AND   tbl_item.item_code =@itemCode
                        ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@customer", customer);
                cmd.Parameters.AddWithValue("@itemCode", itemCode);

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

        public DataTable SPPItemToCustomerSearch(string start, string end, string customer)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                if (customer.ToUpper().Equals("ALL"))
                {
                    dt = ItemToAllCustomerSearch(start, end);
                }
                else
                {
                    //sql query to get data from database
                    String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    cmd.Parameters.AddWithValue("@OTHER", "OTHER");

                    //for executing command
                    //getting data from database
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //database connection open
                    conn.Open();
                    //fill data in our database
                    adapter.Fill(dt);

                }
            }
            catch (Exception ex)
            {
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

        public DataTable ItemToCustomerSearch(string start, string end, string customer)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                if(customer.ToUpper().Equals("ALL"))
                {
                    dt = ItemToAllCustomerSearch(start, end);
                }
                else
                {
                    //sql query to get data from database
                    String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item
                            ON tbl_trf_hist.trf_hist_item_code=tbl_item.item_code
                            WHERE tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start 
                            AND @end 
                            AND tbl_trf_hist.trf_hist_to=@customer ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    cmd.Parameters.AddWithValue("@customer", customer);

                    //for executing command
                    //getting data from database
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //database connection open
                    conn.Open();
                    //fill data in our database
                    adapter.Fill(dt);

                }
            }
            catch (Exception ex)
            {
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

        public DataTable ItemInOutSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND tbl_item.item_cat = @cat 
                                 ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

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

        public DataTable ItemInOutSearch(string start, string end, string customer)
        {
            Tool tool = new Tool();
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                INNER JOIN tbl_item_cust          
                                ON tbl_item.item_code = tbl_item_cust.item_code
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND tbl_item.item_cat = @cat 
                                AND tbl_item_cust.cust_id = @customerID
                                ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@customer", customer);
                cmd.Parameters.AddWithValue("@customerID", tool.getCustID(customer));

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
                tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable MaterialOutSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            string mould = "Mould";
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
                                AND tbl_item.item_cat != @cat AND tbl_item.item_cat != @mould 
                                ORDER BY tbl_item.item_cat ASC, tbl_item.item_name ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@to", to);
                cmd.Parameters.AddWithValue("@mould", mould);

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

        public DataTable MaterialOutSearch(string start, string end, string type)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string to = "Production";

            try
            {
                if(type.ToUpper().Equals("ALL"))
                {
                    dt = MaterialOutSearch(start, end);
                }
                else
                {
                    //sql query to get data from database
                    String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND trf_hist_to=@to
                                AND tbl_item.item_cat = @cat 
                                ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    cmd.Parameters.AddWithValue("@cat", type);
                    cmd.Parameters.AddWithValue("@to", to);

                    //for executing command
                    //getting data from database
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //database connection open
                    conn.Open();
                    //fill data in our database
                    adapter.Fill(dt);
                }
                


            }
            catch (Exception ex)
            {
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

        public DataTable MaterialInOutSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            string mould = "Mould";
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND tbl_item.item_cat != @cat AND tbl_item.item_cat != @mould 
                                ORDER BY tbl_item.item_cat ASC, tbl_item.item_name ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@mould", mould);

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

        public DataTable MaterialInOutSearch(string start, string end, string type)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            try
            {
                if(type.ToUpper().Equals("ALL"))
                {
                    dt = MaterialInOutSearch(start, end);
                }
                else
                {
                    //sql query to get data from database
                    String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND tbl_item.item_cat = @cat 
                                 ORDER BY tbl_item.item_name ASC, tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    cmd.Parameters.AddWithValue("@cat", type);

                    //for executing command
                    //getting data from database
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //database connection open
                    conn.Open();
                    //fill data in our database
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
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

        public DataTable rangeMaterialInFromPMMASearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            string from = "PMMA";

            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND trf_hist_from=@from
                                AND tbl_item.item_cat != @cat";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@from", from);

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


        public DataTable rangeMaterialOutToPMMASearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            string to = "PMMA";

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
                            tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start AND @end 
                            AND tbl_item.item_cat = @cat ORDER BY tbl_item.item_name ASC";
          

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            //dt.DefaultView.Sort = "trf_hist_added_date DESC";
            //DataTable sortedDt = dt.DefaultView.ToTable();

            return dt;
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
                            tbl_trf_hist.trf_hist_trf_date 
                            BETWEEN @start AND @end 
                            AND tbl_item_cust.cust_id = @custID ORDER BY tbl_item.item_code ASC";


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
                if(material.ToUpper().Equals("ALL"))
                {
                    //sql query to get data from database
                    sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND tbl_item.item_cat != @cat ORDER BY tbl_item.item_name  ASC , tbl_trf_hist.trf_hist_trf_date ASC";
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
                                AND tbl_item.item_cat = @material ORDER BY tbl_item.item_name ASC , tbl_trf_hist.trf_hist_trf_date ASC";
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
                                tbl_trf_hist.trf_hist_trf_date 
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
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

        public DataTable pastAddedSearch(int fromPast)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            String sql = null;
            try
            {
                //sql query to get data from database
                sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result
                                FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                tbl_trf_hist.trf_hist_added_date >= DATEADD(day, @fromPast, GetDate())";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@fromPast", fromPast*-1);

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable rangeTrfSearch(string itemCode, string start, string end)
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
                                WHERE trf_hist_trf_date 
                                BETWEEN @start AND @end
                                AND
                                trf_hist_item_code =@itemCode";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@itemCode", itemCode);


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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
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

        public DataTable rangePartTrfKeywordSearch(string keywords, string start, string end)
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
                                WHERE tbl_trf_hist.trf_hist_trf_date 
                                BETWEEN @start 
                                AND @end 
                                AND (tbl_item.item_code LIKE '%" + keywords + "%' OR tbl_item.item_name LIKE '%" + keywords + "%' ) " +
                                " AND tbl_item.item_cat = @cat" +
                                " ORDER BY tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", "Part");

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

        public DataTable rangeMatTrfKeywordSearch(string keywords, string start, string end)
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
                                WHERE tbl_trf_hist.trf_hist_trf_date 
                                BETWEEN @start 
                                AND @end 
                                AND (tbl_item.item_code LIKE '%" + keywords + "%' OR tbl_item.item_name LIKE '%" + keywords + "%' ) " +
                                "AND tbl_item.item_cat != @cat " +
                                "ORDER BY tbl_trf_hist.trf_hist_item_code ASC, tbl_trf_hist.trf_hist_trf_date ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", "Part");


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

        public DataTable TrfSearch(string itemCode, string month, string year)
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
                                tbl_item.item_code =@itemCode AND MONTH(trf_hist_trf_date) = @month AND  YEAR(trf_hist_trf_date) = @year";



                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);


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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
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

        public DataTable catSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result,tbl_trf_hist.balance
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_cat=@category";

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            dt.DefaultView.Sort = "trf_hist_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
            
        }

        public DataTable catTrfRangeAddSearch(string keywords, int fromPast)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  tbl_trf_hist.trf_hist_id,
                                tbl_trf_hist.trf_hist_added_date,
                                tbl_trf_hist.trf_hist_trf_date,
                                tbl_trf_hist.trf_hist_item_code,
                                tbl_item.item_name,
                                tbl_trf_hist.trf_hist_from,
                                tbl_trf_hist.trf_hist_to,
                                tbl_trf_hist.trf_hist_qty,
                                tbl_trf_hist.trf_hist_unit,
                                tbl_trf_hist.trf_hist_note,
                                tbl_trf_hist.trf_hist_added_by,
                                tbl_trf_hist.trf_result,tbl_trf_hist.balance
                                FROM tbl_trf_hist INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_cat=@category
                                AND tbl_trf_hist.trf_hist_added_date >= DATEADD(day, @fromPast, GetDate())";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@category", keywords);
                cmd.Parameters.AddWithValue("@fromPast", fromPast*-1);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
           
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
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

        public bool Delete(string itemCode)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_trf_hist WHERE trf_hist_item_code=@itemCode";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
