﻿using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using FactoryManagementSoftware.UI;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace FactoryManagementSoftware.DAL
{
    class itemDAL
    {   
        #region data string name getter
        public string ItemMaterial { get; } = "item_material";
        public string ItemRecycleMat { get; } = "item_recycle";
        public string ItemName { get; } = "item_name";
        public string ItemCode { get; } = "item_code";
        public string ItemCodePresent { get; } = "item_code_present";
        public string ItemRemark { get; } = "item_remark";
        public string ItemCustRemark { get; } = "item_cust_remark";
        public string ItemColor { get; } = "item_color";
        public string ItemRawRatio { get; } = "raw_ratio";
        public string ItemRecycleRatio { get; } = "recycle_ratio";
        public string ItemUnit { get; } = "item_unit";
        public string ItemUnitToPCSRate { get; } = "unit_to_pcs_rate";
        public string ItemisRemoved { get; } = "isRemoved";

        public string ItemQuoTon { get; } = "item_quo_ton";
        public string ItemBestTon { get; } = "item_best_ton";
        public string ItemProTon { get; } = "item_mc";
        public string ItemMBRate { get; } = "item_mb_rate";
        public string ColorFollowRaw { get; } = "raw_color";
        public string ColorFollowRecycle { get; } = "recycle_color";

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

        public string ItemCavity { get; } = "item_capacity";
        public string ItemProCooling { get; } = "item_pro_cooling";

        public string ItemCat { get; } = "item_cat";
        
        public string ItemOrd { get; } = "item_ord";
        public string ItemAddDate { get; } = "item_added_date";
        public string ItemAddBy { get; } = "item_added_by";
        public string ItemUpdateDate { get; } = "item_updtd_date";
        public string ItemUpdateBy { get; } = "item_updtd_by";

        public string AddedDate { get; } = "added_date";
        public string AddedBy { get; } = "added_by";

        public string UpdatedDate { get; } = "updated_date";
        public string UpdatedBy { get; } = "updated_by";

        public string ItemWastage { get; } = "item_wastage_allowed";

        public string ItemCurrentMonth { get; } = "item_current_month";
        public string ItemLastQty { get; } = "item_last_qty";
        public string ItemStock { get; } = "item_qty";
        public string ItemLastPMMAQty { get; } = "item_last_pmma_qty";
        public string ItemLastPERMABONNQty { get; } = "item_last_permabonn_qty";
        public string ItemPMMAQty { get; } = "item_pmma_qty";
        public string ItemPERMABONNQty { get; } = "item_permabonn_qty";

        public string ItemAssemblyCheck { get; } = "item_assembly";
        public string ItemProductionCheck { get; } = "item_production";
        public string ItemSBBCheck { get; } = "item_sbb";

        public string ItemSize1 { get; } = "size_tbl_code_1";
        public string ItemSize2 { get; } = "size_tbl_code_2";
        public string ItemSize3 { get; } = "size_tbl_code_3";
        public string TypeTblCode { get; } = "type_tbl_code";
        public string CategoryTblCode { get; } = "category_tbl_code";
        public string TblCode { get; } = "tbl_code";
        public string MouldCode { get; } = "mould_code";
        public string CombinationCode { get; } = "combination_code";
        public string MouldCavity { get; } = "mould_cavity";
        public string MouldCT { get; } = "mould_ct";
        public string ItemPWShot { get; } = "item_pw_shot";
        public string ItemRWShot { get; } = "item_rw_shot";
        public string MouldDefaultSelection { get; } = "default_selection";
        public string Removed { get; } = "removed";
        public string MatCode { get; } = "mat_code";
        public string MatRatio { get; } = "mat_ratio";
        public string MatGroup { get; } = "group_no";
        public string KGPerBag { get; } = "kg_per_bag";



        #endregion

        #region variable/class object declare

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        materialDAL dalMaterial = new materialDAL();
        //Tool tool = new Tool();
        //Text text = new Text();
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable MouldItemSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_mould_item ORDER BY item_code ASC";
                //String sql = "SELECT * FROM tbl_mould_item WHERE (removed IS NULL OR removed = FALSE) ORDER BY item_code ASC";

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

        public DataTable ItemMaterialFormulaSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item_mat_formula ORDER BY item_code ASC";

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
        public DataTable MouldItemNonRemovedSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                //String sql = "SELECT * FROM tbl_mould_item ORDER BY item_code ASC";
                string sql = "SELECT * FROM tbl_mould_item WHERE (removed IS NULL OR removed = 0) ORDER BY item_code ASC";


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
        public DataTable SPPSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_item.item_name as NAME FROM tbl_item
                            INNER JOIN tbl_spp_category 
                            ON (tbl_item.category_tbl_code = tbl_spp_category.tbl_code )";

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

        public DataTable SPPCommonSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database

            string commonPart = new Text().Cat_CommonPart;

            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_spp_size.size_numerator as SIZE , tbl_spp_size.size_unit as UNIT ,tbl_spp_type.type_name as TYPE , tbl_item.item_qty as STOCK
                            FROM tbl_item
                            INNER JOIN tbl_spp_category 
                            ON (tbl_item.category_tbl_code = tbl_spp_category.tbl_code AND tbl_spp_category.category_name = @commonPart)
                            INNER JOIN tbl_spp_type
                            ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                            INNER JOIN tbl_spp_size
                            ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@commonPart", commonPart);
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

        public DataTable SPPUniqueSelect()
        {
            //static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database

            string commonPart = new Text().Cat_CommonPart;

            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_spp_category.category_name as CATEGORY , 
                            tbl_spp_type.type_name as TYPE , 
                            tbl_spp_size.size_numerator as SIZE , 
                            tbl_spp_size.size_unit as UNIT ,
                            tbl_item.item_code as CODE , 
                            tbl_item.item_qty as QUANTITY,
                            tbl_spp_stdpacking.qty_per_bag as STD_PACKING,
                            tbl_item.to_delivery_qty as TO_DELIVERY_QTY
                            FROM tbl_item
                            INNER JOIN tbl_spp_category 
                            ON (tbl_item.category_tbl_code = tbl_spp_category.tbl_code AND tbl_spp_category.category_name != @commonPart)
                            INNER JOIN tbl_spp_type
                            ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                            INNER JOIN tbl_spp_size
                            ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code 
                            FULL JOIN tbl_spp_stdpacking
                            ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                            ORDER BY tbl_spp_type.type_name ASC, tbl_spp_size.size_numerator ASC,  tbl_spp_category.category_name ASC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@commonPart", commonPart);
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

        public DataTable SPPUniqueSelectWithoutAssembledItem()
        {
            //static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database

            string commonPart = new Text().Cat_CommonPart;
            string assembled = new Text().Cat_Assembled;

            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_spp_category.category_name as CATEGORY , 
                            tbl_spp_type.type_name as TYPE , 
                            tbl_spp_size.size_numerator as SIZE , 
                            tbl_spp_size.size_unit as UNIT ,
                            tbl_item.item_code as CODE , 
                            tbl_item.item_qty as QUANTITY,
                            tbl_spp_stdpacking.qty_per_bag as STD_PACKING,
                            tbl_spp_stdpacking.max_level as MAX_LEVEL,
                            tbl_item.to_delivery_qty as TO_DELIVERY_QTY
                            FROM tbl_item
                            INNER JOIN tbl_spp_category 
                            ON (tbl_item.category_tbl_code = tbl_spp_category.tbl_code AND tbl_spp_category.category_name != @commonPart AND tbl_spp_category.category_name != @assembled)
                            INNER JOIN tbl_spp_type
                            ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                            INNER JOIN tbl_spp_size
                            ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code  OR tbl_item.size_tbl_code_2 = tbl_spp_size.tbl_code
                            FULL JOIN tbl_spp_stdpacking
                            ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                            ORDER BY tbl_spp_type.type_name ASC, tbl_spp_size.size_numerator ASC,  tbl_spp_category.category_name ASC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@commonPart", commonPart);
                cmd.Parameters.AddWithValue("@assembled", assembled);

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

        public DataTable SPPNonReadyGoodsSelect()
        {
            //static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database

            string readyGoods = new Text().Cat_ReadyGoods;

            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_spp_category.category_name as CATEGORY , 
                            tbl_spp_type.type_name as TYPE , 
                            tbl_spp_size.size_numerator as SIZE , 
                            tbl_spp_size.size_unit as UNIT ,
                            tbl_item.item_code as CODE , 
                            tbl_item.item_qty as QUANTITY,
                            tbl_spp_stdpacking.qty_per_bag as STD_PACKING
                            FROM tbl_item
                            INNER JOIN tbl_spp_category 
                            ON (tbl_item.category_tbl_code = tbl_spp_category.tbl_code AND tbl_spp_category.category_name != @readyGoods)
                            INNER JOIN tbl_spp_type
                            ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                            INNER JOIN tbl_spp_size
                            ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code 
                            FULL JOIN tbl_spp_stdpacking
                            ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                            ORDER BY tbl_spp_type.type_name ASC, tbl_spp_size.size_numerator ASC,  tbl_spp_category.category_name ASC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@readyGoods", readyGoods);
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

        public DataTable SPPReadyGoodsSelect()
        {
            //static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database

            string readyGoods = new Text().Cat_ReadyGoods;

            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT 
                            tbl_spp_type.type_name as TYPE ,
                            tbl_spp_size.tbl_code,
                            tbl_spp_size.size_numerator as SIZE , 
                            tbl_spp_size.size_unit as UNIT ,
                            tbl_item.item_code as CODE , 
                            tbl_item.item_name as NAME ,
                            tbl_item.item_qty as QUANTITY,
                            tbl_spp_stdpacking.qty_per_bag as STD_PACKING,
                            tbl_spp_stdpacking.qty_per_packet as QTY_PACKET,                            
                            tbl_spp_stdpacking.qty_per_container as QTY_CONTAINER
                            FROM tbl_item
                            INNER JOIN tbl_spp_category 
                            ON (tbl_item.category_tbl_code = tbl_spp_category.tbl_code AND tbl_spp_category.category_name = @readyGoods)
                            INNER JOIN tbl_spp_type
                            ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                            INNER JOIN tbl_spp_size
                            ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code OR tbl_item.size_tbl_code_2 = tbl_spp_size.tbl_code 
                            INNER JOIN tbl_spp_stdpacking
                            ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                            ORDER BY tbl_spp_type.type_name ASC, tbl_spp_size.size_numerator ASC,  tbl_spp_category.category_name ASC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@readyGoods", readyGoods);
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

        public DataTable SBBGoodsSelect()
        {
            //static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database

            string readyGoods = new Text().Cat_ReadyGoods;

            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_item
                            INNER JOIN tbl_spp_type
                            ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                            INNER JOIN tbl_spp_size
                            ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code 
                            INNER JOIN tbl_spp_stdpacking
                            ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                            ORDER BY tbl_spp_type.type_name ASC, tbl_spp_size.size_numerator ASC,  tbl_spp_category.category_name ASC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@readyGoods", readyGoods);
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

        public DataTable InOutSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT item_cat,item_code, item_name,item_ord,item_qty FROM tbl_item ORDER BY item_cat ASC";
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                            + ItemMBRate + ","
                            + ItemColor + ","
                            + ItemQuoTon + ","
                            + ItemBestTon + ","
                            + ItemProTon + ","
                            + ItemQuoCT + ","
                            + ItemProCTFrom + ","
                            + ItemProCTTo + ","
                            + ItemCavity + ","
                            + ItemQuoPWPcs + ","
                            + ItemQuoRWPcs + ","
                            + ItemProPWPcs + ","
                            + ItemProRWPcs + ","
                            + ItemProPWShot + ","
                            + ItemProRWShot + ","
                            + ItemProCooling + ","
                            + ItemWastage + ","
                            + ItemAddDate + ","
                            + ItemAddBy + ","
                            + ItemAssemblyCheck + ","
                            + ItemProductionCheck + ") VALUES" +
                            "(@item_cat,"+
                            "@item_code," +
                            "@item_name," +
                            "@item_material," +
                            "@item_mb," +
                            "@item_mb_rate," +
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
                            "@item_added_by," +
                            "@item_assembly," +
                            "@item_production)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_material", u.item_material);
                cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                cmd.Parameters.AddWithValue("@item_mb_rate", u.item_mb_rate);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                cmd.Parameters.AddWithValue("@item_quo_ton", u.item_quo_ton);
                cmd.Parameters.AddWithValue("@item_best_ton", u.item_best_ton);
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_quo_ct", u.item_quo_ct);
                cmd.Parameters.AddWithValue("@item_pro_ct_from", u.item_pro_ct_from);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_capacity", u.item_cavity);
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
                cmd.Parameters.AddWithValue("@item_assembly", u.item_assembly);
                cmd.Parameters.AddWithValue("@item_production", u.item_production);

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

        public bool ItemMaterialFormulaInsert(itemBLL u)
        {

            

            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_item_mat_formula 
                            (" + ItemCode + ","
                            + MatCode + ","
                            + MatRatio + ","
                            + MatGroup + ","
                            + KGPerBag + ","
                            + AddedDate + ","
                            + AddedBy + ") VALUES" +
                            "(@item_code," +
                            "@mat_code," +
                            "@mat_ratio," +
                            "@mat_formula_group," +
                            "@kg_per_bag," +
                            "@added_date," +
                            "@added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@mat_code", u.mat_code);
                cmd.Parameters.AddWithValue("@mat_ratio", u.mat_ratio);
                cmd.Parameters.AddWithValue("@kg_per_bag", u.kg_per_bag);
                cmd.Parameters.AddWithValue("@mat_formula_group", u.mat_formula_group);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

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

        public bool MouldInsert(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_item 
                            (" + ItemCat + ","
                            + ItemCode + ","
                            + ItemCodePresent + ","
                            + ItemName + ","
                            + ItemQuoTon + ","
                            + ItemBestTon + ","
                            + ItemProTon + ","
                            + ItemQuoCT + ","
                            + ItemProCTFrom + ","
                            + ItemProCTTo + ","
                            + ItemCavity + ","
                            + ItemProPWShot + ","
                            + ItemProRWShot + ","
                            + ItemAddDate + ","
                            + ItemAddBy + ") VALUES" +
                            "(@item_cat," +
                            "@item_code," +
                            "@item_code_present," +
                            "@item_name," +
                            "@item_quo_ton," +
                            "@item_best_ton," +
                            "@item_pro_ton," +
                            "@item_quo_ct," +
                            "@item_pro_ct_from," +
                            "@item_pro_ct_to," +
                            "@item_capacity," +
                            "@item_pro_pw_shot," +
                            "@item_pro_rw_shot," +
                            "@item_added_date," +
                            "@item_added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_code_present", u.item_code_present);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_quo_ton", u.item_quo_ton);
                cmd.Parameters.AddWithValue("@item_best_ton", u.item_best_ton);
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_quo_ct", u.item_quo_ct);
                cmd.Parameters.AddWithValue("@item_pro_ct_from", u.item_pro_ct_from);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_capacity", u.item_cavity);
                cmd.Parameters.AddWithValue("@item_pro_pw_shot", u.item_pro_pw_shot);
                cmd.Parameters.AddWithValue("@item_pro_rw_shot", u.item_pro_rw_shot);
               
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
                Module.Tool tool = new Module.Tool(); tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public bool MouldItemInsert(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_mould_item 
                            (" + MouldCode + ","
                            + CombinationCode + ","
                            + ItemCode + ","
                            + MouldCavity + ","
                            + MouldCT + ","
                            + ItemPWShot + ","
                            + ItemRWShot + ","
                            + MouldDefaultSelection + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@mould_code," +
                            "@combination_code," +
                            "@item_code," +
                            "@mould_cavity," +
                            "@mould_ct," +
                            "@item_pw_shot," +
                            "@item_rw_shot," +
                            "@MouldDefaultSelection," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mould_code", u.mould_code);
                cmd.Parameters.AddWithValue("@combination_code", u.combination_code);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@mould_cavity", u.mould_cavity);
                cmd.Parameters.AddWithValue("@mould_ct", u.mould_ct);
                cmd.Parameters.AddWithValue("@item_pw_shot", u.item_pw_shot);
                cmd.Parameters.AddWithValue("@item_rw_shot", u.item_rw_shot);
                cmd.Parameters.AddWithValue("@MouldDefaultSelection", u.MouldDefaultSelection);
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
        public bool ItemMasterList_ItemAdd(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_item 
                            (" + ItemCat + ","
                            + ItemCode + ","
                            + ItemName + ","
                            + ItemUnit + ","
                            + ItemMaterial + ","
                            + ItemMBatch + ","
                            + ItemMBRate + ","
                            + ItemColor + ","
                            + ItemQuoTon + ","
                            + ItemBestTon + ","
                            + ItemProTon + ","
                            + ItemQuoCT + ","
                            + ItemProCTFrom + ","
                            + ItemProCTTo + ","
                            + ItemCavity + ","
                            + ItemQuoPWPcs + ","
                            + ItemQuoRWPcs + ","
                            + ItemProPWPcs + ","
                            + ItemProRWPcs + ","
                            + ItemProPWShot + ","
                            + ItemProRWShot + ","
                            + ItemProCooling + ","
                            + ItemWastage + ","
                            + ItemAddDate + ","
                            + ItemAddBy + ","
                            + ItemUnitToPCSRate + ","
                            + ItemAssemblyCheck + ","
                            + ItemProductionCheck + ","
                            + ItemSBBCheck + ","
                            + CategoryTblCode + ","
                            + TypeTblCode + ","
                            + ItemSize1 + ","
                            + ItemSize2 + ") VALUES" +
                            "(@item_cat," +
                            "@item_code," +
                            "@item_name," +
                            "@item_unit," +
                            "@item_material," +
                            "@item_mb," +
                            "@item_mb_rate," +
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
                            "@item_added_by," +
                            "@unit_to_pcs_rate," +
                            "@item_assembly," +
                            "@item_production," +
                            "@item_sbb," +
                            "@Category_tbl_code," +
                            "@Type_tbl_code," +
                            "@Size_tbl_code_1," +
                            "@Size_tbl_code_2)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_unit", u.item_unit);
                cmd.Parameters.AddWithValue("@item_material", u.item_material);
                cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                cmd.Parameters.AddWithValue("@item_mb_rate", u.item_mb_rate);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                cmd.Parameters.AddWithValue("@item_quo_ton", u.item_quo_ton);
                cmd.Parameters.AddWithValue("@item_best_ton", u.item_best_ton);
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_quo_ct", u.item_quo_ct);
                cmd.Parameters.AddWithValue("@item_pro_ct_from", u.item_pro_ct_from);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_capacity", u.item_cavity);
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
                cmd.Parameters.AddWithValue("@item_assembly", u.item_assembly);
                cmd.Parameters.AddWithValue("@item_production", u.item_production);

                cmd.Parameters.AddWithValue("@unit_to_pcs_rate", u.unit_to_pcs_rate);

                cmd.Parameters.AddWithValue("@Type_tbl_code", u.Type_tbl_code);
                cmd.Parameters.AddWithValue("@Category_tbl_code", u.Category_tbl_code);
                cmd.Parameters.AddWithValue("@Size_tbl_code_1", u.Size_tbl_code_1);
                cmd.Parameters.AddWithValue("@Size_tbl_code_2", u.Size_tbl_code_2);
                cmd.Parameters.AddWithValue("@item_sbb", u.item_sbb);

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

        public bool SBBItemInsert(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_item 
                            (" + ItemCat + ","
                            + ItemCode + ","
                            + ItemName + ","
                            + TypeTblCode + ","
                            + CategoryTblCode + ","
                            + ItemSize1 + ","
                            + ItemSize2 + ","
                            + ItemAddDate + ","
                            + ItemAddBy + ","
                            + ItemAssemblyCheck + ","
                            + ItemProductionCheck + ") VALUES" +
                            "(@item_cat," +
                            "@item_code," +
                            "@item_name," +
                            "@Type_tbl_code," +
                            "@Category_tbl_code," +
                            "@Size_tbl_code_1," +
                            "@Size_tbl_code_2," +
                            "@item_added_date," +
                            "@item_added_by," +
                            "@item_assembly," +
                            "@item_production)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);

                cmd.Parameters.AddWithValue("@Type_tbl_code", u.Type_tbl_code);
                cmd.Parameters.AddWithValue("@Category_tbl_code", u.Category_tbl_code);

                cmd.Parameters.AddWithValue("@Size_tbl_code_1", u.Size_tbl_code_1);
                cmd.Parameters.AddWithValue("@Size_tbl_code_2", u.Size_tbl_code_2);

                cmd.Parameters.AddWithValue("@item_added_date", u.item_added_date);
                cmd.Parameters.AddWithValue("@item_added_by", u.item_added_by);
                cmd.Parameters.AddWithValue("@item_assembly", u.item_assembly);
                cmd.Parameters.AddWithValue("@item_production", u.item_production);

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                            + ItemMBRate + "=@item_mb_rate,"
                            + ItemColor + "=@item_color,"
                            + ItemQuoTon + "=@item_quo_ton,"
                            + ItemBestTon + "=@item_best_ton,"
                            + ItemProTon + "=@item_pro_ton,"
                            + ItemQuoCT + "=@item_quo_ct,"
                            + ItemProCTFrom + "=@item_pro_ct_from,"
                            + ItemProCTTo + "=@item_pro_ct_to,"
                            + ItemCavity + "=@item_capacity,"
                            + ItemQuoPWPcs + "=@item_quo_pw_pcs,"
                            + ItemQuoRWPcs + "=@item_quo_rw_pcs,"
                            + ItemProPWPcs + "=@item_pro_pw_pcs,"
                            + ItemProRWPcs + "=@item_pro_rw_pcs,"
                            + ItemProPWShot + "=@item_pro_pw_shot,"
                            + ItemProRWShot + "=@item_pro_rw_shot,"
                            + ItemProCooling + "=@item_pro_cooling,"
                            + ItemWastage + "=@item_wastage_allowed,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by,"
                            + ItemAssemblyCheck + "=@item_assembly,"
                            + ItemUnit + "=@item_unit,"
                            + ItemUnitToPCSRate + "=@unit_to_pcs_rate,"
                            + ItemProductionCheck + "=@item_production" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_material", u.item_material);
                cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                cmd.Parameters.AddWithValue("@item_mb_rate", u.item_mb_rate);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                cmd.Parameters.AddWithValue("@item_quo_ton", u.item_quo_ton);
                cmd.Parameters.AddWithValue("@item_best_ton", u.item_best_ton);
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_quo_ct", u.item_quo_ct);
                cmd.Parameters.AddWithValue("@item_pro_ct_from", u.item_pro_ct_from);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_capacity", u.item_cavity);
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
                cmd.Parameters.AddWithValue("@item_assembly", u.item_assembly);
                cmd.Parameters.AddWithValue("@item_production", u.item_production);
                cmd.Parameters.AddWithValue("@item_unit", u.item_unit);
                cmd.Parameters.AddWithValue("@unit_to_pcs_rate", u.unit_to_pcs_rate);
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

        public bool MouldUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemCodePresent + "=@item_code_present,"
                            + ItemName + "=@item_name,"
                            + ItemProTon + "=@item_pro_ton,"
                            + ItemProCTTo + "=@item_pro_ct_to,"
                            + ItemCavity + "=@item_cavity,"
                            + ItemProPWShot + "=@item_pro_pw_shot,"
                            + ItemProRWShot + "=@item_pro_rw_shot,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_code_present", u.item_code_present);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
             
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
               
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);

                cmd.Parameters.AddWithValue("@item_cavity", u.item_cavity);

                cmd.Parameters.AddWithValue("@item_pro_pw_shot", u.item_pro_pw_shot);
                cmd.Parameters.AddWithValue("@item_pro_rw_shot", u.item_pro_rw_shot);

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool MouldItemUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_mould_item 
                            SET "
                            + MouldCode + "=@mould_code,"
                            + ItemCode + "=@item_code,"
                            + MouldCavity + "=@mould_cavity,"
                            + MouldCT + "=@mould_ct,"
                            + ItemPWShot + "=@item_pw_shot,"
                            + ItemRWShot + "=@item_rw_shot,"
                            + MouldDefaultSelection + "=@MouldDefaultSelection,"
                            + Removed + "=@removed,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@mould_code", u.mould_code);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@mould_cavity", u.mould_cavity);
                cmd.Parameters.AddWithValue("@mould_ct", u.mould_ct);
                cmd.Parameters.AddWithValue("@item_pw_shot", u.item_pw_shot);
                cmd.Parameters.AddWithValue("@item_rw_shot", u.item_rw_shot);
                cmd.Parameters.AddWithValue("@MouldDefaultSelection", u.MouldDefaultSelection);
                cmd.Parameters.AddWithValue("@removed", u.removed);
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

        public bool MouldItemProInfoUpdateByCombinationCode(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_mould_item 
                            SET "
                            + MouldCavity + "=@mould_cavity,"
                            + MouldCT + "=@mould_ct,"
                            + ItemPWShot + "=@item_pw_shot,"
                            + ItemRWShot + "=@item_rw_shot,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE combination_code=@combination_code AND mould_code=@mould_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@combination_code", u.combination_code);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@mould_code", u.mould_code);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@mould_cavity", u.mould_cavity);
                cmd.Parameters.AddWithValue("@mould_ct", u.mould_ct);
                cmd.Parameters.AddWithValue("@item_pw_shot", u.item_pw_shot);
                cmd.Parameters.AddWithValue("@item_rw_shot", u.item_rw_shot);
                cmd.Parameters.AddWithValue("@MouldDefaultSelection", u.MouldDefaultSelection);
                cmd.Parameters.AddWithValue("@removed", u.removed);
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

        public bool MouldItemDefaultUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_mould_item 
                            SET "
                            + MouldCode + "=@mould_code,"
                            + ItemCode + "=@item_code,"
                            + MouldDefaultSelection + "=@MouldDefaultSelection,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@mould_code", u.mould_code);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@MouldDefaultSelection", u.MouldDefaultSelection);
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
        public bool ItemMasterList_ItemUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemCat + "=@item_cat,"
                            + ItemName + "=@item_name,"
                            + ItemUnit + "=@item_unit,"
                            + ItemMaterial + "=@item_material,"
                            + ItemMBatch + "=@item_mb,"
                            + ItemMBRate + "=@item_mb_rate,"
                            + ItemColor + "=@item_color,"
                            + ItemQuoTon + "=@item_quo_ton,"
                            + ItemBestTon + "=@item_best_ton,"
                            + ItemProTon + "=@item_pro_ton,"
                            + ItemQuoCT + "=@item_quo_ct,"
                            + ItemProCTFrom + "=@item_pro_ct_from,"
                            + ItemProCTTo + "=@item_pro_ct_to,"
                            + ItemCavity + "=@item_cavity,"
                            + ItemQuoPWPcs + "=@item_quo_pw_pcs,"
                            + ItemQuoRWPcs + "=@item_quo_rw_pcs,"
                            + ItemProPWPcs + "=@item_pro_pw_pcs,"
                            + ItemProRWPcs + "=@item_pro_rw_pcs,"
                            + ItemProPWShot + "=@item_pro_pw_shot,"
                            + ItemProRWShot + "=@item_pro_rw_shot,"
                            + ItemProCooling + "=@item_pro_cooling,"
                            + ItemWastage + "=@item_wastage_allowed,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by,"
                            + ItemAssemblyCheck + "=@item_assembly,"
                            + ItemSBBCheck + "=@item_sbb,"
                            + CategoryTblCode + "=@Category_tbl_code,"
                            + TypeTblCode + "=@Type_tbl_code,"
                            + ItemSize1 + "=@Size_tbl_code_1,"
                            + ItemSize2 + "=@Size_tbl_code_2,"
                            + ItemUnitToPCSRate + "=@unit_to_pcs_rate,"
                            + ItemProductionCheck + "=@item_production" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_unit", u.item_unit);
                cmd.Parameters.AddWithValue("@item_material", u.item_material);
                cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                cmd.Parameters.AddWithValue("@item_mb_rate", u.item_mb_rate);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                cmd.Parameters.AddWithValue("@item_quo_ton", u.item_quo_ton);
                cmd.Parameters.AddWithValue("@item_best_ton", u.item_best_ton);
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_quo_ct", u.item_quo_ct);
                cmd.Parameters.AddWithValue("@item_pro_ct_from", u.item_pro_ct_from);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_cavity", u.item_cavity);
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
                cmd.Parameters.AddWithValue("@item_assembly", u.item_assembly);
                cmd.Parameters.AddWithValue("@item_production", u.item_production);

                cmd.Parameters.AddWithValue("@unit_to_pcs_rate", u.unit_to_pcs_rate);

                cmd.Parameters.AddWithValue("@Type_tbl_code", u.Type_tbl_code);
                cmd.Parameters.AddWithValue("@Category_tbl_code", u.Category_tbl_code);
                cmd.Parameters.AddWithValue("@Size_tbl_code_1", u.Size_tbl_code_1);
                cmd.Parameters.AddWithValue("@Size_tbl_code_2", u.Size_tbl_code_2);
                cmd.Parameters.AddWithValue("@item_sbb", u.item_sbb);

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
        public bool SBBItemUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemName + "=@item_name,"
                            + TypeTblCode + "=@Type_tbl_code,"
                            + CategoryTblCode + "=@Category_tbl_code,"
                            + ItemSize1 + "=@Size_tbl_code_1,"
                            + ItemSize2 + "=@Size_tbl_code_2,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by,"
                            + ItemAssemblyCheck + "=@item_assembly,"
                            + ItemProductionCheck + "=@item_production" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);

                cmd.Parameters.AddWithValue("@Type_tbl_code", u.Type_tbl_code);
                cmd.Parameters.AddWithValue("@Category_tbl_code", u.Category_tbl_code);

                cmd.Parameters.AddWithValue("@Size_tbl_code_1", u.Size_tbl_code_1);
                cmd.Parameters.AddWithValue("@Size_tbl_code_2", u.Size_tbl_code_2);

                cmd.Parameters.AddWithValue("@item_updtd_date", u.item_updtd_date);
                cmd.Parameters.AddWithValue("@item_updtd_by", u.item_updtd_by);

                cmd.Parameters.AddWithValue("@item_assembly", u.item_assembly);
                cmd.Parameters.AddWithValue("@item_production", u.item_production);
              
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
        public bool ItemNameUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemName + "=@item_name,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool ItemRemarkUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemRemark + "=@item_remark,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_remark", u.item_remark);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public bool SPPUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemSize1 + "=@Size_tbl_code_1,"
                            + TypeTblCode + "=@Type_tbl_code,"
                            + CategoryTblCode + "=@Category_tbl_code" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@Size_tbl_code_1", u.Size_tbl_code_1);
                cmd.Parameters.AddWithValue("@Type_tbl_code", u.Type_tbl_code);
                cmd.Parameters.AddWithValue("@Category_tbl_code", u.Category_tbl_code);

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

        public bool UpdateFromExcelImport(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemQuoTon + "=@item_quo_ton,"
                            + ItemProTon + "=@item_pro_ton,"
                            + ItemQuoCT + "=@item_quo_ct,"
                            + ItemProCTTo + "=@item_pro_ct_to,"
                            + ItemCavity + "=@item_capacity,"
                            + ItemProPWShot + "=@item_pro_pw_shot,"
                            + ItemProRWShot + "=@item_pro_rw_shot,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                
                cmd.Parameters.AddWithValue("@item_quo_ton", u.item_quo_ton);
                
                cmd.Parameters.AddWithValue("@item_pro_ton", u.item_pro_ton);
                cmd.Parameters.AddWithValue("@item_quo_ct", u.item_quo_ct);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_capacity", u.item_cavity);
               
                cmd.Parameters.AddWithValue("@item_pro_pw_shot", u.item_pro_pw_shot);
                cmd.Parameters.AddWithValue("@item_pro_rw_shot", u.item_pro_rw_shot);
               
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool UpdateItemProductionInfo(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemProCTTo + "=@item_pro_ct_to,"
                            + ItemCavity + "=@item_capacity,"
                            + ItemProPWShot + "=@item_pro_pw_shot,"
                            + ItemProRWShot + "=@item_pro_rw_shot,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_pro_ct_to", u.item_pro_ct_to);
                cmd.Parameters.AddWithValue("@item_capacity", u.item_cavity);

                cmd.Parameters.AddWithValue("@item_pro_pw_shot", u.item_pro_pw_shot);
                cmd.Parameters.AddWithValue("@item_pro_rw_shot", u.item_pro_rw_shot);

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public bool ItemMatUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemMaterial + "=@item_material,"
                            + ItemRawRatio + "=@item_raw_ratio,"
                            + ItemRecycleMat + "=@item_recycle,"
                            + ItemRecycleRatio + "=@item_recycle_ratio,"
                            + ItemMBatch + "=@item_mb,"
                            + ItemMBRate + "=@item_mb_rate,"
                            + ColorFollowRaw + "=@raw_color,"
                            + ColorFollowRecycle + "=@recycle_color,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_material", u.item_material);
                cmd.Parameters.AddWithValue("@item_raw_ratio", u.item_raw_ratio);
                cmd.Parameters.AddWithValue("@item_recycle", u.item_recycle);
                cmd.Parameters.AddWithValue("@item_recycle_ratio", u.item_recycle_ratio);

                cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                cmd.Parameters.AddWithValue("@item_mb_rate", u.item_mb_rate);
                cmd.Parameters.AddWithValue("@raw_color", u.raw_color);
                cmd.Parameters.AddWithValue("@recycle_color", u.recycle_color);

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool rawMatUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemMaterial + "=@item_material,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_material", u.item_material);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool colorMatUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemMBatch + "=@item_mb,"
                            + ItemMBRate + "=@item_mb_rate,"
                            + ItemColor + "=@item_color,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_mb", u.item_mb);
                cmd.Parameters.AddWithValue("@item_mb_rate", u.item_mb_rate);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
            uItem.item_updtd_by = MainDashboard.USER_ID;

            float oldOrderRecord = getOrderQty(itemCode);

            if (oldOrderRecord < 0 || (oldOrderRecord + number) < 0)
            {
                oldOrderRecord = LoadPendingReceiveOrder(itemCode);

            }

            uItem.item_ord = oldOrderRecord + number;

            //Updating data into database
            bool success = ordUpdate(uItem);

            return success;
        }

        public bool orderAdd(string itemCode, float ordQty)
        {
            itemBLL uItem = new itemBLL();

            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID;

            float oldOrderRecord = getOrderQty(itemCode);

            if (oldOrderRecord < 0 || (oldOrderRecord + ordQty) < 0)
            {
                oldOrderRecord = LoadPendingReceiveOrder(itemCode);

            }

            uItem.item_ord = oldOrderRecord + ordQty;



            //Updating data into database
            bool success = ordUpdate(uItem);

            return success;
        }

        public bool orderSubtract(string itemCode, string ordQty)
        {
            bool success = false;
            itemBLL uItem = new itemBLL();
            float number = Convert.ToSingle(ordQty);

            if(number > 0)
            {
                uItem.item_code = itemCode;
                uItem.item_updtd_date = DateTime.Now;
                uItem.item_updtd_by = MainDashboard.USER_ID;

                float oldOrderRecord = getOrderQty(itemCode);

                if (oldOrderRecord < 0 || (oldOrderRecord - number) < 0)
                {
                    oldOrderRecord = LoadPendingReceiveOrder(itemCode);

                }

                uItem.item_ord = oldOrderRecord - number;

                //Updating data into database
                success = ordUpdate(uItem);
            }
           

            return success;

        }

        public bool orderSubtract(string itemCode, float ordQty)
        {
            itemBLL uItem = new itemBLL();
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID;

            float oldOrderRecord = getOrderQty(itemCode);

            if(oldOrderRecord < 0 || (oldOrderRecord - ordQty) < 0)
            {
                oldOrderRecord = LoadPendingReceiveOrder(itemCode);

            }

            uItem.item_ord = oldOrderRecord - ordQty;

            //Updating data into database
            bool success = ordUpdate(uItem);

            return success;

        }

        private float LoadPendingReceiveOrder(string keywords)
        {
            float pendingOrderQty = 0;

            if (keywords != null)
            {
                DataTable dt;

                dt = new ordDAL().PendingOrderSelect(keywords);

                foreach (DataRow ord in dt.Rows)
                {
                    string itemCode = ord["ord_item_code"].ToString();

                    if (itemCode == keywords)
                    {
                        float ordQty = float.TryParse(ord["ord_qty"].ToString(), out ordQty) ? ordQty : 0;
                        float ordReceived = float.TryParse(ord["ord_received"].ToString(), out ordReceived) ? ordReceived : 0;

                        float ordPending = ordQty - ordReceived;

                        ordPending = ordPending < 0 ? 0 : ordPending;

                        pendingOrderQty += ordPending;
                    }
                }
            }
            return pendingOrderQty;
        }

        public bool stockAdd(string itemCode, string stockQty)
        {
            itemBLL uItem = new itemBLL();

            float number = Convert.ToSingle(stockQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID; ;
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
            uItem.item_updtd_by = MainDashboard.USER_ID; ;
            uItem.item_qty = getStockQty(itemCode) - number;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;

        }

        public bool SemenyihClearStockupdateTotalStock(string itemCode, float SMYStock)
        {
            facStockDAL dalStock = new facStockDAL();
            itemBLL uItem = new itemBLL();
            facStockDAL dalFacStock = new facStockDAL();

            //Update data
            uItem.item_code = itemCode;
            uItem.item_qty = SMYStock;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;
        }

        public bool updateTotalStock(string itemCode)
        {
            float totalStock = 0;
            facStockDAL dalStock = new facStockDAL();
            itemBLL uItem = new itemBLL();
            facStockDAL dalFacStock = new facStockDAL();

            DataTable dtStock = dalStock.Select(itemCode);

            foreach (DataRow stock in dtStock.Rows)
            {
                decimal facStock = Convert.ToDecimal(stock["stock_qty"].ToString());

                decimal multiplied = facStock * 10000m;
                decimal integerPart = Math.Truncate(multiplied);

                if(multiplied != integerPart)
                {
                    facStock = Math.Truncate(facStock * 1000) / 1000;

                    if(!dalFacStock.facStockSet(stock["fac_id"].ToString(), itemCode,(float)facStock, stock["stock_unit"].ToString()))
                    {
                        MessageBox.Show("Failed to make decimal error correction, please contact Jun Ong.");
                    }
                }

                totalStock += (float) facStock;
            }

            //Update data
            uItem.item_code = itemCode;
            uItem.item_qty = totalStock;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID; ;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;
        }

        public bool directUpdateFacStock(string itemCode, string facID, decimal newStock)
        {
            float totalStock = 0;
            facStockDAL dalStock = new facStockDAL();
            itemBLL uItem = new itemBLL();
            facStockDAL dalFacStock = new facStockDAL();

            DataTable dtStock = dalStock.Select(itemCode);

            foreach (DataRow stock in dtStock.Rows)
            {
                decimal facStock = Convert.ToDecimal(stock["stock_qty"].ToString());

                string stockLocation = stock["fac_id"].ToString();

                if (stockLocation == facID)
                {
                    newStock = Math.Truncate(newStock * 1000) / 1000;

                    if (!dalFacStock.facStockDirectUpdate(facID, itemCode, (float)newStock, stock["stock_unit"].ToString()))
                    {
                        MessageBox.Show("Failed to update stock, please contact Jun Ong.");
                    }
                }

                totalStock += (float)facStock;
            }

            //Update data
            uItem.item_code = itemCode;
            uItem.item_qty = totalStock;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;
        }

        public bool BackupQty(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemCurrentMonth + "=@item_current_month,"
                            + ItemLastQty + "=@item_last_qty,"
                            + ItemLastPMMAQty + "=@item_last_pmma_qty" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_current_month", u.item_current_month);
                cmd.Parameters.AddWithValue("@item_last_qty", u.item_last_qty);
                cmd.Parameters.AddWithValue("@item_last_pmma_qty", u.item_last_pmma_qty);
               
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

        public bool UpdatePMMAQty(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemLastPMMAQty + "=@item_last_pmma_qty,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by,"
                            + ItemPMMAQty + "=@item_pmma_qty" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_last_pmma_qty", u.item_last_pmma_qty);
                cmd.Parameters.AddWithValue("@item_pmma_qty", u.item_pmma_qty);
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
                Tool tool = new Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool UpdatePermabonnQty(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + ItemLastPERMABONNQty + "=@item_last_permabonn_qty,"
                            + ItemUpdateDate + "=@item_updtd_date,"
                            + ItemUpdateBy + "=@item_updtd_by,"
                            + ItemPERMABONNQty + "=@item_permabonn_qty" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_last_permabonn_qty", u.item_last_permabonn_qty);
                cmd.Parameters.AddWithValue("@item_permabonn_qty", u.item_permabonn_qty);
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
                Tool tool = new Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public bool updateAndHistoryRecord(itemBLL u)
        {
            
            //get old data
            string itemCode = u.item_code;
            string partWeight = null, runnerWeight = null, cavity = null, proCT = null, quoCT = null, quoTon = null, proTon = null;
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[ItemCat].ToString().Equals("Part"))
                    {
                        partWeight = row[ItemProPWShot] == DBNull.Value ? "" : row[ItemProPWShot].ToString();
                        runnerWeight = row[ItemProRWShot] == DBNull.Value ? "" : row[ItemProRWShot].ToString();
                        cavity = row[ItemCavity] == DBNull.Value ? "" : row[ItemCavity].ToString();
                        proCT = row[ItemProCTTo] == DBNull.Value ? "" : row[ItemProCTTo].ToString();
                        quoCT = row[ItemQuoCT] == DBNull.Value ? "" : row[ItemQuoCT].ToString();

                        quoTon = row[ItemQuoTon] == DBNull.Value ? "" : row[ItemQuoTon].ToString();
                        proTon = row[ItemProTon] == DBNull.Value ? "" : row[ItemProTon].ToString();

                    }
                }
            }

            //update
            bool success = UpdateFromExcelImport(u);
            Tool tool = new Tool();
            Text text = new Text();
            if (!success)
            {
                MessageBox.Show("Failed to update part info(893)!");
                tool.historyRecord(text.System, "Failed to update part info(893)!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                #region History Record

                if (!partWeight.Equals(u.item_pro_pw_shot.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Part Weight(pro shot) : " + partWeight + " -->" + u.item_pro_pw_shot, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!runnerWeight.Equals(u.item_pro_rw_shot.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Runner Weight(pro shot) : " + runnerWeight + " -->" + u.item_pro_rw_shot, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!cavity.Equals(u.item_cavity.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Cavity : " + cavity + " -->" + u.item_cavity, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!quoCT.Equals(u.item_quo_ct.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "QUO CT : " + quoCT + " -->" + u.item_quo_ct, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!proCT.Equals(u.item_pro_ct_to.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "PRO CT(MAX) : " + proCT + " -->" + u.item_pro_ct_to, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!quoTon.Equals(u.item_quo_ton.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Quo Ton : " + quoTon + " -->" + u.item_quo_ton, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!proTon.Equals(u.item_pro_ton.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Pro Ton: " + proTon + " -->" + u.item_pro_ton, DateTime.Now, MainDashboard.USER_ID);
                }
                #endregion
            }

            return success;
        }

        public bool updateItemProductionInfoAndHistoryRecord(itemBLL u)
        {

            //get old data
            string itemCode = u.item_code;
            string PWPerShot = null, RWPerShot = null, cavity = null, proCT = null;

            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[ItemCat].ToString().Equals("Part"))
                    {
                        PWPerShot = row[ItemProPWShot] == DBNull.Value ? "" : row[ItemProPWShot].ToString();
                        RWPerShot = row[ItemProRWShot] == DBNull.Value ? "" : row[ItemProRWShot].ToString();
                        cavity = row[ItemCavity] == DBNull.Value ? "" : row[ItemCavity].ToString();
                        proCT = row[ItemProCTTo] == DBNull.Value ? "" : row[ItemProCTTo].ToString();
                    }
                }
            }

            //update
            bool success = UpdateItemProductionInfo(u);
            Tool tool = new Tool();
            Text text = new Text();

            if (!success)
            {
                MessageBox.Show("Failed to update part info!");
                tool.historyRecord(text.System, "Failed to update part info!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                #region History Record

                if (!PWPerShot.Equals(u.item_pro_pw_shot.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Part Weight(pro shot) : " + PWPerShot + " -->" + u.item_pro_pw_shot, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!RWPerShot.Equals(u.item_pro_rw_shot.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Runner Weight(pro shot) : " + RWPerShot + " -->" + u.item_pro_rw_shot, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!cavity.Equals(u.item_cavity.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Cavity : " + cavity + " -->" + u.item_cavity, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!proCT.Equals(u.item_pro_ct_to.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "PRO CT(MAX) : " + proCT + " -->" + u.item_pro_ct_to, DateTime.Now, MainDashboard.USER_ID);
                }
              
                #endregion
            }

            return success;
        }
        public bool rawMatUpdateAndHistoryRecord(itemBLL u)
        {

            
            string itemCode = u.item_code;
            string rawMaterial = null;
            DataTable dt = codeSearch(itemCode);

            //get old data
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[ItemCat].ToString().Equals("Part"))
                    {
                        rawMaterial = row[ItemMaterial] == DBNull.Value ? "" : row[ItemMaterial].ToString();
                    }
                }
            }

            //update
            bool success = rawMatUpdate(u);
            Tool tool = new Tool();
            Text text = new Text();
            if (!success)
            {
                MessageBox.Show("Failed to update raw material(1035)!");
                tool.historyRecord(text.System, "Failed to update raw material(1035)!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                //History Record
                if (!rawMaterial.Equals(u.item_material.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Raw Material : " + rawMaterial + " -->" + u.item_material, DateTime.Now, MainDashboard.USER_ID);
                }
            }

            return success;
        }

        public bool colorMatUpdateAndHistoryRecord(itemBLL u)
        {


            string itemCode = u.item_code;
            string colorMaterial = null, color = null, colorUsage = null;
            DataTable dt = codeSearch(itemCode);

            //get old data
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[ItemCat].ToString().Equals("Part"))
                    {
                        colorMaterial = row[ItemMBatch] == DBNull.Value ? "" : row[ItemMBatch].ToString();
                        color = row[ItemColor] == DBNull.Value ? "" : row[ItemColor].ToString();
                        colorUsage = row[ItemMBRate] == DBNull.Value ? "" : row[ItemMBRate].ToString();
                    }
                }
            }

            //update
            bool success = colorMatUpdate(u);
            Tool tool = new Tool();
            Text text = new Text();
            if (!success)
            {
                MessageBox.Show("Failed to update color material(1078)!");
                tool.historyRecord(text.System, "Failed to update color material(1078)!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                //History Record
                if (!colorMaterial.Equals(u.item_mb.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Color Material : " + colorMaterial + " -->" + u.item_mb, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!color.Equals(u.item_color.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Item Color : " + color + " -->" + u.item_color, DateTime.Now, MainDashboard.USER_ID);
                }

                if (!colorUsage.Equals(u.item_mb_rate.ToString()))
                {
                    tool.historyRecord(text.ItemEdit, u.item_name + "(" + u.item_code + ")_" + "Color Usage : " + colorUsage + " -->" + u.item_mb_rate, DateTime.Now, MainDashboard.USER_ID);
                }
            }

            return success;
        }

        #endregion

        #region Delete data from Database

        public bool ItemMaterialFormulaRemove(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_item_mat_formula WHERE item_code=@item_code";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool MouldItemRemove(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_mould_item 
                            SET "
                            + Removed + "=@removed,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@removed", u.removed);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable IsDuplicateItemCode(string keywords)
        {

            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_code = @keywords ";

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



        public DataTable CatAndItemSearch(string item, string itemCat)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_code LIKE '%" + item + "%'OR item_name LIKE '%" + item + "%' AND item_cat=@category ORDER BY item_name ASC, item_code ASC";

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

        public DataTable SPPSearch()
        {
            string keywords = "CF";

            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE (item_code LIKE '%" + keywords + "%'OR item_name LIKE '%" + keywords + "%') AND (isRemoved = 0 OR isRemoved is NULL)";

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

        public DataTable InOutSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            try
            {
                //sql query to get data from database
                String sql = "SELECT item_cat,item_code, item_name,item_ord,item_qty FROM tbl_item WHERE item_code LIKE '%" + keywords + "%'OR item_name LIKE '%" + keywords + "%'";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable InOutCatItemSearch(string keywords, string category)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT item_cat,item_code, item_name,item_ord,item_qty FROM tbl_item WHERE (item_code LIKE '%" + keywords + "%'OR item_name LIKE '%" + keywords + "%') AND item_cat = @category";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@category", category);
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

        public DataTable InOutCatSBBItemSearch(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string category = new Text().Cat_Part;

                //sql query to get data from database
                String sql = "SELECT item_cat,item_code, item_name,item_ord,item_qty FROM tbl_item WHERE (item_code LIKE '%" + keywords + "%'OR item_name LIKE '%" + keywords + "%') AND item_cat = @category AND category_tbl_code IS NOT NULL";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@category", category);
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

        public DataTable InOutCatSBBItemSearch()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string category = new Text().Cat_Part;

                //sql query to get data from database
                String sql = "SELECT item_cat,item_code, item_name,item_ord,item_qty FROM tbl_item WHERE item_cat = @category AND category_tbl_code IS NOT NULL";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@category", category);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable CatSearch(string keywords)
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable SBBPartSearch()
        {
            //static methodd to connect database

            Text text = new Text();
            string keywords = text.Cat_Part;
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_cat=@category AND category_tbl_code IS NOT NULL";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable SBBPartSearch(string keywords)
        {
            //static methodd to connect database

            Text text = new Text();
            string category = text.Cat_Part;
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_cat=@category AND (item_code LIKE '%" + keywords + "%'OR item_name LIKE '%" + keywords + "%') AND category_tbl_code IS NOT NULL";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@category", category);
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

        public DataTable MatSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                if(keywords.ToUpper().Equals("ALL"))
                {
                    dt = AllMatSearch();
                }
                else
                {
                    //sql query to get data from database
                    String sql = "SELECT * FROM tbl_item WHERE item_cat=@category ORDER BY item_name ASC, item_code ASC";

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
            }
            catch (Exception ex)
            {
                //throw message if any error occurs
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

        public DataTable AllMatSearch()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            string cat = "Part";
            string mould = "Mould";
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_cat != @cat AND item_cat != @mould";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@mould", mould);

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

        public DataTable catInOutSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT item_cat,item_code, item_name,item_ord,item_qty FROM tbl_item WHERE item_cat=@category";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                String sql = "SELECT * FROM tbl_item WHERE item_name =@keywords";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable SPPCodeSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE tbl_item.item_code=@item_code INNER JOIN tbl_spp_category ON tbl_item.item_code = tbl_spp_category.item_code";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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

        public string getCatName(string itemCode)
        {
            string catName = "";
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                catName = dt.Rows[0]["item_cat"].ToString();
            }
            return catName;
        }

        public string getItemName(string itemCode)
        {
            string itemName = "";
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                itemName = dt.Rows[0]["item_name"].ToString();
            }
            return itemName;
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

        public string getMaterialType(string itemCode)
        {
            string materialType = "";
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                materialType = dt.Rows[0][ItemMaterial].ToString();
            }
            return materialType;
        }

        public bool checkIfAssembly(string itemCode)
        {
            bool result = false;
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                if(dt.Rows[0]["item_assembly"] != DBNull.Value)
                {
                    if(Convert.ToInt32(dt.Rows[0]["item_assembly"]) == 1)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool checkIfAssembly(string code, DataTable dt)
        {
            bool result = false;

            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    if (row[ItemCode].ToString() == code)
                    {
                        int assembly = row[ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[ItemAssemblyCheck]);
                        if (assembly == 1)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool checkIfMBOrPigment(string itemCode)
        {
            bool result = false;
            string materialType = "";

            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][ItemCat] != DBNull.Value)
                {
                    materialType = dt.Rows[0][ItemCat].ToString();
                }
            }

            if(materialType.Equals("Master Batch") || materialType.Equals("Pigment"))
            {
                result = true;
            }
            else if(itemCode.Equals("NATURAL") || itemCode.Equals("COMPOUND") || itemCode.Equals("TRANS") )
            {
                result = true;
            }

            return result;
        }

        public bool checkIfRAWMaterial(string itemCode)
        {
            bool result = false;
            string materialType = "";

            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][ItemCat] != DBNull.Value)
                {
                    materialType = dt.Rows[0][ItemCat].ToString();
                }
            }

            if (materialType.Equals("RAW Material"))
            {
                result = true;
            }

            return result;
        }

        public bool checkIfProduction(string itemCode)
        {
            bool result = false;
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["item_production"] != DBNull.Value)
                {
                    if (Convert.ToInt32(dt.Rows[0]["item_production"]) == 1)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool checkIfProduction(string code,DataTable dt)
        {
            bool result = false;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[ItemCode].ToString() == code)
                    {
                        int production = row[ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[ItemProductionCheck]);
                        if (production == 1)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
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

            //ordDAL dalOrd = new ordDAL();
            //Tool tool = new Tool();

            //DataTable DB_PendingOrder = dalOrd.PendingOrderSelect();

            //float pendingOrder_ZeroCost = tool.GetZeroCostPendingOrder(DB_PendingOrder, itemCode);
            //float pendingOrder_Purchase = tool.GetPurchasePendingOrder(DB_PendingOrder, itemCode);

            //float totalOrder = pendingOrder_ZeroCost + pendingOrder_Purchase;


            //if(orderQty != totalOrder)
            //{
            //    itemBLL uItem = new itemBLL();

            //    uItem.item_code = itemCode;
            //    uItem.item_updtd_date = DateTime.Now;
            //    uItem.item_updtd_by = MainDashboard.USER_ID;
            //    uItem.item_ord = totalOrder;

            //    //Updating data into database
            //    ordUpdate(uItem);
            //}

            //orderQty = totalOrder;

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

        public float getPMMAQty(string itemCode)
        {
            float PMMAQty = 0;
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                if (float.TryParse(dt.Rows[0][ItemPMMAQty].ToString(), out float i))
                {
                    PMMAQty += Convert.ToSingle(dt.Rows[0][ItemPMMAQty].ToString());
                }
                
            }

            //MessageBox.Show("get qty= "+qty);


            return PMMAQty;
        }

        public float getLastPMMAQty(string itemCode)
        {
            float LastPMMAQty = 0;
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                if (float.TryParse(dt.Rows[0][ItemLastPMMAQty].ToString(), out float i))
                {
                    LastPMMAQty += Convert.ToSingle(dt.Rows[0][ItemLastPMMAQty].ToString());
                }
            }

            //MessageBox.Show("get qty= "+qty);


            return LastPMMAQty;
        }

        public DateTime getUpdatedDate(string itemCode)
        {
            DateTime date = new DateTime();
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                date = Convert.ToDateTime(dt.Rows[0][ItemUpdateDate].ToString());
               
            }

            //MessageBox.Show("get qty= "+qty);


            return date;
        }

        public int getUpdatedBy(string itemCode)
        {
            int userID = -1;
            DataTable dt = codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                if(int.TryParse(dt.Rows[0][ItemUpdateBy].ToString(), out int i))
                {
                    userID = Convert.ToInt32(dt.Rows[0][ItemUpdateBy].ToString());
                }
            }
            return userID;
        }
        #endregion


    }
}

//String sql = @"INSERT INTO tbl_item 
//                            (" + ItemCat + ","
//                           + ItemCode + ","
//                           + ItemName + ","
//                           + ItemMaterial + ","
//                           + ItemMBatch + ","
//                           + ItemColor + ","
//                           + ItemQuoTon + ","
//                           + ItemBestTon + ","
//                           + ItemProTon + ","
//                           + ItemQuoCT + ","
//                           + ItemProCTFrom + ","
//                           + ItemProCTTo + ","
//                           + ItemCapacity + ","
//                           + ItemQuoPWPcs + ","
//                           + ItemQuoRWPcs + ","
//                           + ItemProPWPcs + ","
//                           + ItemProRWPcs + ","
//                           + ItemProPWShot + ","
//                           + ItemProRWShot + ","
//                           + ItemProCooling + ","
//                           + ItemWastage + ","
//                           + ItemAddDate + ","
//                           + ItemAddBy + ","
//                           + ItemAssemblyCheck + ","
//                           + ItemProductionCheck + ") VALUES " +
//                           "(@item_cat," +
//                           "@item_code," +
//                           "@item_name," +
//                           "@item_material," +
//                           "@item_mb," +
//                           "@item_color," +
//                           "@item_quo_ton," +
//                           "@item_best_ton," +
//                           "@item_pro_ton," +
//                           "@item_quo_ct," +
//                           "@item_pro_ct_from," +
//                           "@item_pro_ct_to," +
//                           "@item_capacity," +
//                           "@item_quo_pw_pcs," +
//                           "@item_quo_rw_pcs," +
//                           "@item_pro_pw_pcs," +
//                           "@item_pro_rw_pcs," +
//                           "@item_pro_pw_shot," +
//                           "@item_pro_rw_shot," +
//                           "@item_pro_cooling," +
//                           "@item_wastage_allowed," +
//                           "@item_added_date," +
//                           "@item_added_by," +
//                           "@item_assembly," +
//                           "@item_production)";
