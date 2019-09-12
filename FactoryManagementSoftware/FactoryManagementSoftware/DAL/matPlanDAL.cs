using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FactoryManagementSoftware.DAL
{
    class matPlanDAL
    {
        #region data string name getter

        public string MatCode { get; } = "mat_code";
        public string PlanToUse { get; } = "plan_to_use";
        public string PlanID { get; } = "plan_id";
        public string Active { get; } = "active";
        public string MatUsed { get; } = "mat_used";
        public string UpdatedDate { get; } = "updated_date";
        public string UpdatedBy { get; } = "updated_by";
        public string MatNote { get; } = "mat_note";

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
                String sql = @"SELECT * FROM tbl_matplan 
                                INNER JOIN tbl_item 
                                ON tbl_matplan.mat_code = tbl_item.item_code
                                INNER JOIN tbl_plan ON tbl_matplan.plan_id = tbl_plan.plan_id ORDER BY tbl_item.item_cat ASC, tbl_matplan.mat_code ASC";

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

        public bool Insert(matPlanBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_matplan 
                            (" + MatCode + ","
                            +PlanID + ","
                            + PlanToUse + ","
                            + MatUsed + ","
                            + Active + ","
                             + MatNote + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@mat_code,@plan_id," +
                            "@plan_to_use," +
                            "@mat_used," +
                            "@active," +
                            "@mat_note," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mat_code", u.mat_code);
                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@plan_to_use", u.plan_to_use);
                cmd.Parameters.AddWithValue("@mat_used", u.mat_used);
                cmd.Parameters.AddWithValue("@active", u.active);
                cmd.Parameters.AddWithValue("@mat_note", u.mat_note);
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

        #endregion

        #region Update data in Database

        public bool Update(matPlanBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_matplan 
                            SET "
                            + PlanToUse + "=@plan_to_use" +
                            " WHERE mat_code=@mat_code";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mat_code", u.mat_code);
                cmd.Parameters.AddWithValue("@plan_to_use", u.plan_to_use);
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

        public bool ActiveUpdate(matPlanBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_matplan 
                            SET "
                            + Active + "=@active," 
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@active", u.active);
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

        public bool MatUsedUpdate(matPlanBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_matplan 
                            SET "
                            + MatUsed + "=@mat_used" +
                            " WHERE mat_code=@mat_code AND plan_id=@plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mat_code", u.mat_code);
                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@mat_used", u.mat_used);

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
