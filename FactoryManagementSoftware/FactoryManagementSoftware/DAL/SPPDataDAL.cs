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

        //customer table
        public string FullName { get; } = "full_name";
        public string ShortName { get; } = "short_name";
        public string RegistrationNo { get; } = "registration_no";
        public string Address1 { get; } = "address_1";
        public string Address2 { get; } = "address_2";
        public string AddressCity { get; } = "address_city";
        public string AddressState { get; } = "address_state";
        public string AddressPostalCode { get; } = "address_postal_code";
        public string AddressCountry { get; } = "address_country";
        public string Fax { get; } = "fax";
        public string Phone1 { get; } = "phone_1";
        public string Phone2 { get; } = "phone_2";
        public string Email { get; } = "email";
        public string Website { get; } = "website";

        //PO table
        public string POCode { get; } = "po_code";
        public string PONo { get; } = "po_no";
        public string PODate { get; } = "po_date";
        public string CustomerTableCode { get; } = "customer_tbl_code";
        public string POQty { get; } = "po_qty";
        public string DeliveredQty { get; } = "delivered_qty";
        public string DefaultShippingAddress { get; } = "defaultShippingAddress";
        public string PONote { get; } = "po_note";
        public string ToDeliveryQty { get; } = "to_delivery_qty";

        //DO table
        public string DONo { get; } = "do_no";
        public string POTableCode { get; } = "po_tbl_code";
        public string DOToDeliveryQty { get; } = "to_delivery_qty";
        public string DODate { get; } = "do_date";
        
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

        public DataTable SizeForReadyGoodsSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_size WHERE isRemoved IS NULL OR isRemoved = @false";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
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

        public DataTable TypeWithoutRemovedDataSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_type WHERE isRemoved IS NULL OR isRemoved = @false ";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
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

        public DataTable TypeForReadyGoodsSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_type WHERE (isRemoved IS NULL OR isRemoved = @false) AND (isCommon IS NULL OR isCommon = @false)";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
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

        public DataTable CustomerSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_customer";

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

        public DataTable CustomerWithoutRemovedDataSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_customer WHERE isRemoved IS NULL OR isRemoved = @false ";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
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

        public DataTable POSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_po INNER JOIN tbl_spp_customer ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code ORDER BY po_code ASC";

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

        public DataTable POSelectWithSizeAndType()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_po 
                               INNER JOIN tbl_spp_customer 
                               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                               INNER JOIN tbl_item
                               ON tbl_spp_po.item_code = tbl_item.item_code
                               INNER JOIN tbl_spp_size
                               ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code
                               INNER JOIN tbl_spp_type
                               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                               ORDER BY tbl_spp_po.po_code ASC";

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

        public DataTable POWithoutRemovedDataSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_po WHERE isRemoved IS NULL OR isRemoved = @false ";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
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

        public DataTable DOSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_do 
                             ORDER BY do_no DESC";

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

        public DataTable DOSelectWithPO()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_do 
                             INNER JOIN tbl_spp_po
                             ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                             INNER JOIN tbl_spp_customer 
                             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                             ORDER BY do_no ASC";

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

        public bool InsertCustomer(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_customer 
                            (" + FullName + ","
                            + ShortName + ","
                            + RegistrationNo + ","
                            + Address1 + ","
                            + Address2 + ","
                            + AddressCity + ","
                            + AddressState + ","
                            + AddressPostalCode + ","
                            + AddressCountry + ","
                            + Fax + ","
                            + Phone1 + ","
                            + Phone2 + ","
                            + Email + ","
                            + Website + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Full_Name," +
                            "@Short_Name," +
                            "@Registration_No," +
                            "@Address_1," +
                            "@Address_2," +
                            "@Address_City," +
                            "@Address_State," +
                            "@Address_Postal_Code," +
                            "@Address_Country," +
                            "@Fax," +
                            "@Phone_1," +
                            "@Phone_2," +
                            "@Email," +
                            "@Website," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Full_Name", u.Full_Name);
                cmd.Parameters.AddWithValue("@Short_Name", u.Short_Name);
                cmd.Parameters.AddWithValue("@Registration_No", u.Registration_No);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);
                cmd.Parameters.AddWithValue("@Fax", u.Fax);
                cmd.Parameters.AddWithValue("@Phone_1", u.Phone_1);
                cmd.Parameters.AddWithValue("@Phone_2", u.Phone_2);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Website", u.Website);

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

        public bool InsertPO(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_po 
                            (" + POCode + ","
                            + CustomerTableCode + ","
                            + PONo + ","
                            + ItemCode + ","
                            + POQty + ","
                            + DeliveredQty + ","
                            + Address1 + ","
                            + Address2 + ","
                            + AddressCity + ","
                            + AddressState + ","
                            + AddressPostalCode + ","
                            + AddressCountry + ","
                            + DefaultShippingAddress + ","
                             + PONote + ","
                            + PODate + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@PO_code," +
                            "@Customer_tbl_code," +
                            "@PO_no," +
                            "@Item_code," +
                            "@PO_qty," +
                            "@Delivered_qty," +
                            "@Address_1," +
                            "@Address_2," +
                            "@Address_City," +
                            "@Address_State," +
                            "@Address_Postal_Code," +
                            "@Address_Country," +
                            "@DefaultShippingAddress," +
                             "@PO_note," +
                            "@PO_date," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PO_code", u.PO_code);
                cmd.Parameters.AddWithValue("@PO_no", u.PO_no);
                cmd.Parameters.AddWithValue("@PO_date", u.PO_date);
                cmd.Parameters.AddWithValue("@PO_note", u.PO_note);
                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);
                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@PO_qty", u.PO_qty);
                cmd.Parameters.AddWithValue("@Delivered_qty", u.Delivered_qty);
                cmd.Parameters.AddWithValue("@DefaultShippingAddress", u.DefaultShippingAddress);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);

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

        public bool InsertDO(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_do 
                            (" + DONo + ","
                            + POTableCode + ","
                            + DOToDeliveryQty + ","
                            + DODate + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@DO_no," +
                            "@PO_tbl_code," +
                            "@DO_to_delivery_qty," +
                            "@DO_date," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

           
                cmd.Parameters.AddWithValue("@DO_no", u.DO_no);
                cmd.Parameters.AddWithValue("@PO_tbl_code", u.PO_tbl_code);
                cmd.Parameters.AddWithValue("@DO_to_delivery_qty", u.DO_to_delivery_qty);
                cmd.Parameters.AddWithValue("@DO_date", u.DO_date);
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

        public bool CustomerUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_customer 
                            SET "
                            + FullName + "=@Full_Name,"
                            + ShortName + "=@Short_Name,"
                            + RegistrationNo + "=@Registration_No,"
                            + Address1 + "=@Address_1,"
                            + Address2 + "=@Address_2,"
                            + AddressCity + "=@Address_City,"
                            + AddressState + "=@Address_State,"
                            + AddressPostalCode + "=@Address_Postal_Code,"
                            + AddressCountry + "=@Address_Country,"
                            + Fax + "=@Fax,"
                            + Phone1 + "=@Phone_1,"
                            + Phone2 + "=@Phone_2,"
                            + Email + "=@Email,"
                            + Website + "=@Website,"
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Full_Name", u.Full_Name);
                cmd.Parameters.AddWithValue("@Short_Name", u.Short_Name);
                cmd.Parameters.AddWithValue("@Registration_No", u.Registration_No);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);
                cmd.Parameters.AddWithValue("@Fax", u.Fax);
                cmd.Parameters.AddWithValue("@Phone_1", u.Phone_1);
                cmd.Parameters.AddWithValue("@Phone_2", u.Phone_2);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Website", u.Website);

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

        public bool CustomerRemove(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_customer 
                            SET "
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

              
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

        public bool POUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + POCode + "=@PO_code,"
                            + CustomerTableCode + "=@Customer_tbl_code,"
                            + PONo + "=@PO_no,"
                            + ItemCode + "=@Item_code,"
                            + POQty + "=@PO_qty,"
                            + DefaultShippingAddress + "=@DefaultShippingAddress,"
                            + Address1 + "=@Address_1,"
                            + Address2 + "=@Address_2,"
                            + AddressCity + "=@Address_City,"
                            + AddressState + "=@Address_State,"
                            + AddressPostalCode + "=@Address_Postal_Code,"
                            + AddressCountry + "=@Address_Country,"
                            + PONote + "=@PO_note,"
                            + PODate + "=@PO_date,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PO_code", u.PO_code);
                cmd.Parameters.AddWithValue("@PO_no", u.PO_no);
                cmd.Parameters.AddWithValue("@PO_note", u.PO_note);
                cmd.Parameters.AddWithValue("@PO_date", u.PO_date);
                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);
                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@PO_qty", u.PO_qty);
                cmd.Parameters.AddWithValue("@DefaultShippingAddress", u.DefaultShippingAddress);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);

                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool PORemove(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);


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

        public bool POToDeliveryDataUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + DeliveredQty + "=@Delivered_qty,"
                            + ToDeliveryQty + "=@To_delivery_qty,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@Delivered_qty", u.Delivered_qty);
                cmd.Parameters.AddWithValue("@To_delivery_qty", u.To_delivery_qty);
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

        public bool PODelete(SPPDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_spp_po WHERE po_code =@PO_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PO_code", u.PO_code);

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

        public bool DOUpdate(SPPDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_do 
                            SET "
                            + DONo + "=@DO_no,"
                            + POTableCode + "=@PO_tbl_code,"
                            + DOToDeliveryQty + "=@DO_to_delivery_qty,"
                            + DODate + "=@DO_date,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@DO_no", u.DO_no);
                cmd.Parameters.AddWithValue("@PO_tbl_code", u.PO_tbl_code);
                cmd.Parameters.AddWithValue("@DO_to_delivery_qty", u.DO_to_delivery_qty);
                cmd.Parameters.AddWithValue("@DO_date", u.DO_date);

                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);
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
