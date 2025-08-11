using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class joinDAL
    {

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region data string name getter

        public string JoinParent { get; } = "join_parent_code";
        public string JoinChild { get; } = "join_child_code";

        public string ParentCode { get; } = "parent_code"; 
        public string ChildCode { get; } = "child_code";
        public string ChildName { get; } = "child_name";
        public string ChildCat{ get; } = "child_cat";
        public string JoinQty { get; } = "join_qty";
        public string JoinMax { get; } = "join_max";
        public string JoinMin { get; } = "join_min";
        public string JoinWastage { get; } = "join_wastage";
        public string JoinMainCarton { get; } = "join_main_carton";
        public string JoinStockOut { get; } = "join_stock_out";

        public string JoinAddedDate { get; } = "join_added_date";
        public string JoinAddedBy { get; } = "join_added_by";

        public string JoinUpdatedDate { get; } = "join_updated_date";
        public string JoinUpdatedBy { get; } = "join_updated_by";

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
                String sql = @"SELECT * FROM tbl_join";

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

        public DataTable SelectAllWithChildCat()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_join JOIN tbl_item 
                ON tbl_join.join_child_code = tbl_item.item_code ";

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

        public DataTable Select()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_join.join_parent_code as parent_code, 
                tbl_item.item_name as parent_name ,
                tbl_join.join_child_code as child_code ,
                a.item_name as child_name ,join_qty, join_max, join_min, join_wastage,join_added_date, join_added_by,
                join_updated_date,join_updated_by ,tbl_item.item_capacity, tbl_item.item_pro_pw_shot, tbl_item.item_pro_rw_shot,  tbl_item.item_quo_pw_pcs,tbl_item.item_quo_rw_pcs,tbl_item.item_part_weight,tbl_item.item_runner_weight,tbl_item.item_wastage_allowed,a.item_qty
                FROM tbl_join 
                JOIN tbl_item 
                ON tbl_join.join_parent_code = tbl_item.item_code 
                JOIN tbl_item a ON tbl_join.join_child_code = a.item_code";

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

        public DataTable SelectWithChildCat()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {

                //sql query to get data from database

                String sql = @"SELECT tbl_join.join_parent_code as parent_code, 
                tbl_item.item_name as parent_name ,
                tbl_join.join_child_code as child_code ,
                a.item_name as child_name , a.item_material as item_material , a.item_recycle as item_recycle , a.item_mb as item_mb , a.item_cat as child_cat,
                a.size_tbl_code_1,a.size_tbl_code_2,
                join_qty, join_max, join_min, join_added_date, join_added_by,
                join_updated_date,join_updated_by ,a.item_pro_pw_shot,a.item_pro_rw_shot,a.item_capacity,a.raw_ratio,a.recycle_ratio,a.item_mb_rate,
                a.item_part_weight,a.item_runner_weight,a.item_wastage_allowed,a.item_qty,a.type_tbl_code,a.category_tbl_code
                FROM tbl_join 
                JOIN tbl_item 
                ON tbl_join.join_parent_code = tbl_item.item_code 
                JOIN tbl_item a ON tbl_join.join_child_code = a.item_code";

                //String sql = @"SELECT tbl_join.join_parent_code as parent_code, 
                //tbl_item.item_name as parent_name ,
                //tbl_join.join_child_code as child_code ,
                //a.item_name as child_name ,join_qty, join_max, join_min, join_added_date, join_added_by,
                //join_updated_date,join_updated_by ,tbl_item.item_quo_pw_pcs,tbl_item.item_quo_rw_pcs,tbl_item.item_part_weight,tbl_item.item_runner_weight,tbl_item.item_wastage_allowed,a.item_qty
                //FROM tbl_join 
                //JOIN tbl_item 
                //ON tbl_join.join_parent_code = tbl_item.item_code 
                //JOIN tbl_item a ON tbl_join.join_child_code = a.item_code";

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

        public DataTable SelectWithSBBInfo()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_join.join_parent_code as parent_code, 
                tbl_item.item_name as parent_name ,
                tbl_join.join_child_code as child_code ,
                a.item_name as child_name ,join_qty, join_max, join_min, join_added_date, join_added_by,
                join_updated_date,join_updated_by ,
                tbl_item.item_quo_pw_pcs,
                tbl_item.item_quo_rw_pcs,
                tbl_item.item_part_weight,
                tbl_item.item_runner_weight,
                tbl_item.item_wastage_allowed,
                tbl_spp_type.type_name,
                tbl_spp_size.size_numerator,
                tbl_spp_size.size_denominator,
                tbl_spp_size.size_unit,
                tbl_spp_stdpacking.qty_per_bag,
                a.item_qty
                FROM tbl_join 
                JOIN tbl_item 
                ON tbl_join.join_parent_code = tbl_item.item_code 
                JOIN tbl_item a ON tbl_join.join_child_code = a.item_code
                INNER JOIN tbl_spp_type
                ON a.type_tbl_code = tbl_spp_type.tbl_code
                INNER JOIN tbl_spp_size
                ON a.size_tbl_code_1 = tbl_spp_size.tbl_code
                FULL JOIN tbl_spp_stdpacking
                ON a.item_code = tbl_spp_stdpacking.item_code";

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

        public DataTable SelectwithChildInfo()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_join.join_parent_code as parent_code, 
                tbl_item.item_name as parent_name ,
                tbl_join.join_child_code as child_code ,
                a.item_name as child_name ,
                a.item_cat as child_cat ,
                a.item_material as child_material ,
                a.item_mb as child_mb ,
                a.item_mb_rate as child_mb_rate ,
                a.item_part_weight as child_part_weight ,
                a.item_runner_weight as child_runner_weight ,
                a.item_quo_pw_pcs as child_quo_part_weight ,
                a.item_quo_rw_pcs as child_quo_runner_weight ,
                a.item_wastage_allowed as child_wastage_allowed ,
                a.item_qty as child_qty ,
                a.item_assembly as child_assembly ,
                a.item_production as child_production ,
                join_qty,join_max,join_min 
                FROM tbl_join 
                JOIN tbl_item 
                ON tbl_join.join_parent_code = tbl_item.item_code 
                JOIN tbl_item a ON tbl_join.join_child_code = a.item_code";

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

        public DataTable SelectwithParentInfo()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_join.join_parent_code as parent_code, 
                tbl_item.item_name as parent_name ,
                tbl_join.join_child_code as child_code ,
                a.item_name as child_name ,
                a.item_cat as child_cat ,
                a.item_material as child_material ,
                a.item_mb as child_mb ,
                a.item_mb_rate as child_mb_rate ,
                a.item_part_weight as child_part_weight ,
                a.item_runner_weight as child_runner_weight ,
                a.item_wastage_allowed as child_wastage_allowed ,
                a.item_qty as child_qty ,
                a.item_assembly as child_assembly ,
                a.item_production as child_production ,
                join_qty 
                FROM tbl_join 
                JOIN tbl_item 
                ON tbl_join.join_parent_code = tbl_item.item_code 
                JOIN tbl_item a ON tbl_join.join_child_code = a.item_code";

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

        public bool Insert(joinBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_join (join_parent_code, join_child_code, join_qty, join_added_date, join_added_by) VALUES ( @join_parent_code, @join_child_code,  @join_qty, @join_added_date, @join_added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@join_parent_code", u.join_parent_code);
                cmd.Parameters.AddWithValue("@join_child_code", u.join_child_code);
                cmd.Parameters.AddWithValue("@join_qty", u.join_qty);
                cmd.Parameters.AddWithValue("@join_added_date", u.join_added_date);
                cmd.Parameters.AddWithValue("@join_added_by", u.join_added_by);

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

        public bool InsertWithMaxMin(joinBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_join (join_parent_code, join_child_code, join_qty,join_max,join_min,join_main_carton,join_stock_out,join_added_date, join_added_by) VALUES ( @join_parent_code, @join_child_code, @join_qty, @join_max, @join_min,@join_main_carton, @join_stock_out, @join_added_date, @join_added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@join_parent_code", u.join_parent_code);
                cmd.Parameters.AddWithValue("@join_child_code", u.join_child_code);
                cmd.Parameters.AddWithValue("@join_qty", u.join_qty);
                cmd.Parameters.AddWithValue("@join_max", u.join_max);
                cmd.Parameters.AddWithValue("@join_min", u.join_min);
                cmd.Parameters.AddWithValue("@join_main_carton", u.join_main_carton);
                cmd.Parameters.AddWithValue("@join_stock_out", u.join_stock_out);
                cmd.Parameters.AddWithValue("@join_added_date", u.join_added_date);
                cmd.Parameters.AddWithValue("@join_added_by", u.join_added_by);

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
        
        #region Update Data in Database

        public bool Update(joinBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_join 
                            SET "
                            + JoinQty + "=@join_qty,"
                            + JoinUpdatedDate + "=@join_updated_date,"
                            + JoinUpdatedBy + "=@join_updated_by" +
                            " WHERE join_parent_code=@join_parent_code AND join_child_code=@join_child_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@join_parent_code", u.join_parent_code);
                cmd.Parameters.AddWithValue("@join_child_code", u.join_child_code);
                cmd.Parameters.AddWithValue("@join_qty", u.join_qty);
                cmd.Parameters.AddWithValue("@join_updated_date", u.join_updated_date);
                cmd.Parameters.AddWithValue("@join_updated_by", u.join_updated_by);

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

        public bool UpdateWithMaxMin(joinBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_join 
                            SET "
                            + JoinQty + "=@join_qty,"
                            + JoinMax + "=@join_max,"
                            + JoinMin + "=@join_min,"
                            + JoinMainCarton + "=@join_main_carton,"
                            + JoinStockOut + "=@join_stock_out,"
                            + JoinUpdatedDate + "=@join_updated_date,"
                            + JoinUpdatedBy + "=@join_updated_by" +
                            " WHERE join_parent_code=@join_parent_code AND join_child_code=@join_child_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@join_parent_code", u.join_parent_code);
                cmd.Parameters.AddWithValue("@join_child_code", u.join_child_code);
                cmd.Parameters.AddWithValue("@join_qty", u.join_qty);
                cmd.Parameters.AddWithValue("@join_max", u.join_max);
                cmd.Parameters.AddWithValue("@join_min", u.join_min);
                cmd.Parameters.AddWithValue("@join_main_carton", u.join_main_carton);
                cmd.Parameters.AddWithValue("@join_stock_out", u.join_stock_out);
                cmd.Parameters.AddWithValue("@join_updated_date", u.join_updated_date);
                cmd.Parameters.AddWithValue("@join_updated_by", u.join_updated_by);

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

        #region Delete data from Database
        public bool Delete(joinBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_join WHERE join_parent_code=@join_parent_code AND join_child_code=@join_child_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@join_parent_code", u.join_parent_code);
                cmd.Parameters.AddWithValue("@join_child_code", u.join_child_code);

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

        public bool itemDelete(joinBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_join WHERE join_parent_code=@join_parent_code OR join_child_code=@join_child_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@join_parent_code", u.join_parent_code);
                cmd.Parameters.AddWithValue("@join_child_code", u.join_child_code);

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

        public DataTable Search(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                //String sql = "SELECT * FROM tbl_join WHERE join_parent_code LIKE '%" + keywords + "%' OR join_child_code LIKE '%" + keywords + "%'";

                //sql query to get data from database
                String sql = @"SELECT tbl_join.join_parent_code as parent_code, 
                tbl_item.item_name as parent_name ,
                tbl_join.join_child_code as child_code ,
                tbl_join.join_main_carton, tbl_join.join_stock_out,
                a.item_name as child_name ,join_qty, join_max, join_min, join_added_date, join_added_by,
                join_updated_date,join_updated_by 
                FROM tbl_join 
                JOIN tbl_item 
                ON tbl_join.join_parent_code = tbl_item.item_code 
                JOIN tbl_item a ON tbl_join.join_child_code = a.item_code
                WHERE tbl_join.join_parent_code LIKE '%" + keywords + "%' OR tbl_join.join_child_code LIKE '%" + keywords + "%'" +
                "OR tbl_item.item_name LIKE '%" + keywords + "%' OR a.item_name LIKE '%" + keywords + "%' ORDER BY tbl_join.join_parent_code";

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

        public DataTable OldSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                //String sql = "SELECT * FROM tbl_join WHERE join_parent_code LIKE '%" + keywords + "%' OR join_child_code LIKE '%" + keywords + "%'";

                //sql query to get data from database
                String sql = @"SELECT tbl_join.join_parent_code as parent_code, 
                tbl_item.item_name as parent_name ,
                tbl_join.join_child_code as child_code ,
                a.item_name as child_name ,join_qty, join_max, join_min, join_added_date, join_added_by,
                join_updated_date,join_updated_by 
                FROM tbl_join 
                JOIN tbl_item 
                ON tbl_join.join_parent_code = tbl_item.item_code 
                JOIN tbl_item a ON tbl_join.join_child_code = a.item_code
                WHERE tbl_join.join_parent_code LIKE '%" + keywords + "%' OR tbl_join.join_child_code LIKE '%" + keywords + "%'" +
                "OR tbl_item.item_name LIKE '%" + keywords + "%' OR a.item_name LIKE '%" + keywords + "%' ORDER BY tbl_join.join_parent_code";

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

        public DataTable parentSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_join WHERE join_parent_name LIKE '%" + keywords + "%' OR join_parent_code LIKE '%" + keywords + "%'";

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

        public DataTable childSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_join WHERE join_child_name LIKE '%" + keywords + "%' OR join_child_code LIKE '%" + keywords + "%'";

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

        public DataTable loadChildList(string parentCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_join WHERE join_parent_code = @itemCode";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", parentCode);
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

        public DataTable loadParentList(string childCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_join WHERE join_child_code = @itemCode";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", childCode);
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

        public DataTable existCheck(string parentCode, string childCode)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_join WHERE join_parent_code = @parent_code AND join_child_code = @child_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@parent_code", parentCode);
                cmd.Parameters.AddWithValue("@child_code", childCode);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
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
    }
}
