using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FactoryManagementSoftware.DAL
{
    class SPPDataDAL
    {
        #region data string name getter
        //common table
        public string TableCode { get; } = "tbl_code";
        public string UpdatedDate { get; } = "updated_date";
        public string UpdatedBy { get; } = "updated_by";
        public string IsRemoved { get; } = "isRemoved";

        //item table
        public string ItemCode { get; } = "item_code";
        public string ItemName { get; } = "item_name";
        public string ColorTblCode { get; } = "color_tbl_code";
        public string TonTblCode { get; } = "ton_tbl_code";
        public string ItemCT { get; } = "item_ct";
        public string ItemShotPW { get; } = "item_shot_pw";
        public string ItemShotRW { get; } = "item_shot_rw";
        public string ItemCavity { get; } = "item_cavity";
        public string ItemSize1 { get; } = "size_tbl_code_1";
        public string ItemSize2 { get; } = "size_tbl_code_2";
        public string ItemSize3 { get; } = "size_tbl_code_3";
        public string TypeTblCode { get; } = "type_tbl_code";
        public string CategoryTblCode { get; } = "category_tbl_code";

        //color table
        public string ColorName { get; } = "color_name";

        //Std Packing table
        public string QtyPerBag { get; } = "qty_per_bag";
        public string QtyPerPacket { get; } = "qty_per_packet";

        //ton table
        public string TonNumber { get; } = "ton_number";

        //size table
        public string SizeNumerator { get; } = "size_numerator";
        public string SizeDenominator { get; } = "size_denominator";
        public string SizeUnit { get; } = "size_unit";

        //type table
        public string TypeName { get; } = "type_name";
        public string IsCommon { get; } = "isCommon";

        //category table
        public string CategoryName { get; } = "category_name";

        
       
        #endregion

        #region variable/class object declare

        static readonly string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #endregion

        #region Select Data from Database

        public DataTable SizeSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_size";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
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

        public DataTable TypeSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_type";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
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

        public DataTable CategorySelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_category";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
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

        public DataTable StdPackingSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_stdpacking";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
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

        public DataTable ItemSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                //String sql = @"SELECT * FROM tbl_spp_item";

                String sql = @"SELECT category_tbl_code, type_tbl_code, size_tbl_code_1, item_code, item_name, isRemoved  FROM tbl_item where category_tbl_code is not null";

                //ORDER BY category_tbl_code ASC, type_tbl_code ASC,size_tbl_code_1 ASC, item_code, ASC 

               
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

        public DataTable ColorSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_color";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
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

        public DataTable TonSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_ton";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
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

        public bool InsertSize(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_size 
                            (" + SizeNumerator + ","
                            + SizeDenominator + ","
                            + SizeUnit + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Size_Numerator," +
                            "@Size_Denominator," +
                            "@Size_Unit," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Size_Numerator", u.Size_Numerator);
                cmd.Parameters.AddWithValue("@Size_Denominator", u.Size_Denominator);
                cmd.Parameters.AddWithValue("@Size_Unit", u.Size_Unit);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);


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

        public bool InsertType(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_type 
                            (" + TypeName + ","
                            + IsCommon + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Type_Name," +
                            "@IsCommon," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Type_Name", u.Type_Name);
                cmd.Parameters.AddWithValue("@IsCommon", u.IsCommon);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);


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

        public bool InsertCategory(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_category 
                            (" + CategoryName + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Category_Name," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Category_Name", u.Category_Name);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);


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

        public bool InsertItem(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_item 
                            (" + ItemCode + ","
                            + ItemName + ","
                            + ColorTblCode + ","
                            + TonTblCode + ","
                            + ItemCT + ","
                            + ItemShotPW + ","
                            + ItemShotRW + ","
                            + ItemCavity + ","
                            + ItemSize1 + ","
                            + ItemSize2 + ","
                            + ItemSize3 + ","
                            + TypeTblCode + ","
                            + CategoryTblCode + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Item_code," +
                            "@Item_name," +
                            "@Color_tbl_code," +
                            "@Ton_tbl_code," +
                            "@Item_ct," +
                            "@Item_shot_pw," +
                            "@Item_shot_rw," +
                            "@Item_cavity," +
                            "@Size_tbl_code_1," +
                            "@Size_tbl_code_2," +
                            "@Size_tbl_code_3," +
                            "@Type_tbl_code," +
                            "@Category_tbl_code," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Item_name", u.Item_name);
                cmd.Parameters.AddWithValue("@Color_tbl_code", u.Color_tbl_code);
                cmd.Parameters.AddWithValue("@Ton_tbl_code", u.Ton_tbl_code);
                cmd.Parameters.AddWithValue("@Item_ct", u.Item_ct);
                cmd.Parameters.AddWithValue("@Item_shot_pw", u.Item_shot_pw);
                cmd.Parameters.AddWithValue("@Item_shot_rw", u.Item_shot_rw);
                cmd.Parameters.AddWithValue("@Item_cavity", u.Item_cavity);
                cmd.Parameters.AddWithValue("@Size_tbl_code_1", u.Size_tbl_code_1);
                cmd.Parameters.AddWithValue("@Size_tbl_code_2", u.Size_tbl_code_2);
                cmd.Parameters.AddWithValue("@Size_tbl_code_3", u.Size_tbl_code_3);
                cmd.Parameters.AddWithValue("@Type_tbl_code", u.Type_tbl_code);
                cmd.Parameters.AddWithValue("@Category_tbl_code", u.Category_tbl_code);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);


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

        public bool InsertColor(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_color 
                            (" + ColorName + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Color_name," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Color_name", u.Color_name);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);


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

        public bool InsertTon(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_ton 
                            (" + TonNumber + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Ton_number," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Ton_number", u.Ton_number);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);


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

        public bool InsertStdPacking(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_stdpacking 
                            (" + ItemCode + ","
                            + QtyPerPacket + ","
                             + QtyPerBag + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Item_code," +
                              "@Qty_Per_Packet," +
                                "@Qty_Per_Bag," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Qty_Per_Packet", u.Qty_Per_Packet);
                cmd.Parameters.AddWithValue("@Qty_Per_Bag", u.Qty_Per_Bag);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);


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

        public bool SizeUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_size 
                            SET "
                            + SizeNumerator + "=@Size_Numerator,"
                            + SizeDenominator + "=@Size_Denominator,"
                            + SizeUnit + "=@Size_Unit,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Size_Numerator", u.Size_Numerator);
                cmd.Parameters.AddWithValue("@Size_Denominator", u.Size_Denominator);
                cmd.Parameters.AddWithValue("@Size_Unit", u.Size_Unit);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);

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

        public bool TypeUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_type
                            SET "
                            + TypeName + "=@Type_Name,"
                            + IsCommon + "=@IsCommon,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Type_Name", u.Type_Name);
                cmd.Parameters.AddWithValue("@IsCommon", u.IsCommon);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);

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

        public bool CategoryUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_category 
                            SET "
                            + CategoryName + "=@Category_Name,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Category_Name", u.Category_Name);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);

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

        public bool ItemUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_item 
                            SET "
                            + ItemCode + "=@Item_code,"
                            + ItemName + "=@Item_name,"
                            + ColorTblCode + "=@Color_tbl_code,"
                            + TonTblCode + "=@Ton_tbl_code,"
                            + ItemCT + "=@Item_ct,"
                            + ItemShotPW + "=@Item_shot_pw,"
                            + ItemShotRW + "=@Item_shot_rw,"
                            + ItemCavity + "=@Item_cavity,"
                            + ItemSize1 + "=@Size_tbl_code_1,"
                            + ItemSize2 + "=@Size_tbl_code_2,"
                            + ItemSize3 + "=@Size_tbl_code_3,"
                            + TypeTblCode + "=@Type_tbl_code,"
                            + CategoryTblCode + "=@Category_tbl_code,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Item_name", u.Item_name);
                cmd.Parameters.AddWithValue("@Color_tbl_code", u.Color_tbl_code);
                cmd.Parameters.AddWithValue("@Ton_tbl_code", u.Ton_tbl_code);
                cmd.Parameters.AddWithValue("@Item_ct", u.Item_ct);
                cmd.Parameters.AddWithValue("@Item_shot_pw", u.Item_shot_pw);
                cmd.Parameters.AddWithValue("@Item_shot_rw", u.Item_shot_rw);
                cmd.Parameters.AddWithValue("@Item_cavity", u.Item_cavity);
                cmd.Parameters.AddWithValue("@Size_tbl_code_1", u.Size_tbl_code_1);
                cmd.Parameters.AddWithValue("@Size_tbl_code_2", u.Size_tbl_code_2);
                cmd.Parameters.AddWithValue("@Size_tbl_code_3", u.Size_tbl_code_3);
                cmd.Parameters.AddWithValue("@Type_tbl_code", u.Type_tbl_code);
                cmd.Parameters.AddWithValue("@Category_tbl_code", u.Category_tbl_code);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);

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

        public bool ColorUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_color 
                            SET "
                            + ColorName + "=@Color_name,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Color_name", u.Color_name);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);

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

        public bool TonUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_ton 
                            SET "
                            + TonNumber + "=@Ton_number,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Ton_number", u.Ton_number);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);

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

        public bool StdPackingUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_stdpacking 
                            SET "
                            + QtyPerPacket + "=@Qty_Per_Packet,"
                            + QtyPerBag + "=@Qty_Per_Bag,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Qty_Per_Packet", u.Qty_Per_Packet);
                cmd.Parameters.AddWithValue("@Qty_Per_Bag", u.Qty_Per_Bag);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);

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

        public bool SPPItemUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item 
                            SET "
                            + CategoryTblCode + "=@Category_tbl_code,"
                            + TypeTblCode + "=@Type_tbl_code,"
                            + ItemSize1 + "=@Size_tbl_code_1,"
                            + ItemSize2 + "=@Size_tbl_code_2,"
                            + ItemSize3 + "=@Size_tbl_code_3,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE item_code=@item_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Category_tbl_code", u.Category_tbl_code);
                cmd.Parameters.AddWithValue("@Type_tbl_code", u.Type_tbl_code);
                cmd.Parameters.AddWithValue("@Size_tbl_code_1", u.Size_tbl_code_1);
                cmd.Parameters.AddWithValue("@Size_tbl_code_2", u.Size_tbl_code_2);
                cmd.Parameters.AddWithValue("@Size_tbl_code_3", u.Size_tbl_code_3);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);

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
