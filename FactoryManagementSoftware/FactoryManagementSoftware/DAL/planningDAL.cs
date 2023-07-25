using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.DAL
{
    class planningDAL
    {
        #region data string name getter
        public string jobNo { get; } = "plan_id";
        public string planAddedDate { get; } = "plan_added_date";
        public string planAddedBy { get; } = "plan_added_by";
        public string planUpdatedDate { get; } = "plan_updated_date";
        public string planUpdatedby { get; } = "plan_updated_by";
        public string planStatus { get; } = "plan_status";
        public string planNote { get; } = "plan_note";
        public string planCT { get; } = "plan_ct";
        public string planMouldCode { get; } = "mould_code";
        public string planPW { get; } = "plan_pw";
        public string planItemPW { get; } = "plan_item_pw";
        public string planRW { get; } = "plan_rw";
        public string planCavity { get; } = "plan_cavity";
        public string planItemCavity { get; } = "plan_item_cavity";

        public string productionPurpose { get; } = "production_purpose";

        public string partCode { get; } = "part_code";

        public string materialCode { get; } = "material_code";
        public string rawMatRatio_1 { get; } = "raw_mat_ratio_1";
        public string rawMatRatio_2 { get; } = "raw_mat_ratio_2";
        public string materialCode2 { get; } = "material_code_2";
        public string materialBagQty_1 { get; } = "material_bag_qty";
        public string materialBagQty_2 { get; } = "material_bag_qty_2";
        public string rawMaterialQty_1 { get; } = "raw_material_qty";
        public string rawMaterialQty_2 { get; } = "raw_material_qty_2";
        public string materialRecycleUse { get; } = "material_recycle_use";

        public string colorMaterialCode { get; } = "color_material_code";
        public string colorMaterialUsage { get; } = "color_material_usage";
        public string colorMaterialQty { get; } = "color_material_qty";

        public string targetQty { get; } = "target_qty";
        public string ableQty { get; } = "able_produce_qty";
        public string productionDay { get; } = "production_day";
        public string productionHour { get; } = "production_hour";
        public string productionHourPerDay { get; } = "production_hour_per_day";
        public string productionStartDate { get; } = "production_start_date";
        public string productionEndDate { get; } = "production_End_date";

        public string planProduced { get; } = "plan_produced";
        public string machineID { get; } = "machine_id";
        public string familyWith { get; } = "family_with";

        public string recording { get; } = "recording";

        public string Checked { get; } = "checked";
        public string CheckedBy { get; } = "check_by";
        public string CHeckedDate { get; } = "check_date";
        #endregion

        #region variable/class object declare

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

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
                String sql = @"SELECT * FROM tbl_plan 
                                INNER JOIN tbl_item 
                                ON tbl_plan.part_code = tbl_item.item_code 
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC";
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

        public DataTable SelectRecordingPlan()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_plan 
                                INNER JOIN tbl_item 
                                ON tbl_plan.part_code = tbl_item.item_code 
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id 
                                WHERE tbl_plan.recording = 1
                                ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC";
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
        public DataTable SelectOrderByItem()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_plan 
                                INNER JOIN tbl_item 
                                ON tbl_plan.part_code = tbl_item.item_code 
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id ORDER BY tbl_plan.part_code ASC";
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

        public DataTable SelectActivePlanning()
        {
            Text text = new Text();
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_plan 
                                INNER JOIN tbl_item
                                ON tbl_plan.part_code = tbl_item.item_code
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id 
                                WHERE tbl_plan.plan_status=@planning_status_running OR tbl_plan.plan_status=@planning_status_pending OR tbl_plan.plan_status=@planning_status_draft";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@planning_status_running", text.planning_status_running);
                cmd.Parameters.AddWithValue("@planning_status_pending", text.planning_status_pending);
                cmd.Parameters.AddWithValue("@planning_status_draft", text.planning_status_draft);
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

        public DataTable SelectCompletedOrRunningPlan()
        {
            Text text = new Text();
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_plan 
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id 
                                WHERE tbl_plan.plan_status=@planning_status_running OR tbl_plan.plan_status=@planning_status_completed OR tbl_plan.plan_status=@planning_status_pending
                                ORDER BY tbl_plan.part_code ASC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@planning_status_running", text.planning_status_running);
                cmd.Parameters.AddWithValue("@planning_status_pending", text.planning_status_pending);
                cmd.Parameters.AddWithValue("@planning_status_completed", text.planning_status_completed);
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
                String sql = "SELECT TOP 1 * FROM tbl_plan ORDER BY plan_id DESC";
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

        public int getLastInsertedPlanID()
        {
            int PlanID = -1;

            DataTable lastInsertedData = lastRecordSelect();

            foreach(DataRow row in lastInsertedData.Rows)
            {
                PlanID = Convert.ToInt32(row[jobNo]);
            }

            return PlanID;
        }

        #endregion

        #region Insert Data in Database

        public bool Insert(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_plan 
                            (" + planAddedDate + ","
                            + planAddedBy + ","
                            + planStatus + ","
                            + planNote + ","
                            + planCT + ","
                            + planPW + ","
                            + planRW + ","
                            + planCavity + ","
                            + partCode + ","
                            + productionPurpose + ","
                            + materialCode + ","
                            + materialBagQty_1 + ","
                            + materialRecycleUse + ","
                            + colorMaterialCode + ","
                            + colorMaterialUsage + ","
                            + colorMaterialQty + ","
                            + targetQty + ","
                            + ableQty + ","
                            + productionDay + ","
                            + productionHour + ","
                            + productionHourPerDay + ","
                            + productionStartDate + ","
                            + productionEndDate + ","
                            + machineID + ","
                            + familyWith + ") VALUES" +
                            "(@plan_added_date," +
                            "@plan_added_by," +
                            "@plan_status," +
                            "@plan_note," +
                            "@plan_ct," +
                            "@plan_pw," +
                            "@plan_rw," +
                            "@plan_cavity," +
                            "@part_code," +
                            "@production_purpose," +
                            "@material_code," +
                            "@material_bag_qty," +
                            "@material_recycle_use," +
                            "@color_material_code," +
                            "@color_material_usage," +
                            "@color_material_qty," +
                            "@production_target_qty," +
                            "@production_able_produce_qty," +
                            "@production_day," +
                            "@production_hour," +
                            "@production_hour_per_day," +
                            "@production_start_date," +
                            "@production_end_date," +
                            "@machine_id," +
                            "@family_with)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_added_date", u.plan_added_date);
                cmd.Parameters.AddWithValue("@plan_added_by", u.plan_added_by);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);
                cmd.Parameters.AddWithValue("@plan_note", u.plan_remark);
                cmd.Parameters.AddWithValue("@plan_ct", u.plan_ct);
                cmd.Parameters.AddWithValue("@plan_pw", u.plan_pw);
                cmd.Parameters.AddWithValue("@plan_rw", u.plan_rw);
                cmd.Parameters.AddWithValue("@plan_cavity", u.plan_cavity);
                cmd.Parameters.AddWithValue("@part_code", u.part_code);
                cmd.Parameters.AddWithValue("@production_purpose", u.production_purpose);
                cmd.Parameters.AddWithValue("@material_code", u.material_code);
                cmd.Parameters.AddWithValue("@material_bag_qty", u.material_bag_qty);
                cmd.Parameters.AddWithValue("@material_recycle_use", u.material_recycle_use);
                cmd.Parameters.AddWithValue("@color_material_code", u.color_material_code);
                cmd.Parameters.AddWithValue("@color_material_usage", u.color_material_usage);
                cmd.Parameters.AddWithValue("@color_material_qty", u.color_material_qty);
                cmd.Parameters.AddWithValue("@production_target_qty", u.production_target_qty);
                cmd.Parameters.AddWithValue("@production_able_produce_qty", u.production_able_produce_qty);
                cmd.Parameters.AddWithValue("@production_day", u.production_day);
                cmd.Parameters.AddWithValue("@production_hour", u.production_hour);
                cmd.Parameters.AddWithValue("@production_hour_per_day", u.production_hour_per_day);
                cmd.Parameters.AddWithValue("@production_start_date", u.production_start_date);
                cmd.Parameters.AddWithValue("@production_end_date", u.production_end_date);
                cmd.Parameters.AddWithValue("@machine_id", u.machine_id);
                cmd.Parameters.AddWithValue("@family_with", u.family_with);

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

        public bool NewInsert(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_plan 
                            (" + planAddedDate + ","
                            + planAddedBy + ","
                            + planStatus + ","
                            + planNote + ","
                            + planCT + ","
                            + planPW + ","
                            + planItemPW + ","
                            + planRW + ","
                            + planCavity + ","
                            + planItemCavity + ","
                            + partCode + ","
                            + productionPurpose + ","
                            + materialCode + ","
                            + materialBagQty_1 + ","
                            + rawMaterialQty_1 + ","
                            + materialRecycleUse + ","
                            + colorMaterialCode + ","
                            + colorMaterialUsage + ","
                            + colorMaterialQty + ","
                            + targetQty + ","
                            + ableQty + ","
                            + productionDay + ","
                            + productionHour + ","
                            + productionHourPerDay + ","
                            + productionStartDate + ","
                            + productionEndDate + ","
                            + machineID + ","
                            + planMouldCode + ","
                            + familyWith + ") VALUES" +
                            "(@plan_added_date," +
                            "@plan_added_by," +
                            "@plan_status," +
                            "@plan_note," +
                            "@plan_ct," +
                            "@plan_pw," +
                            "@plan_item_pw," +
                            "@plan_rw," +
                            "@plan_cavity," +
                            "@plan_item_cavity," +
                            "@part_code," +
                            "@production_purpose," +
                            "@material_code," +
                            "@material_bag_qty," +
                            "@raw_material_qty," +
                            "@material_recycle_use," +
                            "@color_material_code," +
                            "@color_material_usage," +
                            "@color_material_qty," +
                            "@production_target_qty," +
                            "@production_able_produce_qty," +
                            "@production_day," +
                            "@production_hour," +
                            "@production_hour_per_day," +
                            "@production_start_date," +
                            "@production_end_date," +
                            "@machine_id," +
                            "@plan_mould_code," +
                            "@family_with)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_added_date", u.plan_added_date);
                cmd.Parameters.AddWithValue("@plan_added_by", u.plan_added_by);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);
                cmd.Parameters.AddWithValue("@plan_note", u.plan_remark);
                cmd.Parameters.AddWithValue("@plan_ct", u.plan_ct);
                cmd.Parameters.AddWithValue("@plan_pw", u.plan_pw);
                cmd.Parameters.AddWithValue("@plan_item_pw", u.plan_item_pw);
                cmd.Parameters.AddWithValue("@plan_rw", u.plan_rw);
                cmd.Parameters.AddWithValue("@plan_cavity", u.plan_cavity);
                cmd.Parameters.AddWithValue("@plan_item_cavity", u.plan_item_cavity);
                cmd.Parameters.AddWithValue("@part_code", u.part_code);
                cmd.Parameters.AddWithValue("@production_purpose", u.production_purpose);
                cmd.Parameters.AddWithValue("@material_code", u.material_code);
                cmd.Parameters.AddWithValue("@material_bag_qty", u.material_bag_qty);
                cmd.Parameters.AddWithValue("@raw_material_qty", u.raw_material_qty);
                cmd.Parameters.AddWithValue("@material_recycle_use", u.material_recycle_use);
                cmd.Parameters.AddWithValue("@color_material_code", u.color_material_code);
                cmd.Parameters.AddWithValue("@color_material_usage", u.color_material_usage);
                cmd.Parameters.AddWithValue("@color_material_qty", u.color_material_qty);
                cmd.Parameters.AddWithValue("@production_target_qty", u.production_target_qty);
                cmd.Parameters.AddWithValue("@production_able_produce_qty", u.production_able_produce_qty);
                cmd.Parameters.AddWithValue("@production_day", u.production_day);
                cmd.Parameters.AddWithValue("@production_hour", u.production_hour);
                cmd.Parameters.AddWithValue("@production_hour_per_day", u.production_hour_per_day);
                cmd.Parameters.AddWithValue("@production_start_date", u.production_start_date);
                cmd.Parameters.AddWithValue("@production_end_date", u.production_end_date);
                cmd.Parameters.AddWithValue("@machine_id", u.machine_id);
                cmd.Parameters.AddWithValue("@plan_mould_code", u.plan_mould_code);
                cmd.Parameters.AddWithValue("@family_with", u.family_with);

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

        public bool TargetQtyUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {

                String sql = @"UPDATE tbl_plan
                            SET "
                           + planNote + "=@plan_note,"
                           + materialBagQty_1 + "=@material_bag_qty,"
                           + materialRecycleUse + "=@material_recycle_use,"
                           + colorMaterialQty + "=@color_material_qty,"
                           + targetQty + "=@production_target_qty,"
                           + ableQty + "=@production_able_produce_qty,"
                           + productionDay + "=@production_day,"
                           + productionHour + "=@production_hour,"
                           + productionHourPerDay + "=@production_hour_per_day,"
                           + planUpdatedDate + "=@plan_updated_date,"
                           + planUpdatedby + "=@plan_updated_by" +
                           " WHERE plan_id=@plan_id";

                //String sql = @"UPDATE tbl_plan SET
                //                plan_produced=@plan_produced
                //                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_updated_date", u.plan_updated_date);
                cmd.Parameters.AddWithValue("@plan_updated_by", u.plan_updated_by);
                cmd.Parameters.AddWithValue("@material_bag_qty", u.material_bag_qty);
                cmd.Parameters.AddWithValue("@material_recycle_use", u.material_recycle_use);
                cmd.Parameters.AddWithValue("@color_material_qty", u.color_material_qty);
                cmd.Parameters.AddWithValue("@production_target_qty", u.production_target_qty);
                cmd.Parameters.AddWithValue("@production_able_produce_qty", u.production_able_produce_qty);
                cmd.Parameters.AddWithValue("@production_day", u.production_day);
                cmd.Parameters.AddWithValue("@production_hour", u.production_hour);
                cmd.Parameters.AddWithValue("@production_hour_per_day", u.production_hour_per_day);


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

        public bool TotalProducedUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                plan_produced=@plan_produced
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_produced", u.plan_produced);

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

        public bool RecordingUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                recording=@recording
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@recording", u.recording);

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

        public bool CheckedUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                checked=@Checked,
                                check_by=@CheckedBy,
                                check_date=@CheckedDate
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@Checked", u.Checked);
                cmd.Parameters.AddWithValue("@CheckedBy", u.CheckedBy);
                cmd.Parameters.AddWithValue("@CheckedDate", u.CheckedDate);

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

        public bool statusUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                plan_status=@plan_status,
                                plan_updated_date=@plan_updated_date,
                                plan_updated_by=@plan_updated_by
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);
                cmd.Parameters.AddWithValue("@plan_updated_date", u.plan_updated_date);
                cmd.Parameters.AddWithValue("@plan_updated_by", u.plan_updated_by);


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

        public bool remarkUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                plan_note=@plan_note,
                                plan_updated_date=@plan_updated_date,
                                plan_updated_by=@plan_updated_by
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_note", u.plan_remark);
                cmd.Parameters.AddWithValue("@plan_updated_date", u.plan_updated_date);
                cmd.Parameters.AddWithValue("@plan_updated_by", u.plan_updated_by);


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
        public bool statusAndRecordingUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                plan_status=@plan_status,
                                plan_updated_date=@plan_updated_date,
                                plan_updated_by=@plan_updated_by
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                // recording=@recording

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);
                cmd.Parameters.AddWithValue("@plan_updated_date", u.plan_updated_date);
                cmd.Parameters.AddWithValue("@plan_updated_by", u.plan_updated_by);
                //cmd.Parameters.AddWithValue("@recording", u.recording);

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

        public bool familyWithUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                family_with=@family_with,
                                plan_note=@plan_note,
                                plan_updated_date=@plan_updated_date,
                                plan_updated_by=@plan_updated_by
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@family_with", u.family_with);
                cmd.Parameters.AddWithValue("@plan_note", u.plan_remark);
                cmd.Parameters.AddWithValue("@plan_updated_date", u.plan_updated_date);
                cmd.Parameters.AddWithValue("@plan_updated_by", u.plan_updated_by);

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

        public bool scheduleDataUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                plan_status=@plan_status,family_with=@family_with, production_start_date=@production_start_date, production_end_date=@production_end_date,
                                plan_note=@plan_note,
                                plan_updated_date=@plan_updated_date,
                                plan_updated_by=@plan_updated_by
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);
                cmd.Parameters.AddWithValue("@family_with", u.family_with);
                cmd.Parameters.AddWithValue("@production_start_date", u.production_start_date);
                cmd.Parameters.AddWithValue("@production_end_date", u.production_end_date);
                cmd.Parameters.AddWithValue("@plan_note", u.plan_remark);
                cmd.Parameters.AddWithValue("@plan_updated_date", u.plan_updated_date);
                cmd.Parameters.AddWithValue("@plan_updated_by", u.plan_updated_by);

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

        public bool scheduleDataAndRecordingUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                plan_status=@plan_status,family_with=@family_with, production_start_date=@production_start_date, production_end_date=@production_end_date,
                                plan_note=@plan_note,
                                plan_updated_date=@plan_updated_date,
                                plan_updated_by=@plan_updated_by,
                                recording=@recording
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);
                cmd.Parameters.AddWithValue("@family_with", u.family_with);
                cmd.Parameters.AddWithValue("@production_start_date", u.production_start_date);
                cmd.Parameters.AddWithValue("@production_end_date", u.production_end_date);
                cmd.Parameters.AddWithValue("@plan_note", u.plan_remark);
                cmd.Parameters.AddWithValue("@plan_updated_date", u.plan_updated_date);
                cmd.Parameters.AddWithValue("@plan_updated_by", u.plan_updated_by);
                cmd.Parameters.AddWithValue("@recording", u.recording);

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

        public bool scheduleAndProDayUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                production_hour=@production_hour,production_day=@production_day, production_start_date=@production_start_date, production_end_date=@production_end_date,
                                production_hour_per_day=@production_hour_per_day,
                                plan_updated_date=@plan_updated_date,
                                plan_updated_by=@plan_updated_by
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@production_hour", u.production_hour);
                cmd.Parameters.AddWithValue("@production_day", u.production_day);
                cmd.Parameters.AddWithValue("@production_hour_per_day", u.production_hour_per_day);
                cmd.Parameters.AddWithValue("@production_start_date", u.production_start_date);
                cmd.Parameters.AddWithValue("@production_end_date", u.production_end_date);
                
                cmd.Parameters.AddWithValue("@plan_updated_date", u.plan_updated_date);
                cmd.Parameters.AddWithValue("@plan_updated_by", u.plan_updated_by);

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

        public DataTable itemSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_plan 
                                INNER JOIN tbl_item
                                ON tbl_plan.part_code = tbl_item.item_code
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id
                                WHERE tbl_plan.part_code LIKE '%" + keywords + "%' OR tbl_item.item_name LIKE '%" + keywords + "%'" +
                                "ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC";

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

        public DataTable idSearch(string planID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_plan 
                                INNER JOIN tbl_item
                                ON tbl_plan.part_code = tbl_item.item_code
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id 
                                WHERE tbl_plan.plan_id=@planID ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@planID", planID);
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

        public DataTable macIDSearch(string macID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_plan 
                                INNER JOIN tbl_item
                                ON tbl_plan.part_code = tbl_item.item_code
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id 
                                WHERE tbl_plan.machine_id = @macID ORDER BY production_start_date ASC";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@macID", macID);
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
    }
}
