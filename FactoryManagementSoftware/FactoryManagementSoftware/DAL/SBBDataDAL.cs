using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class SBBDataDAL
    {
        #region data string name getter
        //common table
        public string TableCode { get; } = "tbl_code";
        public string UpdatedDate { get; } = "updated_date";
        public string UpdatedBy { get; } = "updated_by";
        public string IsRemoved { get; } = "isRemoved";
        public string Freeze { get; } = "freeze";
        public string PriorityLevel { get; } = "priority_level";
        public string ItemPriorityLevel { get; } = "item_priority_level";

        //mould table
        public string MouldTblCode { get; } = "mould_tbl_code";
        public string MouldCode { get; } = "mould_code";
        public string MouldName { get; } = "mould_name";
        public string MouldTon { get; } = "mould_ton";
        public string MouldWidth { get; } = "mould_width";
        public string MouldHeight { get; } = "mould_height";
        public string MouldLength { get; } = "mould_length";
        public string MouldDateStart { get; } = "mould_date_start";
        public string GroupCode { get; } = "group_code";
        public string MouldCycleTime { get; } = "mould_cycle_time";
        public string PWPerShot { get; } = "pw_per_shot";
        public string RWPerShot { get; } = "rw_per_shot";
        public string MouldItemCavity { get; } = "item_cavity";
        public string MouldTotalCavity { get; } = "mould_cavity";

        //delivery table
        public string TripNo { get; } = "trip_no";
        public string RouteTblCode { get; } = "route_tbl_code";
        public string DeliveryDate { get; } = "delivery_date";
        public string DeliveryStatus { get; } = "delivery_status";
        public string DeliverPcs { get; } = "deliver_pcs";
        public string DOTableCode { get; } = "do_tbl_code";
        public string PlanningNo { get; } = "planning_no";

        //route table
        public string RouteName { get; } = "route_name";
     
        //item table
        public string ItemCode { get; } = "item_code";
        public string ItemTblCode { get; } = "item_tbl_code";
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
        public string QtyPerContainer { get; } = "qty_per_container";
        public string QtyPerBag { get; } = "qty_per_bag";
        public string QtyPerPacket { get; } = "qty_per_packet";
        public string MaxLevel { get; } = "max_level";

        //ton table
        public string TonNumber { get; } = "ton_number";

        //size table
        public string SizeNumerator { get; } = "size_numerator";
        public string SizeDenominator { get; } = "size_denominator";
        public string SizeUnit { get; } = "size_unit";
        public string SizeWeight { get; } = "size_weight";

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
        public string DiscountAdjust { get; } = "discount_adjust";
        public string AddressCountry { get; } = "address_country";
        public string Fax { get; } = "fax";
        public string Phone1 { get; } = "phone_1";
        public string Phone2 { get; } = "phone_2";
        public string Email { get; } = "email";
        public string Website { get; } = "website";

        public string Address3 { get; } = "address_3";
        public string ShippingSameAsBilling { get; } = "ShippingSameAsBilling";
        public string ShippingFullName { get; } = "shipping_fullname";
        public string ShippingShortName { get; } = "shipping_shortname";
        public string ShippingTransporter { get; } = "shipping_transporter";

        //PO table
        public string POTableName { get; } = "tbl_spp_po";
        public string POCode { get; } = "po_code";
        public string PONo { get; } = "po_no";
        public string PODate { get; } = "po_date";
        public string CustTblCode { get; } = "customer_tbl_code";
        public string POQty { get; } = "po_qty";
        public string DeliveredQty { get; } = "delivered_qty";
        public string DefaultShippingAddress { get; } = "defaultShippingAddress";
        public string PONote { get; } = "po_note";
        public string ToDeliveryQty { get; } = "to_delivery_qty";
        public string CustOwnDO { get; } = "cust_own_do";
        public string RemarkInDO { get; } = "remark_in_do";
        public string TargetDeliveryDate { get; } = "target_delivery_date";
        public string ItemTargetDeliveryDate { get; } = "item_target_delivery_date";

        //DO table
        public string DOTableName { get; } = "tbl_spp_do";
        public string DONo { get; } = "do_no";
        public string POTableCode { get; } = "po_tbl_code";
        public string DOToDeliveryQty { get; } = "to_delivery_qty";
        public string DODate { get; } = "do_date";
        public string IsDelivered { get; } = "isDelivered";
        public string TrfTableCode { get; } = "trf_tbl_code";

        //Plan table
        public string LocationArea { get; } = "location_area";
        public string LocationLine { get; } = "location_line";
        public string DateStart { get; } = "date_start";
        public string DateEnd { get; } = "date_end";
        public string TargetQty { get; } = "target_qty";
        public string MaxQty { get; } = "max_qty";
        public string PlanStatus { get; } = "plan_status";
        public string PlanType { get; } = "plan_type";
        public string PlanNote { get; } = "plan_note";

        //Material Plan table
        public string PlanCode { get; } = "plan_code";
        public string RequiredQty { get; } = "required_qty";
        public string PreparingQty { get; } = "preparing_qty";
        public string Note { get; } = "note";
        public string IsCompleted { get; } = "isCompleted";

        //Material Plan Prepare table
        public string MatPlanCode { get; } = "mat_plan_code";
        public string StdPacking { get; } = "std_packing";
        public string DeliveryBag { get; } = "delivery_bag";
        public string LocationFrom { get; } = "location_from";

        //Price
        public string DefaultPrice { get; } = "default_price";
        public string UnitPrice { get; } = "unit_price";
        public string DefaultDiscount { get; } = "default_discount";
        public string DiscountRate { get; } = "discount_rate";
        public string SubTotal { get; } = "sub_total";

        #endregion

        #region variable/class object declare

        static readonly string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #endregion

        #region Select Data from Database

        public DataTable DiscountSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_discount";

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

        public DataTable PriceSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_price";

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

        public DataTable PlanSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_plan";

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

        public DataTable MouldSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try

            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_mould";

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

        public DataTable MouldWithoudRemovedDataSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try

            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_mould WHERE isRemoved IS NULL OR isRemoved = @false";

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

        public DataTable MouldKeyWordsSelect(string KeyWords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try

            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_mould WHERE mould_name =@keywords";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keywords", KeyWords);

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

        public DataTable MouldWithLinkedItemSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_mould
                             LEFT JOIN tbl_sbb_mould_item
                             ON tbl_sbb_mould_item.mould_code = tbl_sbb_mould.mould_code 
                             ORDER BY tbl_sbb_mould.mould_code ASC";

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

        public DataTable MatPlanSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_mat_plan";

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

        public DataTable MatPreparePlanSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_mat_prepare";

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

        public DataTable RouteSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_route";

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

        public DataTable RouteWithoutRemovedDataSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_route WHERE isRemoved IS NULL OR isRemoved = @false ";

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
                String sql = @"SELECT * FROM tbl_spp_stdpacking WHERE item_code <> '' AND item_code IS NOT NULL ORDER BY item_code ASC";


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

                String sql = @"SELECT category_tbl_code, type_tbl_code, size_tbl_code_1, size_tbl_code_2, type_tbl_code, item_code, item_name, isRemoved  FROM tbl_item where category_tbl_code is not null";

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
                //String sql = @"SELECT * FROM tbl_spp_po INNER JOIN tbl_spp_customer ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code ORDER BY po_date ASC";


                //String sql = @"SELECT * FROM tbl_spp_po 
                //               INNER JOIN tbl_spp_customer 
                //               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                //               WHERE tbl_spp_po.po_date >= DATEADD(year, -1, GETDATE())
                //               ORDER BY tbl_spp_po.po_date ASC";

                String sql = @"SELECT * FROM tbl_spp_po 
                               INNER JOIN tbl_spp_customer 
                               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                               WHERE tbl_spp_po.po_date >= DATEADD(month, -3, GETDATE())
                               ORDER BY tbl_spp_po.po_date ASC";


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

        public DataTable ActivePOSelect()
        {
            //static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"
            SELECT 
                *
            FROM 
                tbl_spp_po 
            INNER JOIN 
                tbl_spp_customer 
                ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code
            WHERE 
                tbl_spp_po.po_code IN (
                    SELECT 
                        po_code
                    FROM 
                        tbl_spp_po
                    GROUP BY 
                        po_code
                    HAVING 
                        SUM(CASE WHEN delivered_qty < po_qty THEN 1 ELSE 0 END) > 0
                )
            ORDER BY 
                tbl_spp_po.po_date ASC";

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

   
        public DataTable SelectActivePOPastYear()
        {
            // Static method to connect to the database
            SqlConnection conn = new SqlConnection(myconnstrng);
            // To hold the data from the database
            DataTable dt = new DataTable();
            try
            {
                // SQL query to get data from the database
                String sql = @"
                SELECT 
                    *
                FROM 
                    tbl_spp_po 
                    INNER JOIN 
                    tbl_spp_customer 
                    ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code
                WHERE 
                    tbl_spp_po.po_code IN (
                        SELECT 
                            po_code
                        FROM 
                            tbl_spp_po
                        GROUP BY 
                            po_code
                        HAVING 
                            SUM(CASE WHEN delivered_qty < po_qty THEN 1 ELSE 0 END) > 0
                    )
                AND tbl_spp_po.po_date >= DATEADD(year, -1, GETDATE())
                ORDER BY 
                tbl_spp_po.po_date ASC";

                // For executing the command
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Getting data from the database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Open the database connection
                conn.Open();
                // Fill data into our DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
            return dt;
        }


        public DataTable POSelect(string poNoString)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_po INNER JOIN tbl_spp_customer ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code WHERE po_no=@poNoString ORDER BY po_code ASC";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@poNoString", poNoString);

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

        public DataTable POSelectWithCustCode(string custTblCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_po WHERE customer_tbl_code=@custTblCode ORDER BY updated_date DESC";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@custTblCode", custTblCode);

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
                ////sql query to get data from database
                //String sql = @"SELECT * FROM tbl_spp_po 
                //               INNER JOIN tbl_spp_customer 
                //               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                //               INNER JOIN tbl_item
                //               ON tbl_spp_po.item_code = tbl_item.item_code
                //               LEFT JOIN tbl_spp_size SIZE1
                //               ON tbl_item.size_tbl_code_1 = SIZE1.tbl_code
                //               LEFT JOIN tbl_spp_size SIZE2
                //               ON tbl_item.size_tbl_code_2 = SIZE2.tbl_code
                //               INNER JOIN tbl_spp_type
                //               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                //               LEFT JOIN tbl_spp_stdpacking
                //               ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                //               ORDER BY tbl_spp_po.po_code ASC, tbl_spp_po.item_code ASC";

                String sql = @"SELECT * FROM tbl_spp_po 
                               INNER JOIN tbl_spp_customer 
                               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                               INNER JOIN tbl_item
                               ON tbl_spp_po.item_code = tbl_item.item_code
                               LEFT JOIN tbl_spp_size SIZE1
                               ON tbl_item.size_tbl_code_1 = SIZE1.tbl_code
                               LEFT JOIN tbl_spp_size SIZE2
                               ON tbl_item.size_tbl_code_2 = SIZE2.tbl_code
                               INNER JOIN tbl_spp_type
                               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                               LEFT JOIN tbl_spp_stdpacking
                               ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                               WHERE tbl_spp_po.po_date >= DATEADD(month, -2, GETDATE())
                               ORDER BY tbl_spp_po.po_code ASC, tbl_spp_po.item_code ASC";

                //INNER JOIN tbl_spp_customer
                //              ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code
                //               INNER JOIN tbl_item
                //               ON tbl_spp_po.item_code = tbl_item.item_code
                //               LEFT JOIN tbl_spp_size SIZE1
                //               ON tbl_item.size_tbl_code_1 = SIZE1.tbl_code
                //               LEFT JOIN tbl_spp_size SIZE2
                //               ON tbl_item.size_tbl_code_2 = SIZE2.tbl_code
                //               INNER JOIN tbl_spp_type
                //               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                //               LEFT JOIN tbl_spp_stdpacking
                //               ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                //               ORDER BY tbl_spp_po.po_code ASC, tbl_spp_po.item_code ASC";

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

        public DataTable SBBPagePOSelectWithSizeAndType()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                //String sql = @"SELECT
                //               tbl_spp_po.isRemoved,
                //               tbl_spp_po.po_code,
                //               tbl_spp_po.po_qty,
                //               tbl_spp_po.delivered_qty,
                //               tbl_spp_po.po_no,
                //               tbl_spp_po.po_date,
                //               tbl_spp_po.customer_tbl_code,
                //               tbl_spp_stdpacking.qty_per_bag,
                //               tbl_spp_stdpacking.qty_per_packet,
                //               tbl_spp_customer.short_name,
                //               tbl_spp_customer.full_name
                //               FROM tbl_spp_po 
                //               INNER JOIN tbl_spp_customer 
                //               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                //               INNER JOIN tbl_item
                //               ON tbl_spp_po.item_code = tbl_item.item_code
                //               LEFT JOIN tbl_spp_size SIZE1
                //               ON tbl_item.size_tbl_code_1 = SIZE1.tbl_code
                //               LEFT JOIN tbl_spp_size SIZE2
                //               ON tbl_item.size_tbl_code_2 = SIZE2.tbl_code
                //               INNER JOIN tbl_spp_type
                //               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                //               LEFT JOIN tbl_spp_stdpacking
                //               ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                //               ORDER BY tbl_spp_po.po_code ASC, tbl_spp_po.item_code ASC";

                String sql = @"SELECT
                               tbl_spp_po.isRemoved,
                               tbl_spp_po.po_code,
                               tbl_spp_po.po_qty,
                               tbl_spp_po.delivered_qty,
                               tbl_spp_po.po_no,
                               tbl_spp_po.po_date,
                               tbl_spp_po.customer_tbl_code,
                               tbl_spp_stdpacking.qty_per_bag,
                               tbl_spp_stdpacking.qty_per_packet,
                               tbl_spp_customer.short_name,
                               tbl_spp_customer.full_name
                               FROM tbl_spp_po 
                               INNER JOIN tbl_spp_customer 
                               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                               INNER JOIN tbl_item
                               ON tbl_spp_po.item_code = tbl_item.item_code
                               LEFT JOIN tbl_spp_size SIZE1
                               ON tbl_item.size_tbl_code_1 = SIZE1.tbl_code
                               LEFT JOIN tbl_spp_size SIZE2
                               ON tbl_item.size_tbl_code_2 = SIZE2.tbl_code
                               INNER JOIN tbl_spp_type
                               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                               LEFT JOIN tbl_spp_stdpacking
                               ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                               WHERE tbl_spp_po.po_date >= DATEADD(month, -3, GETDATE())
                               ORDER BY tbl_spp_po.po_code ASC, tbl_spp_po.item_code ASC";


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

        public DataTable PendingPOSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                //String sql = @"SELECT * FROM tbl_spp_po INNER JOIN tbl_item 
                //               ON tbl_spp_po.item_code = tbl_item.item_code 
                //                WHERE (tbl_spp_po.isRemoved IS NULL OR tbl_spp_po.isRemoved = @false) AND tbl_spp_po.po_qty > tbl_spp_po.delivered_qty 
                //                ORDER BY tbl_spp_po.item_code ASC";

                String sql = @"SELECT * FROM tbl_spp_po 
                               INNER JOIN tbl_spp_customer 
                               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                               INNER JOIN tbl_item
                               ON tbl_spp_po.item_code = tbl_item.item_code
                               LEFT JOIN tbl_spp_size SIZE1
                               ON tbl_item.size_tbl_code_1 = SIZE1.tbl_code
                               LEFT JOIN tbl_spp_size SIZE2
                               ON tbl_item.size_tbl_code_2 = SIZE2.tbl_code
                               INNER JOIN tbl_spp_type
                               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                               LEFT JOIN tbl_spp_stdpacking
                               ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                               WHERE (tbl_spp_po.isRemoved IS NULL OR tbl_spp_po.isRemoved = @false) AND tbl_spp_po.po_qty > tbl_spp_po.delivered_qty 
                               ORDER BY tbl_spp_po.item_code ASC";

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

        public DataTable SBBPagePendingPOSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                //String sql = @"SELECT * FROM tbl_spp_po INNER JOIN tbl_item 
                //               ON tbl_spp_po.item_code = tbl_item.item_code 
                //                WHERE (tbl_spp_po.isRemoved IS NULL OR tbl_spp_po.isRemoved = @false) AND tbl_spp_po.po_qty > tbl_spp_po.delivered_qty 
                //                ORDER BY tbl_spp_po.item_code ASC";

                String sql = @"SELECT 
                               tbl_item.item_code,
                               tbl_item.item_name,
                               tbl_item.item_qty,
                               tbl_spp_po.po_qty,
                               tbl_spp_po.delivered_qty,
                               tbl_spp_stdpacking.qty_per_bag,
                               tbl_spp_po.po_date
                               FROM tbl_spp_po 
                               INNER JOIN tbl_spp_customer 
                               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                               INNER JOIN tbl_item
                               ON tbl_spp_po.item_code = tbl_item.item_code
                               LEFT JOIN tbl_spp_size SIZE1
                               ON tbl_item.size_tbl_code_1 = SIZE1.tbl_code
                               LEFT JOIN tbl_spp_size SIZE2
                               ON tbl_item.size_tbl_code_2 = SIZE2.tbl_code
                               INNER JOIN tbl_spp_type
                               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                               LEFT JOIN tbl_spp_stdpacking
                               ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                               WHERE tbl_spp_po.po_qty > tbl_spp_po.delivered_qty 
                               ORDER BY tbl_spp_po.item_code ASC";

                //(tbl_spp_po.isRemoved IS NOT NULL OR tbl_spp_po.isRemoved = @false) AND 
                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
                cmd.Parameters.AddWithValue("@StartDate", DateTime.Now.AddYears(-1));
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

        public DataTable DOSelect(string doNo)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_spp_do 
                             WHERE do_no = @doNo
                             ORDER BY do_no DESC";
                

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@doNo", doNo);
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

        public DataTable DOWithInfoSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                //String sql = @"SELECT * FROM tbl_spp_do 
                //             INNER JOIN tbl_spp_po
                //             ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                //             INNER JOIN tbl_spp_customer 
                //             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                //             INNER JOIN tbl_item
                //             ON tbl_spp_po.item_code = tbl_item.item_code
                //             LEFT JOIN tbl_spp_size size1
                //                ON tbl_item.size_tbl_code_1 = size1.tbl_code
                //             LEFT JOIN tbl_spp_size size2
                //                ON tbl_item.size_tbl_code_2 = size2.tbl_code
                //             INNER JOIN tbl_spp_type
                //             ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                //             FULL JOIN tbl_spp_stdpacking
                //             ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                //             ORDER BY tbl_spp_do.do_no ASC";
                String sql = @"SELECT * FROM tbl_spp_do 
                               INNER JOIN tbl_spp_po
                               ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                               INNER JOIN tbl_spp_customer 
                               ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                               INNER JOIN tbl_item
                               ON tbl_spp_po.item_code = tbl_item.item_code
                               LEFT JOIN tbl_spp_size size1
                               ON tbl_item.size_tbl_code_1 = size1.tbl_code
                               LEFT JOIN tbl_spp_size size2
                               ON tbl_item.size_tbl_code_2 = size2.tbl_code
                               INNER JOIN tbl_spp_type
                               ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                               FULL JOIN tbl_spp_stdpacking
                               ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                               WHERE tbl_spp_do.do_date >= DATEADD(month, -2, GETDATE())
                               ORDER BY tbl_spp_do.do_no ASC";


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

        public DataTable DOWithInfoSelectByMonth(int monthsAgo)
        {
            // Static method to connect to the database
            SqlConnection conn = new SqlConnection(myconnstrng);
            // DataTable to hold the data from the database
            DataTable dt = new DataTable();
            try
            {
                // Calculate the start date (first day of the month, monthsAgo months ago)
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-monthsAgo + 1);
                // Calculate the end date (last day of the current month)
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

                // SQL query to get data from the database within the specific date range
                String sql = @"SELECT * FROM tbl_spp_do 
                        INNER JOIN tbl_spp_po ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                        INNER JOIN tbl_spp_customer ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                        INNER JOIN tbl_item ON tbl_spp_po.item_code = tbl_item.item_code
                        LEFT JOIN tbl_spp_size size1 ON tbl_item.size_tbl_code_1 = size1.tbl_code
                        LEFT JOIN tbl_spp_size size2 ON tbl_item.size_tbl_code_2 = size2.tbl_code
                        INNER JOIN tbl_spp_type ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                        FULL JOIN tbl_spp_stdpacking ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                        WHERE CONVERT(date, tbl_spp_do.do_date) BETWEEN @StartDate AND @EndDate
                        ORDER BY tbl_spp_do.do_no ASC";

                // Command for executing the query
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                // Adapter to handle the data from the database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Open database connection
                conn.Open();
                // Fill data into the DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                // Ensure the connection is closed
                conn.Close();
            }
            return dt;
        }

        public DataTable CompletedDOWithInfoSelectByMonth(int monthsAgo)
        {
            // Static method to connect to the database
            SqlConnection conn = new SqlConnection(myconnstrng);
            // DataTable to hold the data from the database
            DataTable dt = new DataTable();
            try
            {
                // Calculate the start date (first day of the month, monthsAgo months ago)
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-monthsAgo + 1);
                // Calculate the end date (last day of the current month)
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

                // SQL query to get data from the database within the specific date range
                String sql = @"SELECT * FROM tbl_spp_do 
                        INNER JOIN tbl_spp_po ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                        INNER JOIN tbl_trf_hist ON tbl_spp_do.trf_tbl_code = tbl_trf_hist.trf_hist_id 
                        INNER JOIN tbl_spp_customer ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                        INNER JOIN tbl_item ON tbl_spp_po.item_code = tbl_item.item_code
                        LEFT JOIN tbl_spp_size size1 ON tbl_item.size_tbl_code_1 = size1.tbl_code
                        LEFT JOIN tbl_spp_size size2 ON tbl_item.size_tbl_code_2 = size2.tbl_code
                        INNER JOIN tbl_spp_type ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                        FULL JOIN tbl_spp_stdpacking ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                        WHERE CONVERT(date, tbl_spp_do.do_date) BETWEEN @StartDate AND @EndDate
                        ORDER BY tbl_spp_do.do_no ASC";

                // Command for executing the query
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                // Adapter to handle the data from the database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Open database connection
                conn.Open();
                // Fill data into the DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                // Ensure the connection is closed
                conn.Close();
            }
            return dt;
        }




        public DataTable ActiveDOWithInfoSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                String sql = @"SELECT * FROM tbl_spp_do 
                         INNER JOIN tbl_spp_po
                         ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                         INNER JOIN tbl_spp_customer 
                         ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                         INNER JOIN tbl_item
                         ON tbl_spp_po.item_code = tbl_item.item_code
                         LEFT JOIN tbl_spp_size size1
                            ON tbl_item.size_tbl_code_1 = size1.tbl_code
                         LEFT JOIN tbl_spp_size size2
                            ON tbl_item.size_tbl_code_2 = size2.tbl_code
                         INNER JOIN tbl_spp_type
                         ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                         FULL JOIN tbl_spp_stdpacking
                         ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                         WHERE (tbl_spp_do.isRemoved = @isRemoved OR tbl_spp_do.isRemoved IS NULL)
                     AND (tbl_spp_do.isDelivered = @isDelivered OR tbl_spp_do.isDelivered IS NULL)
                     ORDER BY tbl_spp_do.do_no ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@isRemoved", false);
                cmd.Parameters.AddWithValue("@isDelivered", false);

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
        public DataTable SBBPageDOWithInfoSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                ////sql query to get data from database
                //String sql = @"SELECT 
                //             tbl_spp_do.do_no,
                //             tbl_spp_do.to_delivery_qty,
                //             tbl_spp_do.isRemoved,
                //             tbl_spp_do.isDelivered,
                //             tbl_spp_do.trf_tbl_code,
                //             tbl_spp_stdpacking.qty_per_bag,
                //             tbl_spp_po.customer_tbl_code
                //             FROM tbl_spp_do 
                //             INNER JOIN tbl_spp_po
                //             ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                //             INNER JOIN tbl_spp_customer 
                //             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                //             INNER JOIN tbl_item
                //             ON tbl_spp_po.item_code = tbl_item.item_code
                //             LEFT JOIN tbl_spp_size size1
                //                ON tbl_item.size_tbl_code_1 = size1.tbl_code
                //             LEFT JOIN tbl_spp_size size2
                //                ON tbl_item.size_tbl_code_2 = size2.tbl_code
                //             INNER JOIN tbl_spp_type
                //             ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                //             FULL JOIN tbl_spp_stdpacking
                //             ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                //             WHERE tbl_spp_do.do_no >= 0
                //             ORDER BY tbl_spp_do.do_no ASC";
                String sql = @"SELECT 
                             tbl_spp_do.do_no,
                             tbl_spp_do.to_delivery_qty,
                             tbl_spp_do.isRemoved,
                             tbl_spp_do.isDelivered,
                             tbl_spp_do.trf_tbl_code,
                             tbl_spp_stdpacking.qty_per_bag,
                             tbl_spp_po.customer_tbl_code
                             FROM tbl_spp_do 
                             INNER JOIN tbl_spp_po
                             ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                             INNER JOIN tbl_spp_customer 
                             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                             INNER JOIN tbl_item
                             ON tbl_spp_po.item_code = tbl_item.item_code
                             LEFT JOIN tbl_spp_size size1
                             ON tbl_item.size_tbl_code_1 = size1.tbl_code
                             LEFT JOIN tbl_spp_size size2
                             ON tbl_item.size_tbl_code_2 = size2.tbl_code
                             INNER JOIN tbl_spp_type
                             ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                             LEFT JOIN tbl_spp_stdpacking
                             ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                             WHERE tbl_spp_do.do_date >= DATEADD(month, -1, GETDATE())
                             ORDER BY tbl_spp_do.do_no ASC";


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

        public DataTable SBBPageDOWithTrfInfoSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                
                //sql query to get data from database
                String sql = @"SELECT 
                             tbl_spp_do.do_no,
                             tbl_spp_do.isRemoved,
                             tbl_spp_do.isDelivered,
                             tbl_spp_do.to_delivery_qty,
                             tbl_spp_do.trf_tbl_code,
                             tbl_trf_hist.trf_hist_trf_date,
                             tbl_trf_hist.trf_hist_qty,
                             tbl_trf_hist.trf_result,
                             tbl_spp_po.customer_tbl_code,
                             tbl_spp_customer.short_name,
                             tbl_spp_stdpacking.qty_per_packet,
                             tbl_spp_stdpacking.qty_per_bag
                             FROM tbl_spp_do 
                             INNER JOIN tbl_trf_hist
                             ON tbl_spp_do.trf_tbl_code = tbl_trf_hist.trf_hist_id
                             INNER JOIN tbl_spp_po
                             ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                             INNER JOIN tbl_spp_customer 
                             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                             INNER JOIN tbl_item
                             ON tbl_spp_po.item_code = tbl_item.item_code
                             INNER JOIN tbl_spp_size
                             ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code
                             INNER JOIN tbl_spp_type
                             ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                             FULL JOIN tbl_spp_stdpacking
                             ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                             ORDER BY tbl_spp_do.do_no ASC";

              
                //tbl_spp_do.tbl_code IS NOT NULL AND 
                //   INNER JOIN tbl_trf_hist
                //ON tbl_spp_do.trf_tbl_code = tbl_trf_hist.trf_hist_id
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

        public DataTable SBBPageDOWithTrfInfoSelect(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT  
                             tbl_spp_do.do_no,
                             tbl_spp_do.isRemoved,
                             tbl_spp_do.isDelivered,
                             tbl_spp_do.to_delivery_qty,
                             tbl_spp_do.trf_tbl_code,
                             tbl_trf_hist.trf_hist_trf_date,
                             tbl_trf_hist.trf_hist_qty,
                             tbl_trf_hist.trf_result,
                             tbl_spp_po.customer_tbl_code,
                             tbl_spp_customer.short_name,
                             tbl_spp_stdpacking.qty_per_packet,
                             tbl_spp_stdpacking.qty_per_bag
                             FROM tbl_spp_do 
                             INNER JOIN tbl_spp_po
                             ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                             INNER JOIN tbl_trf_hist
                             ON tbl_spp_do.trf_tbl_code = tbl_trf_hist.trf_hist_id 
                             INNER JOIN tbl_spp_customer 
                             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                             INNER JOIN tbl_item
                             ON tbl_spp_po.item_code = tbl_item.item_code
                             INNER JOIN tbl_spp_size
                             ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code
                             INNER JOIN tbl_spp_type
                             ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                             FULL JOIN tbl_spp_stdpacking
                             ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                             WHERE (tbl_trf_hist.trf_hist_trf_date 
                             BETWEEN @start 
                             AND @end OR (tbl_spp_do.isDelivered IS NULL OR tbl_spp_do.isDelivered = @false)) AND tbl_spp_do.do_no > 0
                             ORDER BY tbl_item.item_code ASC,tbl_spp_customer.tbl_code ASC,  tbl_trf_hist.trf_hist_trf_date ASC";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

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

        public DataTable DOWithTrfInfoSelect(string start, string end)
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
                             INNER JOIN tbl_trf_hist
                             ON tbl_spp_do.trf_tbl_code = tbl_trf_hist.trf_hist_id 
                             INNER JOIN tbl_spp_customer 
                             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                             INNER JOIN tbl_item
                             ON tbl_spp_po.item_code = tbl_item.item_code
                             INNER JOIN tbl_spp_size
                             ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code
                             INNER JOIN tbl_spp_type
                             ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                             FULL JOIN tbl_spp_stdpacking
                             ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                             WHERE tbl_trf_hist.trf_hist_trf_date 
                             BETWEEN @start 
                             AND @end 
                             ORDER BY tbl_item.item_code ASC,tbl_spp_customer.tbl_code ASC,  tbl_trf_hist.trf_hist_trf_date ASC";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

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


        public DataTable NEW_DOWithTrfInfoSelect(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT tbl_trf_hist.trf_result, 
                              tbl_item.item_code, 
                              tbl_spp_po.customer_tbl_code,
                              tbl_spp_customer.short_name,
                              tbl_trf_hist.trf_hist_trf_date,
                              tbl_trf_hist.trf_hist_qty FROM tbl_spp_do 
                             INNER JOIN tbl_spp_po
                             ON tbl_spp_do.po_tbl_code = tbl_spp_po.tbl_code 
                             INNER JOIN tbl_trf_hist
                             ON tbl_spp_do.trf_tbl_code = tbl_trf_hist.trf_hist_id 
                             INNER JOIN tbl_spp_customer 
                             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                             INNER JOIN tbl_item
                             ON tbl_spp_po.item_code = tbl_item.item_code
                             INNER JOIN tbl_spp_size
                             ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code
                             INNER JOIN tbl_spp_type
                             ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                             FULL JOIN tbl_spp_stdpacking
                             ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                             WHERE tbl_trf_hist.trf_hist_trf_date 
                             BETWEEN @start 
                             AND @end 
                             AND tbl_trf_hist.trf_result = 'Passed'
                             ORDER BY tbl_item.item_code ASC,tbl_spp_customer.tbl_code ASC,  tbl_trf_hist.trf_hist_trf_date ASC";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@false", false);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

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

        public DataTable DeliverySelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_delivery";

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

        public DataTable DeliveryOWithoutRemovedDataSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_delivery WHERE isRemoved IS NULL OR isRemoved = @false ";

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

        public DataTable DeliveryWithInfoSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_delivery 
                             INNER JOIN tbl_spp_po
                             ON tbl_delivery.po_tbl_code = tbl_spp_po.tbl_code 
                             INNER JOIN tbl_route
                             ON tbl_delivery.route_tbl_code = tbl_route.tbl_code 
                             INNER JOIN tbl_spp_customer 
                             ON tbl_spp_po.customer_tbl_code = tbl_spp_customer.tbl_code 
                             INNER JOIN tbl_item
                             ON tbl_spp_po.item_code = tbl_item.item_code
                             INNER JOIN tbl_spp_size
                             ON tbl_item.size_tbl_code_1 = tbl_spp_size.tbl_code
                             INNER JOIN tbl_spp_type
                             ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                             FULL JOIN tbl_spp_stdpacking
                             ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                             ORDER BY tbl_delivery.isRemoved ASC, tbl_delivery.isDelivered ASC, tbl_delivery.trip_no ASC, tbl_delivery.planning_no ASC, tbl_spp_po.po_date ASC, tbl_spp_po.po_code ASC";

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

        public DataTable ShippingDataSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_cust_shipping ORDER BY address_state ASC, shipping_fullname ASC";

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

        public DataTable ShippingDataSelectWithCustCode(string custTblCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_sbb_cust_shipping WHERE customer_tbl_code=@custTblCode ORDER BY address_state ASC, shipping_fullname ASC";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@custTblCode", custTblCode);

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

        public bool InsertMould(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_sbb_mould 
                            (" + MouldCode + ","
                            + MouldName + ","
                            + MouldTon + ","
                            + MouldWidth + ","
                            + MouldHeight + ","
                            + MouldLength + ","
                            + MouldDateStart + ","
                            + MouldTotalCavity + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Mould_code," +
                            "@Mould_name," +
                            "@Mould_ton," +
                            "@Mould_width," +
                            "@Mould_height," +
                            "@Mould_length," +
                            "@Mould_startdate," +
                            "@Mould_Total_cavity," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Mould_code", u.Mould_code);
                cmd.Parameters.AddWithValue("@Mould_name", u.Mould_name);
                cmd.Parameters.AddWithValue("@Mould_ton", u.Mould_ton);
                cmd.Parameters.AddWithValue("@Mould_width", u.Mould_width);
                cmd.Parameters.AddWithValue("@Mould_height", u.Mould_height);
                cmd.Parameters.AddWithValue("@Mould_length", u.Mould_length);
                cmd.Parameters.AddWithValue("@Mould_startdate", u.Mould_startdate);
                cmd.Parameters.AddWithValue("@Mould_Total_cavity", u.Mould_Total_cavity);
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

        public bool InsertMouldItemGroup(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_sbb_mould_item 
                            (" + GroupCode + ","
                            + MouldTblCode + ","
                            + ItemCode + ","
                            + MouldCycleTime + ","
                            + PWPerShot + ","
                            + RWPerShot + ","
                            + MouldItemCavity + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Group_code," +
                            "@Mould_tbl_code," +
                            "@Item_code," +
                            "@Mould_cycletime," +
                            "@Mould_pwpershot," +
                            "@Mould_rwpershot," +
                            "@Mould_item_cavity," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Group_code", u.Group_code);
                cmd.Parameters.AddWithValue("@Mould_tbl_code", u.Mould_tbl_code);
                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Mould_cycletime", u.Mould_cycletime);
                cmd.Parameters.AddWithValue("@Mould_pwpershot", u.Mould_pwpershot);
                cmd.Parameters.AddWithValue("@Mould_rwpershot", u.Mould_rwpershot);
                cmd.Parameters.AddWithValue("@Mould_item_cavity", u.Mould_item_cavity);

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

        public bool InsertPrice(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_sbb_price 
                            (" + ItemTblCode + ","
                            + DefaultPrice + ","
                            + DefaultDiscount + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Item_tbl_code," +
                            "@Default_price," +
                            "@Default_discount," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_tbl_code", u.Item_tbl_code);
                cmd.Parameters.AddWithValue("@Default_price", u.Default_price);
                cmd.Parameters.AddWithValue("@Default_discount", u.Default_discount);
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

        public bool InsertDiscount(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);


            try
            {
                String sql = @"INSERT INTO tbl_sbb_discount
                            (" + ItemTblCode + ","
                            + CustTblCode + ","
                            + UnitPrice + ","
                            + DiscountRate + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Item_tbl_code," +
                            "@Customer_tbl_code," +
                            "@Unit_price," +
                            "@Discount_rate," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_tbl_code", u.Item_tbl_code);
                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);
                cmd.Parameters.AddWithValue("@Unit_price", u.Unit_price);
                cmd.Parameters.AddWithValue("@Discount_rate", u.Discount_rate);
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

        public bool InsertPlan(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_sbb_plan 
                            (" + ItemCode + ","
                            + LocationArea + ","
                            + LocationLine + ","
                            + DateStart + ","
                            + DateEnd + ","
                            + TargetQty + ","
                            + MaxQty + ","
                            + PlanStatus + ","
                            + PlanType + ","
                            + PlanNote + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Item_code," +
                            "@Location_area," +
                            "@Location_line," +
                            "@Date_start," +
                            "@Date_end," +
                            "@Target_qty," +
                            "@Max_qty," +
                            "@Plan_status," +
                            "@Plan_type," +
                            "@Plan_note," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Location_area", u.Location_area);
                cmd.Parameters.AddWithValue("@Location_line", u.Location_line);
                cmd.Parameters.AddWithValue("@Date_start", u.Date_start);
                cmd.Parameters.AddWithValue("@Date_end", u.Date_end);
                cmd.Parameters.AddWithValue("@Target_qty", u.Target_qty);
                cmd.Parameters.AddWithValue("@Max_qty", u.Max_qty);
                cmd.Parameters.AddWithValue("@Plan_status", u.Plan_status);
                cmd.Parameters.AddWithValue("@Plan_type", u.Plan_type);
                cmd.Parameters.AddWithValue("@Plan_note", u.Plan_note);
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

        public bool InsertMatPlan(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_sbb_mat_plan 
                            (" + PlanCode + ","
                            + ItemCode + ","
                            + RequiredQty + ","
                            + DeliveredQty + ","
                            + PreparingQty + ","
                            + Note + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Plan_code," +
                            "@Item_code," +
                            "@Required_qty," +
                            "@Delivered_qty," +
                            "@Preparing_qty," +
                            "@Note," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Plan_code", u.Plan_code);
                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Required_qty", u.Required_qty);
                cmd.Parameters.AddWithValue("@Delivered_qty", u.Delivered_qty);
                cmd.Parameters.AddWithValue("@Preparing_qty", u.Preparing_qty);
                cmd.Parameters.AddWithValue("@Note", u.Note);
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

        public bool InsertMatPreparePlan(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_sbb_mat_prepare
                            (" + MatPlanCode + ","
                            + StdPacking + ","
                            + DeliveryBag + ","
                            + LocationFrom + ","
                            + DeliveryDate + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Mat_plan_code," +
                            "@Std_packing," +
                            "@Delivery_bag," +
                            "@Location_from," +
                            "@Delivery_date," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Mat_plan_code", u.Mat_plan_code);
                cmd.Parameters.AddWithValue("@Std_packing", u.Std_packing);
                cmd.Parameters.AddWithValue("@Delivery_bag", u.Delivery_bag);
                cmd.Parameters.AddWithValue("@Location_from", u.Location_from);
                cmd.Parameters.AddWithValue("@Delivery_date", u.Delivery_date);
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

        public bool InsertSize(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_size 
                            (" + SizeNumerator + ","
                            + SizeDenominator + ","
                            + SizeUnit + ","
                            + SizeWeight + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Size_Numerator," +
                            "@Size_Denominator," +
                            "@Size_Unit," +
                            "@Size_Weight," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Size_Numerator", u.Size_Numerator);
                cmd.Parameters.AddWithValue("@Size_Denominator", u.Size_Denominator);
                cmd.Parameters.AddWithValue("@Size_Unit", u.Size_Unit);
                cmd.Parameters.AddWithValue("@Size_Weight", u.Size_Weight);
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

        public bool InsertType(SBBDataBLL u)
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

        public bool InsertCategory(SBBDataBLL u)
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

        public bool InsertItem(SBBDataBLL u)
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

        public bool InsertColor(SBBDataBLL u)
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

        public bool InsertTon(SBBDataBLL u)
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

        public bool InsertStdPacking(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_stdpacking 
                            (" + ItemCode + ","
                            + QtyPerContainer + ","
                            + QtyPerPacket + ","
                             + QtyPerBag + ","
                               + MaxLevel + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Item_code," +
                            "@Qty_Per_Container," +
                            "@Qty_Per_Packet," +
                            "@Qty_Per_Bag," +
                            "@Max_Lvl," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Qty_Per_Container", u.Qty_Per_Container);
                cmd.Parameters.AddWithValue("@Qty_Per_Packet", u.Qty_Per_Packet);
                cmd.Parameters.AddWithValue("@Qty_Per_Bag", u.Qty_Per_Bag);
                cmd.Parameters.AddWithValue("@Max_Lvl", u.Max_Lvl);
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

        public bool InsertStdPackingBagContainer(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_stdpacking 
                            (" + ItemCode + ","
                            + QtyPerPacket + ","
                            + QtyPerBag + ","
                             + QtyPerContainer + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Item_code," +
                            "@Qty_Per_Packet," +
                            "@qty_per_bag," +
                            "@qty_per_container," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@qty_per_container", u.Qty_Per_Container);
                cmd.Parameters.AddWithValue("@qty_per_bag", u.Qty_Per_Bag);
                cmd.Parameters.AddWithValue("@Qty_Per_Packet", u.Qty_Per_Packet);
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

        public bool InsertCustomer(SBBDataBLL u)
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
                            + Address3 + ","
                            + AddressCity + ","
                            + AddressState + ","
                            + AddressPostalCode + ","
                            + AddressCountry + ","
                            + Fax + ","
                            + Phone1 + ","
                            + Phone2 + ","
                            + Email + ","
                            + Website + ","
                            + RouteTblCode + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Full_Name," +
                            "@Short_Name," +
                            "@Registration_No," +
                            "@Address_1," +
                            "@Address_2," +
                            "@Address_3," +
                            "@Address_City," +
                            "@Address_State," +
                            "@Address_Postal_Code," +
                            "@Address_Country," +
                            "@Fax," +
                            "@Phone_1," +
                            "@Phone_2," +
                            "@Email," +
                            "@Website," +
                            "@route_tbl_code," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Full_Name", u.Full_Name);
                cmd.Parameters.AddWithValue("@Short_Name", u.Short_Name);
                cmd.Parameters.AddWithValue("@Registration_No", u.Registration_No);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_3", u.Address_3);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);
                cmd.Parameters.AddWithValue("@Fax", u.Fax);
                cmd.Parameters.AddWithValue("@Phone_1", u.Phone_1);
                cmd.Parameters.AddWithValue("@Phone_2", u.Phone_2);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Website", u.Website);
                cmd.Parameters.AddWithValue("@route_tbl_code", u.Route_tbl_code);
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

        public bool InsertCustomerShippingData(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_sbb_cust_shipping 
                            (" + CustTblCode + ","
                            + ShippingFullName + ","
                            + ShippingShortName + ","
                            + Address1 + ","
                            + Address2 + ","
                            + Address3 + ","
                            + AddressCity + ","
                            + AddressState + ","
                            + AddressPostalCode + ","
                            + AddressCountry + ","
                            + Phone1 + ","
                            + RouteTblCode + ","
                            + ShippingTransporter + ","
                            + DiscountAdjust + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@Customer_tbl_code," +
                            "@Shipping_Full_Name," +
                            "@Shipping_Short_Name," +
                            "@Address_1," +
                            "@Address_2," +
                            "@Address_3," +
                            "@Address_City," +
                            "@Address_State," +
                            "@Address_Postal_Code," +
                            "@Address_Country," +
                            "@Phone_1," +
                            "@route_tbl_code," +
                            "@Shipping_Transporter," +
                            "@Discount_Adjust," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);
                cmd.Parameters.AddWithValue("@Shipping_Full_Name", u.Shipping_Full_Name);
                cmd.Parameters.AddWithValue("@Shipping_Short_Name", u.Shipping_Short_Name);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_3", u.Address_3);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);
                cmd.Parameters.AddWithValue("@Phone_1", u.Phone_1);
                cmd.Parameters.AddWithValue("@route_tbl_code", u.Route_tbl_code);
                cmd.Parameters.AddWithValue("@Shipping_Transporter", u.Shipping_Transporter);
                cmd.Parameters.AddWithValue("@Discount_Adjust", u.Discount_Adjust);
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

        public bool InsertPO(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_spp_po 
                            (" + POCode + ","
                            + CustTblCode + ","
                            + PONo + ","
                            + ItemCode + ","
                            + POQty + ","
                            + DeliveredQty + ","
                            + ShippingFullName + ","
                            + ShippingShortName + ","
                            + ShippingTransporter + ","
                            + Address1 + ","
                            + Address2 + ","
                            + Address3 + ","
                            + AddressCity + ","
                            + AddressState + ","
                            + AddressPostalCode + ","
                            + AddressCountry + ","
                            + DefaultShippingAddress + ","
                             + PONote + ","
                            + PODate + ","
                            + UnitPrice + ","
                            + DiscountRate + ","
                            + SubTotal + ","
                            + Phone1 + ","
                            + CustOwnDO + ","
                            + RemarkInDO + ","
                            + PriorityLevel + ","
                            + TargetDeliveryDate + ","
                            + ItemPriorityLevel + ","
                            + ItemTargetDeliveryDate + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@PO_code," +
                            "@Customer_tbl_code," +
                            "@PO_no," +
                            "@Item_code," +
                            "@PO_qty," +
                            "@Delivered_qty," +
                            "@Shipping_Full_Name," +
                            "@Shipping_Short_Name," +
                            "@Shipping_Transporter," +
                            "@Address_1," +
                            "@Address_2," +
                            "@Address_3," +
                            "@Address_City," +
                            "@Address_State," +
                            "@Address_Postal_Code," +
                            "@Address_Country," +
                            "@UseBillingAddress," +
                             "@PO_note," +
                            "@PO_date," +
                              "@Unit_price," +
                             "@Discount_rate," +
                            "@Sub_total," +
                            "@Phone_1," +
                            "@Cust_Own_DO," +
                            "@remark_in_do," +
                            "@Priority_level," +
                            "@Target_Delivery_Date," +
                            "@Item_Priority_level," +
                            "@Item_Target_Delivery_Date," +
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
                cmd.Parameters.AddWithValue("@UseBillingAddress", u.UseBillingAddress);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_3", u.Address_3);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);
                cmd.Parameters.AddWithValue("@remark_in_do", u.remark_in_do);

                cmd.Parameters.AddWithValue("@Shipping_Full_Name", u.Shipping_Full_Name);
                cmd.Parameters.AddWithValue("@Shipping_Short_Name", u.Shipping_Short_Name);
                cmd.Parameters.AddWithValue("@Shipping_Transporter", u.Shipping_Transporter);


                cmd.Parameters.AddWithValue("@Unit_price", u.Unit_price);
                cmd.Parameters.AddWithValue("@Discount_rate", u.Discount_rate);
                cmd.Parameters.AddWithValue("@Sub_total", u.Sub_total);
                cmd.Parameters.AddWithValue("@Phone_1", u.Phone_1);
                cmd.Parameters.AddWithValue("@Cust_Own_DO", u.Cust_Own_DO);

                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);
                cmd.Parameters.AddWithValue("@Priority_level", u.Priority_level);
                cmd.Parameters.AddWithValue("@Target_Delivery_Date", u.Target_Delivery_Date);
                cmd.Parameters.AddWithValue("@Item_Priority_level", u.Item_Priority_level);
                cmd.Parameters.AddWithValue("@Item_Target_Delivery_Date", u.Item_Target_Delivery_Date);

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

        public bool InsertOrUpdatePO(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                           + POCode + "=@PO_code,"
                           + CustTblCode + "=@Customer_tbl_code,"
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
                           " WHERE tbl_code=@Table_Code " +
                            "IF @@ROWCOUNT = 0 " +
                            "INSERT INTO tbl_spp_po(" 
                            + POCode + ", "
                            + CustTblCode + ","
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
                cmd.Parameters.AddWithValue("@PO_note", u.PO_note);
                cmd.Parameters.AddWithValue("@PO_date", u.PO_date);
                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);
                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@PO_qty", u.PO_qty);
                cmd.Parameters.AddWithValue("@DefaultShippingAddress", u.UseBillingAddress);
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool InsertDO(SBBDataBLL u)
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

        public bool InsertDelivery(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_delivery 
                            (" + RouteTblCode + ","
                            + TripNo + ","
                            + PlanningNo + ","
                            + POTableCode + ","
                            + DeliveryStatus + ","
                            + DeliverPcs + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@route_tbl_code," +
                            "@trip_no," +
                             "@planning_no," +
                             "@PO_tbl_code," +
                            "@delviery_status," +
                             "@deliver_pcs," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@trip_no", 9999);
                cmd.Parameters.AddWithValue("@route_tbl_code", u.Route_tbl_code);
                cmd.Parameters.AddWithValue("@planning_no", u.Planning_no);
                cmd.Parameters.AddWithValue("@PO_tbl_code", u.PO_tbl_code);
                cmd.Parameters.AddWithValue("@delviery_status", u.Delivery_status);
                cmd.Parameters.AddWithValue("@deliver_pcs", u.Deliver_pcs);
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

        public bool InsertRoute(SBBDataBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_route 
                            (" + RouteName + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@route_name," +
                            "@Updated_Date," +
                            "@Updated_By)";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@route_name", u.Route_name);
              
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

        public bool MouldUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_mould
                            SET "
                            + MouldCode + "=@Mould_code,"
                            + MouldName + "=@Mould_name,"
                            + MouldTon + "=@Mould_ton,"
                            + MouldWidth + "=@Mould_width,"
                            + MouldHeight + "=@Mould_height,"
                            + MouldLength + "=@Mould_length,"
                            + MouldDateStart + "=@Mould_startdate,"
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Mould_code", u.Mould_code);
                cmd.Parameters.AddWithValue("@Mould_name", u.Mould_name);
                cmd.Parameters.AddWithValue("@Mould_ton", u.Mould_ton);
                cmd.Parameters.AddWithValue("@Mould_width", u.Mould_width);
                cmd.Parameters.AddWithValue("@Mould_height", u.Mould_height);
                cmd.Parameters.AddWithValue("@Mould_length", u.Mould_length);
                cmd.Parameters.AddWithValue("@Mould_startdate", u.Mould_startdate);

                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);

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

        public bool MouldItemGroupUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_mould_item
                            SET "
                            + GroupCode + "=@Group_code,"
                            + MouldTblCode + "=@Mould_tbl_code,"
                            + ItemCode + "=@Item_code,"
                            + MouldCycleTime + "=@Mould_cycletime,"
                            + PWPerShot + "=@Mould_pwpershot,"
                            + RWPerShot + "=@Mould_rwpershot,"
                            + MouldItemCavity + "=@Mould_item_cavity,"
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Group_code", u.Group_code);
                cmd.Parameters.AddWithValue("@Mould_tbl_code", u.Mould_tbl_code);
                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@Mould_cycletime", u.Mould_cycletime);
                cmd.Parameters.AddWithValue("@Mould_pwpershot", u.Mould_pwpershot);
                cmd.Parameters.AddWithValue("@Mould_rwpershot", u.Mould_rwpershot);
                cmd.Parameters.AddWithValue("@Mould_item_cavity", u.Mould_item_cavity);

                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);

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

        public bool PriceUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_price
                            SET "
                            + ItemTblCode + "=@Item_tbl_code,"
                            + DefaultPrice + "=@Default_price,"
                            + DefaultDiscount + "=@Default_discount,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_tbl_code", u.Item_tbl_code);
                cmd.Parameters.AddWithValue("@Default_price", u.Default_price);
                cmd.Parameters.AddWithValue("@Default_discount", u.Default_discount);
            
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

        public bool DiscountUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_discount
                            SET "
                            + ItemTblCode + "=@Item_tbl_code,"
                            + CustTblCode + "=@Customer_tbl_code,"
                            + UnitPrice + "=@Unit_price,"
                            + DiscountRate + "=@Discount_rate,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Item_tbl_code", u.Item_tbl_code);
                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);
                cmd.Parameters.AddWithValue("@Unit_price", u.Unit_price);
                cmd.Parameters.AddWithValue("@Discount_rate", u.Discount_rate);

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

        public bool PlanUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_plan
                            SET "
                            + LocationArea + "=@Location_area,"
                            + LocationLine + "=@Location_line,"
                            + DateStart + "=@Date_start,"
                            + DateEnd + "=@Date_end,"
                            + TargetQty + "=@Target_qty,"
                            + MaxQty + "=@Max_qty,"
                            + PlanStatus + "=@Plan_status,"
                            + PlanType + "=@Plan_type,"
                            + PlanNote + "=@Plan_note,"
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Location_area", u.Location_area);
                cmd.Parameters.AddWithValue("@Location_line", u.Location_line);
                cmd.Parameters.AddWithValue("@Date_start", u.Date_start);
                cmd.Parameters.AddWithValue("@Date_end", u.Date_end);
                cmd.Parameters.AddWithValue("@Target_qty", u.Target_qty);
                cmd.Parameters.AddWithValue("@Max_qty", u.Max_qty);
                cmd.Parameters.AddWithValue("@Plan_status", u.Plan_status);
                cmd.Parameters.AddWithValue("@Plan_type", u.Plan_type);
                cmd.Parameters.AddWithValue("@Plan_note", u.Plan_note);

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

        public bool PlanRemove(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_plan
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

        public bool MatPlanUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_mat_plan
                            SET "
                            + RequiredQty + "=@Required_qty,"
                            + DeliveredQty + "=@Delivered_qty,"
                            + PreparingQty + "=@Preparing_qty,"
                            + Note + "=@Note,"
                            + IsCompleted + "=@IsCompleted,"
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Required_qty", u.Required_qty);
                cmd.Parameters.AddWithValue("@Delivered_qty", u.Delivered_qty);
                cmd.Parameters.AddWithValue("@Preparing_qty", u.Preparing_qty);
                cmd.Parameters.AddWithValue("@Note", u.Note);

                cmd.Parameters.AddWithValue("@IsCompleted", u.IsCompleted);
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

        public bool MatPlanRemove(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_mat_plan
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

        public bool MatPlanComplete(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_mat_plan
                            SET "
                            + IsCompleted + "=@IsCompleted,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@IsCompleted", u.IsCompleted);
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

        public bool MatPlanPrepareUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_mat_prepare
                            SET "
                            + StdPacking + "=@Std_packing,"
                            + DeliveryBag + "=@Delivery_bag,"
                            + LocationFrom + "=@Location_from,"
                            + DeliveryDate + "=@Delivery_date,"
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Std_packing", u.Std_packing);
                cmd.Parameters.AddWithValue("@Delivery_bag", u.Delivery_bag);
                cmd.Parameters.AddWithValue("@Location_from", u.Location_from);
                cmd.Parameters.AddWithValue("@Delivery_date", u.Delivery_date);

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

        public bool MatPlanPrepareRemove(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_mat_prepare
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

        public bool SizeUpdate(SBBDataBLL u)
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
                            + SizeWeight + "=@Size_Weight,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Size_Numerator", u.Size_Numerator);
                cmd.Parameters.AddWithValue("@Size_Denominator", u.Size_Denominator);
                cmd.Parameters.AddWithValue("@Size_Unit", u.Size_Unit);
                cmd.Parameters.AddWithValue("@Size_Weight", u.Size_Weight);
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

        public bool TypeUpdate(SBBDataBLL u)
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

        public bool CategoryUpdate(SBBDataBLL u)
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

        public bool ItemUpdate(SBBDataBLL u)
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

        public bool ColorUpdate(SBBDataBLL u)
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

        public bool TonUpdate(SBBDataBLL u)
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

        public bool StdPackingBagContainerUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_stdpacking 
                            SET "
                            + QtyPerContainer + "=@qty_per_container,"
                            + QtyPerPacket + "=@Qty_Per_Packet,"
                            + QtyPerBag + "=@qty_per_bag,"
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@qty_per_container", u.Qty_Per_Container);
                cmd.Parameters.AddWithValue("@Qty_Per_Packet", u.Qty_Per_Packet);
                cmd.Parameters.AddWithValue("@qty_per_bag", u.Qty_Per_Bag);
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

        public bool StdPackingUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_stdpacking 
                            SET "
                            + QtyPerContainer + "=@Qty_Per_Container,"
                            + QtyPerPacket + "=@Qty_Per_Packet,"
                            + QtyPerBag + "=@Qty_Per_Bag,"
                            + MaxLevel + "=@Max_Lvl,"
                             + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Qty_Per_Container", u.Qty_Per_Container);
                cmd.Parameters.AddWithValue("@Qty_Per_Packet", u.Qty_Per_Packet);
                cmd.Parameters.AddWithValue("@Qty_Per_Bag", u.Qty_Per_Bag);
                cmd.Parameters.AddWithValue("@Max_Lvl", u.Max_Lvl);
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

        public bool SPPItemUpdate(SBBDataBLL u)
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

        public bool CustomerUpdate(SBBDataBLL u)
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
                            + Address3 + "=@Address_3,"
                            + AddressCity + "=@Address_City,"
                            + AddressState + "=@Address_State,"
                            + AddressPostalCode + "=@Address_Postal_Code,"
                            + AddressCountry + "=@Address_Country,"
                            + Fax + "=@Fax,"
                            + Phone1 + "=@Phone_1,"
                            + Phone2 + "=@Phone_2,"
                            + Email + "=@Email,"
                            + Website + "=@Website,"
                            + RouteTblCode + "=@route_tbl_code,"
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
                cmd.Parameters.AddWithValue("@Address_3", u.Address_3);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);
                cmd.Parameters.AddWithValue("@Fax", u.Fax);
                cmd.Parameters.AddWithValue("@Phone_1", u.Phone_1);
                cmd.Parameters.AddWithValue("@Phone_2", u.Phone_2);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Website", u.Website);
                cmd.Parameters.AddWithValue("@route_tbl_code", u.Route_tbl_code);
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

        public bool ShippingDataUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_sbb_cust_shipping
                            SET "
                            + CustTblCode + "=@Customer_tbl_code,"
                            + ShippingFullName + "=@Shipping_Full_Name,"
                            + ShippingShortName + "=@Shipping_Short_Name,"
                            + Address1 + "=@Address_1,"
                            + Address2 + "=@Address_2,"
                            + Address3 + "=@Address_3,"
                            + AddressCity + "=@Address_City,"
                            + AddressState + "=@Address_State,"
                            + AddressPostalCode + "=@Address_Postal_Code,"
                            + AddressCountry + "=@Address_Country,"
                            + Phone1 + "=@Phone_1,"
                            + RouteTblCode + "=@route_tbl_code,"
                            + ShippingTransporter + "=@Shipping_Transporter,"
                            + IsRemoved + "=@IsRemoved,"
                            + CustOwnDO + "=@Cust_Own_DO,"
                            + DiscountAdjust + "=@Discount_Adjust,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);
                cmd.Parameters.AddWithValue("@Shipping_Full_Name", u.Shipping_Full_Name);
                cmd.Parameters.AddWithValue("@Shipping_Short_Name", u.Shipping_Short_Name);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_3", u.Address_3);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);
                cmd.Parameters.AddWithValue("@Phone_1", u.Phone_1);
                cmd.Parameters.AddWithValue("@route_tbl_code", u.Route_tbl_code);
                cmd.Parameters.AddWithValue("@Shipping_Transporter", u.Shipping_Transporter);
                cmd.Parameters.AddWithValue("@Cust_Own_DO", u.Cust_Own_DO);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@Discount_Adjust", u.Discount_Adjust);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);

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


        public bool ShippingRemove(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE  tbl_sbb_cust_shipping 
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
        public bool CustomerRemove(SBBDataBLL u)
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

        public bool POUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + CustTblCode + "=@Customer_tbl_code,"
                            + PONo + "=@PO_no,"
                            + ItemCode + "=@Item_code,"
                            + POQty + "=@PO_qty,"
                            + DefaultShippingAddress + "=@DefaultShippingAddress,"
                            + ShippingFullName + "=@Shipping_Full_Name,"
                            + ShippingShortName + "=@Shipping_Short_Name,"
                            + ShippingTransporter + "=@Shipping_Transporter,"
                            + Address1 + "=@Address_1,"
                            + Address2 + "=@Address_2,"
                            + Address3 + "=@Address_3,"
                            + AddressCity + "=@Address_City,"
                            + AddressState + "=@Address_State,"
                            + AddressPostalCode + "=@Address_Postal_Code,"
                            + AddressCountry + "=@Address_Country,"
                            + PONote + "=@PO_note,"
                            + PODate + "=@PO_date,"
                             + UnitPrice + "=@Unit_price,"
                            + DiscountRate + "=@Discount_rate,"
                            + SubTotal + "=@Sub_total,"
                            + Phone1 + "=@Phone_1,"
                            + CustOwnDO + "=@Cust_Own_DO,"
                            + RemarkInDO + "=@remark_in_do,"
                            + PriorityLevel + "=@Priority_level,"
                            + TargetDeliveryDate + "=@Target_Delivery_Date,"
                            + ItemPriorityLevel + "=@Item_Priority_level,"
                            + ItemTargetDeliveryDate + "=@Item_Target_Delivery_Date,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PO_no", u.PO_no);
                cmd.Parameters.AddWithValue("@PO_note", u.PO_note);
                cmd.Parameters.AddWithValue("@PO_date", u.PO_date);
                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);
                cmd.Parameters.AddWithValue("@Item_code", u.Item_code);
                cmd.Parameters.AddWithValue("@PO_qty", u.PO_qty);
                cmd.Parameters.AddWithValue("@DefaultShippingAddress", u.UseBillingAddress);
                cmd.Parameters.AddWithValue("@Address_1", u.Address_1);
                cmd.Parameters.AddWithValue("@Address_2", u.Address_2);
                cmd.Parameters.AddWithValue("@Address_3", u.Address_3);
                cmd.Parameters.AddWithValue("@Shipping_Full_Name", u.Shipping_Full_Name);
                cmd.Parameters.AddWithValue("@Shipping_Short_Name", u.Shipping_Short_Name);
                cmd.Parameters.AddWithValue("@Shipping_Transporter", u.Shipping_Transporter);
                cmd.Parameters.AddWithValue("@Address_City", u.Address_City);
                cmd.Parameters.AddWithValue("@Address_State", u.Address_State);
                cmd.Parameters.AddWithValue("@Address_Postal_Code", u.Address_Postal_Code);
                cmd.Parameters.AddWithValue("@Address_Country", u.Address_Country);

                cmd.Parameters.AddWithValue("@Unit_price", u.Unit_price);
                cmd.Parameters.AddWithValue("@Discount_rate", u.Discount_rate);
                cmd.Parameters.AddWithValue("@Sub_total", u.Sub_total);
                cmd.Parameters.AddWithValue("@Phone_1", u.Phone_1);
                cmd.Parameters.AddWithValue("@Cust_Own_DO", u.Cust_Own_DO);
                cmd.Parameters.AddWithValue("@remark_in_do", u.remark_in_do);

                cmd.Parameters.AddWithValue("@Table_Code", u.Table_Code);
                cmd.Parameters.AddWithValue("@Updated_Date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", u.Updated_By);
                cmd.Parameters.AddWithValue("@Priority_level", u.Priority_level);
                cmd.Parameters.AddWithValue("@Target_Delivery_Date", u.Target_Delivery_Date);
                cmd.Parameters.AddWithValue("@Item_Priority_level", u.Item_Priority_level);
                cmd.Parameters.AddWithValue("@Item_Target_Delivery_Date", u.Item_Target_Delivery_Date);
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

        public bool PORemove(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //String sql = "DELETE FROM tbl_spp_po WHERE po_code =@PO_code";

                String sql = @"DELETE FROM tbl_spp_po "
                           +" WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                //cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                //cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
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

        public bool POToDeliveryDataUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + ToDeliveryQty + "=@To_delivery_qty,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);


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

        public bool PODeliveredDataUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + DeliveredQty + "=@Delivered_qty,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@Delivered_qty", u.Delivered_qty);
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

        public bool SetPOPriorityUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + PriorityLevel + "=@priority_level,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE po_code=@PO_code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@priority_level", u.Priority_level);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
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

        public bool SetItemPriorityUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + PriorityLevel + "=@priority_level,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@priority_level", u.Priority_level);
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

        public bool SetCustomerPriorityUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + PriorityLevel + "=@priority_level,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE customer_tbl_code=@Customer_tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@priority_level", u.Priority_level);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@Customer_tbl_code", u.Customer_tbl_code);

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

        public bool POFreezeUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_po 
                            SET "
                            + Freeze + "=@Freeze,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE po_code=@PO_code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@Freeze", u.Freeze);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
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

        public bool PODelete(SBBDataBLL u)
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

        public bool DOUpdate(SBBDataBLL u)
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

        public bool DORemovedStatusAndDoNoUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_do 
                            SET "
                            + DONo + "=@DO_no,"                        
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@DO_no", u.DO_no);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
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

        public bool DONoChange(SBBDataBLL u, string oldDoNo)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_do 
                            SET "
                            + DONo + "=@DO_no,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE do_no=@oldDoNo";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@oldDoNo", oldDoNo);

                cmd.Parameters.AddWithValue("@DO_no", u.DO_no);
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

        public bool DODelivered(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_do 
                            SET "
                            + IsDelivered + "=@IsDelivered,"
                            + TrfTableCode + "=@Trf_tbl_code,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@IsDelivered", u.IsDelivered);
                cmd.Parameters.AddWithValue("@Trf_tbl_code", u.Trf_tbl_code);
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

        public bool DORemove(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_spp_do
                            SET "
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE do_no=@DO_no";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.Updated_Date);
                cmd.Parameters.AddWithValue("@updated_by", u.Updated_By);
                cmd.Parameters.AddWithValue("@DO_no", u.DO_no);

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

        public bool DeliveryUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_delivery

                            SET "
                            + TripNo + "=@trip_no,"
                            + RouteTblCode + "=@route_tbl_code,"
                            + POTableCode + "=@PO_tbl_code,"
                            + DeliveryDate + "=@delivery_date,"
                            + DeliveryStatus + "=@delviery_status,"
                            + DeliverPcs + "=@deliver_pcs,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@trip_no", u.Trip_no);
                cmd.Parameters.AddWithValue("@route_tbl_code", u.Route_tbl_code);
                cmd.Parameters.AddWithValue("@PO_tbl_code", u.PO_tbl_code);
                cmd.Parameters.AddWithValue("@delivery_date", u.Delivery_date);
                cmd.Parameters.AddWithValue("@delviery_status", u.Delivery_status);
                cmd.Parameters.AddWithValue("@deliver_pcs", u.Deliver_pcs);

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

        public bool DeliveryTripNumberAndStatusUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_delivery
                            SET "
                            + TripNo + "=@trip_no,"
                             + DeliveryStatus + "=@delivery_status,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " FROM tbl_delivery " +
                            "INNER JOIN tbl_spp_po " +
                            "ON tbl_delivery.po_tbl_code = tbl_spp_po.tbl_code " +
                            "WHERE tbl_delivery.planning_no=@planning_no " +
                            "AND tbl_spp_po.po_code=@PO_code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@trip_no", u.Trip_no);
                cmd.Parameters.AddWithValue("@delivery_status", u.Delivery_status);
                cmd.Parameters.AddWithValue("@planning_no", u.Planning_no);
                cmd.Parameters.AddWithValue("@PO_code", u.PO_code);

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

        public bool DeliveryTripNumberUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_delivery
                            SET "
                            + TripNo + "=@trip_no,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " FROM tbl_delivery " +
                            "INNER JOIN tbl_spp_po " +
                            "ON tbl_delivery.po_tbl_code = tbl_spp_po.tbl_code " +
                            "WHERE tbl_delivery.planning_no=@planning_no " +
                            "AND tbl_spp_po.po_code=@PO_code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@trip_no", u.Trip_no);
                cmd.Parameters.AddWithValue("@planning_no", u.Planning_no);
                cmd.Parameters.AddWithValue("@PO_code", u.PO_code);

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

        public bool DeliveryTripDateUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_delivery
                            SET "
                            + DeliveryDate + "=@delivery_date,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " FROM tbl_delivery " +
                            "INNER JOIN tbl_spp_po " +
                            "ON tbl_delivery.po_tbl_code = tbl_spp_po.tbl_code " +
                            "WHERE tbl_delivery.planning_no=@planning_no " +
                            "AND tbl_spp_po.po_code=@PO_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

             
                cmd.Parameters.AddWithValue("@delivery_date", u.Delivery_date?? Convert.DBNull);
                cmd.Parameters.AddWithValue("@planning_no", u.Planning_no);
                cmd.Parameters.AddWithValue("@PO_code", u.PO_code);

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

        public bool RouteUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_route

                            SET "
                            + RouteName + "=@route_name,"
                            + IsRemoved + "=@IsRemoved,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@route_name", u.Route_name);
                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
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

        public bool RouteRemove(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_route
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

        public bool DeliveryCancel(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_delivery
                            SET "
                            + IsRemoved + "=@IsRemoved,"
                            + DeliveryStatus + "=@delivery_status,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@IsRemoved", u.IsRemoved);
                cmd.Parameters.AddWithValue("@delivery_status", u.Delivery_status);
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

        public bool DeliveryStatusUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_delivery
                            SET "
                            + DeliveryStatus + "=@delivery_status,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@delivery_status", u.Delivery_status);
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

        public bool DeliveryisDeliveredUpdate(SBBDataBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_delivery
                            SET "
                            + IsDelivered + "=@IsDelivered,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE tbl_code=@Table_Code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@IsDelivered", u.IsDelivered);
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

        public bool SaveMouldData(SBBDataBLL u)
        {
            bool result = false;
            bool dataExist = false;

            DataTable dt_Mould = MouldSelect();

            if(dt_Mould != null && dt_Mould.Rows.Count > 0)
            {
                foreach(DataRow row in dt_Mould.Rows)
                {
                    if(u.Mould_code == row[MouldCode].ToString())
                    {
                        dataExist = true;
                        u.Table_Code = int.TryParse(row[TableCode].ToString(), out int x) ? x : -1;
                    }
                }
            }
            
            if(!dataExist)
            {
                //insert
                result = InsertMould(u);
            }
            else
            {
                //update
                if(u.Table_Code > 0)
                    result = MouldUpdate(u);

                else
                {
                    MessageBox.Show("Failed to save mould data!\nTable code= -1");
                }
            }

            if(!result)
            {
                MessageBox.Show("Failed to save mould data!\nresult = false");

            }
            return result;
        }

    }
}
