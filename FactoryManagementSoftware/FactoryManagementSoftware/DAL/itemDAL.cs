using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class itemDAL
    {   
        #region data string name getter
        public string ItemMaterial { get; } = "item_material";
        public string ItemName { get; } = "item_name";
        public string ItemCode { get; } = "item_code";
        public string ItemColor { get; } = "item_color";

        public string ItemQuoTon { get; } = "item_quo_ton";
        public string ItemBestTon { get; } = "item_best_ton";
        public string ItemProTon { get; } = "item_mc";

        public string ItemMBatch { get; } = "item_mb";

        public string ItemQuoCT { get; } = "item_quo_ct";
        public string ItemProCTFrom { get; } = "item_pro_ct_from";
        public string ItemProCTTo { get; } = "item_pro_ct_to";

        public string ItemQuoPWPcs { get; } = "item_quo_pw_pcs";
        public string ItemQuoRWPcs { get; } = "item_quo_rw_pcs";

        public string ItemProPWPcs { get; } = "item_part_weight";
        public string ItemProRWPcs { get; } = "item_runner_weight";

        public string ItemProPWShot { get; } = "item_pro_pw_shot";
        public string ItemProRWShot { get; } = "item_pro_rw_shot";

        public string ItemCapacity { get; } = "item_capacity";
        public string ItemProCooling { get; } = "item_pro_cooling";

        public string ItemCat { get; } = "item_cat";
        public string ItemQty { get; } = "item_qty";
        public string ItemOrd { get; } = "item_ord";
        public string ItemAddDate { get; } = "item_added_date";
        public string ItemAddBy { get; } = "item_added_by";
        public string ItemUpdateDate { get; } = "item_updtd_date";
        public string ItemUpdateBy { get; } = "item_updtd_by";

        public string ItemWastage { get; } = "item_wastage_allowed";

        #endregion

        #region variable/class object declare

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        materialDAL dalMaterial = new materialDAL();

        #endregion

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
                String sql = "SELECT * FROM tbl_item ORDER BY item_cat ASC";
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

        public DataTable catSelect(string category)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_item WHERE item_cat = @category";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@category", category);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion

        #region Insert Data in Database
        public bool Insert(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_item (item_code, item_name, item_cat, item_color, item_material, item_mb, item_mc, item_part_weight, item_runner_weight, item_added_date, item_added_by) VALUES (@item_code, @item_name, @item_cat, @item_color, @item_material, @item_mb, @item_mc, @item_part_weight, @item_runner_weight, @item_added_date, @item_added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);

                if(string.IsNullOrEmpty(u.item_material))
                {
                    cmd.Parameters.AddWithValue("@item_material", DBNull.Value);
                   
                }
                else
                {
                    cmd.Parameters.AddWithValue("@item_material", u.item_material);
                }

                if (string.IsNullOrEmpty(u.item_mb))
                {
                    cmd.Parameters.AddWithValue("@item_mb", DBNull.Value);
                  
                }
                else
                {
                    cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                }

               
                cmd.Parameters.AddWithValue("@item_mc", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_part_weight", u.item_pro_pw_pcs);
                cmd.Parameters.AddWithValue("@item_runner_weight", u.item_pro_rw_pcs);
                cmd.Parameters.AddWithValue("@item_added_date", u.item_added_date);
                cmd.Parameters.AddWithValue("@item_added_by", u.item_added_by);

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

        public bool NewInsert(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_item 
                            (" + ItemCat + ","
                            + ItemCode + ","
                            +ItemName + ","
                            + ItemMaterial + ","
                            + ItemMBatch + ","
                            + ItemColor + ","
                            + ItemQuoTon + ","
                            + ItemBestTon + ","
                            + ItemProTon + ","
                            + ItemQuoCT + ","
                            + ItemProCTFrom + ","
                            + ItemProCTTo + ","
                            + ItemCapacity + ","
                            + ItemQuoPWPcs + ","
                            + ItemQuoRWPcs + ","
                            + ItemProPWPcs + ","
                            + ItemProRWPcs + ","
                            + ItemProPWShot + ","
                            + ItemProRWShot + ","
                            + ItemProCooling + ","
                            + ItemWastage + ","
                            + ItemAddDate + ","
                            + ItemAddBy + ") VALUES " +
                            "(@item_cat," +
                            "@item_code," +
                            "@item_name," +
                            "@item_material," +
                            "@item_mb," +
                            "@item_color," +
                            "@item_quo_ton," +
                            "@item_best_ton," +
                            "@item_pro_ton," +
                            "@item_quo_ct," +
                            "@item_pro_ct_from," +
                            "@item_pro_ct_to," +
                            "@item_capacity," +
                            "@item_quo_pw_pcs," +
                            "@item_quo_rw_pcs," +
                            "@item_pro_pw_pcs," +
                            "@item_pro_rw_pcs," +
                            "@item_pro_pw_shot," +
                            "@item_pro_rw_shot," +
                            "@item_pro_cooling," +
                            "@item_wastage_allowed," +
                            "@item_added_date," +
                            "@item_added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_material", u.item_material);
                cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                cmd.Parameters.AddWithValue("@item_quo_ton", u.item_quo_ton);
                cmd.Parameters.AddWithValue("@item_best_ton", u.item_best_ton);
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_quo_ct", u.item_quo_ct);
                cmd.Parameters.AddWithValue("@item_pro_ct_from", u.item_pro_ct_from);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_capacity", u.item_capacity);
                cmd.Parameters.AddWithValue("@item_quo_pw_pcs", u.item_quo_pw_pcs);
                cmd.Parameters.AddWithValue("@item_quo_rw_pcs", u.item_quo_rw_pcs);
                cmd.Parameters.AddWithValue("@item_pro_pw_pcs", u.item_pro_pw_pcs);
                cmd.Parameters.AddWithValue("@item_pro_rw_pcs", u.item_pro_rw_pcs);
                cmd.Parameters.AddWithValue("@item_pro_pw_shot", u.item_pro_pw_shot);
                cmd.Parameters.AddWithValue("@item_pro_rw_shot", u.item_pro_rw_shot);
                cmd.Parameters.AddWithValue("@item_pro_cooling", u.item_pro_cooling);
                cmd.Parameters.AddWithValue("@item_wastage_allowed", u.item_wastage_allowed);
                cmd.Parameters.AddWithValue("@item_added_date", u.item_added_date);
                cmd.Parameters.AddWithValue("@item_added_by", u.item_added_by);

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

        public bool Update(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_item SET item_name=@item_name, item_cat=@item_cat, item_color=@item_color, item_material=@item_material, item_mb=@item_mb, item_mc=@item_mc, item_part_weight=@item_part_weight, item_runner_weight=@item_runner_weight, item_updtd_date=@item_updtd_date, item_updtd_by=@item_updtd_by WHERE item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                if (string.IsNullOrEmpty(u.item_material))
                {
                    cmd.Parameters.AddWithValue("@item_material", DBNull.Value);
                    
                }
                else
                {
                    cmd.Parameters.AddWithValue("@item_material", u.item_material);
                }

                if (string.IsNullOrEmpty(u.item_mb))
                {
                    cmd.Parameters.AddWithValue("@item_mb", DBNull.Value);
                   
                }
                else
                {
                    cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                }
                cmd.Parameters.AddWithValue("@item_mc", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_part_weight", u.item_pro_pw_pcs);
                cmd.Parameters.AddWithValue("@item_runner_weight", u.item_pro_rw_pcs);
                cmd.Parameters.AddWithValue("@item_updtd_date", u.item_updtd_date);
                cmd.Parameters.AddWithValue("@item_updtd_by", u.item_updtd_by);
                
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

        public bool NewUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemName + "=@item_name," 
                            + ItemMaterial + "=@item_material,"
                            + ItemMBatch + "=@item_mb,"
                            + ItemColor + "=@item_color,"
                            + ItemQuoTon + "=@item_quo_ton,"
                            + ItemBestTon + "=@item_best_ton,"
                            + ItemProTon + "=@item_pro_ton,"
                            + ItemQuoCT + "=@item_quo_ct,"
                            + ItemProCTFrom + "=@item_pro_ct_from,"
                            + ItemProCTTo + "=@item_pro_ct_to,"
                            + ItemCapacity + "=@item_capacity,"
                            + ItemQuoPWPcs + "=@item_quo_pw_pcs,"
                            + ItemQuoRWPcs + "=@item_quo_rw_pcs,"
                            + ItemProPWPcs + "=@item_pro_pw_pcs,"
                            + ItemProRWPcs + "=@item_pro_rw_pcs,"
                            + ItemProPWShot + "=@item_pro_pw_shot,"
                            + ItemProRWShot + "=@item_pro_rw_shot,"
                            + ItemProCooling + "=@item_pro_cooling,"
                            + ItemWastage + "=@item_wastage_allowed,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_material", u.item_material);
                cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                cmd.Parameters.AddWithValue("@item_quo_ton", u.item_quo_ton);
                cmd.Parameters.AddWithValue("@item_best_ton", u.item_best_ton);
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_quo_ct", u.item_quo_ct);
                cmd.Parameters.AddWithValue("@item_pro_ct_from", u.item_pro_ct_from);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_capacity", u.item_capacity);
                cmd.Parameters.AddWithValue("@item_quo_pw_pcs", u.item_quo_pw_pcs);
                cmd.Parameters.AddWithValue("@item_quo_rw_pcs", u.item_quo_rw_pcs);
                cmd.Parameters.AddWithValue("@item_pro_pw_pcs", u.item_pro_pw_pcs);
                cmd.Parameters.AddWithValue("@item_pro_rw_pcs", u.item_pro_rw_pcs);
                cmd.Parameters.AddWithValue("@item_pro_pw_shot", u.item_pro_pw_shot);
                cmd.Parameters.AddWithValue("@item_pro_rw_shot", u.item_pro_rw_shot);
                cmd.Parameters.AddWithValue("@item_pro_cooling", u.item_pro_cooling);
                cmd.Parameters.AddWithValue("@item_wastage_allowed", u.item_wastage_allowed);
                cmd.Parameters.AddWithValue("@item_updtd_date", u.item_updtd_date);
                cmd.Parameters.AddWithValue("@item_updtd_by", u.item_updtd_by);
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

        public bool qtyUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_item SET item_qty=@item_qty, item_updtd_date=@item_updtd_date, item_updtd_by=@item_updtd_by WHERE item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_qty", u.item_qty);
                cmd.Parameters.AddWithValue("@item_updtd_date", u.item_updtd_date);
                cmd.Parameters.AddWithValue("@item_updtd_by", u.item_updtd_by);

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

        public bool ordUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_item SET item_ord=@item_ord, item_updtd_date=@item_updtd_date, item_updtd_by=@item_updtd_by WHERE item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_ord", u.item_ord);
                cmd.Parameters.AddWithValue("@item_updtd_date", u.item_updtd_date);
                cmd.Parameters.AddWithValue("@item_updtd_by", u.item_updtd_by);

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

        public bool orderAdd(string itemCode, string ordQty)
        {
            itemBLL uItem = new itemBLL();

            float number = Convert.ToSingle(ordQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;
            uItem.item_ord = getOrderQty(itemCode) + number;

            //Updating data into database
            bool success = ordUpdate(uItem);

            return success;
        }

        public bool orderSubtract(string itemCode, string ordQty)
        {
            itemBLL uItem = new itemBLL();
            float number = Convert.ToSingle(ordQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;
            uItem.item_ord = getOrderQty(itemCode) - number;

            //Updating data into database
            bool success = ordUpdate(uItem);

            return success;

        }

        public bool stockAdd(string itemCode, string stockQty)
        {
            itemBLL uItem = new itemBLL();

            float number = Convert.ToSingle(stockQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;
            uItem.item_qty = getStockQty(itemCode) + number;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;
        }

        public bool stockSubtract(string itemCode, string stockQty)
        {
            itemBLL uItem = new itemBLL();
            float number = Convert.ToSingle(stockQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;
            uItem.item_qty = getStockQty(itemCode) - number;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;

        }

        public bool updateTotalStock(string itemCode)
        {
            float totalStock = 0;
            facStockDAL dalStock = new facStockDAL();
            itemBLL uItem = new itemBLL();

            DataTable dtStock = dalStock.Select(itemCode);

            foreach (DataRow stock in dtStock.Rows)
            {
                totalStock += Convert.ToSingle(stock["stock_qty"].ToString());
            }

            //Update data
            uItem.item_code = itemCode;
            uItem.item_qty = totalStock;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;
        }

        #endregion

        #region Delete data from Database
        public bool Delete(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_item WHERE item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);

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

        #region Search

        public DataTable Search(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_code LIKE '%" + keywords + "%'OR item_name LIKE '%" + keywords + "%'";

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

        public DataTable catItemSearch(string keywords, string category)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE (item_code LIKE '%" + keywords + "%'OR item_name LIKE '%" + keywords + "%') AND item_cat = @category";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@category", category);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
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

        public DataTable itemMaterialSearch(string material)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_material=@material";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@material", material);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
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

        public DataTable itemMBSearch(string mb)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_mb=@mb";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mb", mb);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
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
                String sql = "SELECT * FROM tbl_item WHERE item_cat=@category";

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

        public DataTable nameSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_name LIKE '%" + keywords + "%'";

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

        public DataTable itemCodeSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_code LIKE '%" + keywords + "%'";

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
                String sql = "SELECT * FROM tbl_item WHERE item_code=@item_code";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@item_code", keywords);
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

        public string getMaterialName(string materialCode)
        {
            string materialName = "";
            DataTable dt = dalMaterial.codeSearch(materialCode);

            if (dt.Rows.Count > 0)
            {
                materialName = dt.Rows[0]["material_name"].ToString();
            }


            return materialName;
        }

        public string getMBName(string mbCode)
        {
            string mbName = "";
            DataTable dt = dalMaterial.codeSearch(mbCode);

            if (dt.Rows.Count > 0)
            {
                mbName = dt.Rows[0]["material_name"].ToString();
            }


            return mbName;
        }

        public float getOrderQty(string itemCode)
        {
            float orderQty = 0;
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                orderQty = Convert.ToSingle(dt.Rows[0]["item_ord"].ToString());
                //MessageBox.Show("get qty= "+qty);
            }



            return orderQty;
        }

        public float getStockQty(string itemCode)
        {
            float stockQty = 0;
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                stockQty = Convert.ToSingle(dt.Rows[0]["item_qty"].ToString());
            }

            //MessageBox.Show("get qty= "+qty);


            return stockQty;
        }

        #endregion

    }
}
