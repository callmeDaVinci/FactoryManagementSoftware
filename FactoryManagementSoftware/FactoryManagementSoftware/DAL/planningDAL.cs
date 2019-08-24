using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FactoryManagementSoftware.DAL
{
    class planningDAL
    {
        #region data string name getter
        public string planID { get; } = "plan_id";
        public string planAddedDate { get; } = "plan_added_date";
        public string planAddedBy { get; } = "plan_added_by";
        public string planUpdatedDate { get; } = "plan_updated_date";
        public string planUpdatedby { get; } = "plan_updated_by";
        public string planStatus { get; } = "plan_status";
        public string planNote { get; } = "plan_note";

        public string productionPurpose { get; } = "production_purpose";

        public string partCode { get; } = "part_code";

        public string materialCode { get; } = "material_code";
        public string materialBagQty { get; } = "material_bag_qty";
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

        public string machineID { get; } = "machine_id";

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
                                INNER JOIN tbl_mac ON tbl_plan.machine_id = tbl_mac.mac_id ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date";
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
                            + partCode + ","
                            + productionPurpose + ","
                            + materialCode + ","
                            + materialBagQty + ","
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
                            + machineID + ") VALUES" +
                            "(@plan_added_date," +
                            "@plan_added_by," +
                            "@plan_status," +
                            "@plan_note," +
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
                            "@machine_id)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_added_date", u.plan_added_date);
                cmd.Parameters.AddWithValue("@plan_added_by", u.plan_added_by);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);
                cmd.Parameters.AddWithValue("@plan_note", u.plan_note);
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

        public bool statusUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                plan_status=@plan_status
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);

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

        public bool statusAndDateUpdate(PlanningBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_plan SET
                                plan_status=@plan_status, production_start_date=@production_start_date, production_end_date=@production_end_date
                                WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_status", u.plan_status);
                cmd.Parameters.AddWithValue("@production_start_date", u.production_start_date);
                cmd.Parameters.AddWithValue("@production_end_date", u.production_end_date);

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
                                "ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date";

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

        public DataTable idSearch(string keywords)
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
                                WHERE tbl_plan.plan_id LIKE '%" + keywords + "%'" +
                                "ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date";

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
