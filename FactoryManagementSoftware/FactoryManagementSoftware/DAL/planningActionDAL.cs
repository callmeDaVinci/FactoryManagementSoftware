using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.UI;
using FactoryManagementSoftware.Module;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace FactoryManagementSoftware.DAL
{
    class planningActionDAL
    {
        #region data string name getter

        public string ActionID { get; } = "planning_action_id";
        public string PlanID { get; } = "planning_id";
        public string ActionAddedDate { get; } = "added_date";
        public string ActionAddedBy { get; } = "added_by";
        public string Action { get; } = "action";
        public string ActionDetail { get; } = "action_detail";
        public string ActionFrom { get; } = "action_from";
        public string ActionTo { get; } = "action_to";
        public string ActioNote { get; } = "note";
        #endregion

        #region variable/class object declare

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        planningActionBLL uPlanningAction = new planningActionBLL();
        planningDAL dalPlanning = new planningDAL();
        Tool tool = new Tool();
        Text text = new Text();

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
                String sql = "SELECT * FROM tbl_planning_action";
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

        public DataTable SelectByPlanningID(int planningID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_planning_action WHERE planning_id = @planningID ORDER BY added_date DESC";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@planningID", planningID);

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

        public DataTable SelectByActionID(int actionID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_planning_action WHERE planning_action_id = @actionID";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@actionID", actionID);

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
                Tool tool = new Tool(); tool.saveToText(ex);
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

        public bool Insert(planningActionBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_planning_action 
                                (planning_id, added_date, added_by, action, action_detail, action_from, action_to, note)    
                         VALUES (@planning_id, @added_date, @added_by, @action, @action_detail, @action_from, @action_to, @note)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@planning_id", u.planning_id);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@action", u.action);
                cmd.Parameters.AddWithValue("@action_detail", u.action_detail);
                cmd.Parameters.AddWithValue("@action_from", u.action_from);
                cmd.Parameters.AddWithValue("@action_to", u.action_to);
                cmd.Parameters.AddWithValue("@note", u.note);

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

        #region planning action

        //production plan add
        public bool planningAdd(PlanningBLL u)
        {
            bool success = dalPlanning.Insert(u);

            if (!success)
            {
                MessageBox.Show("Failed to add new production plan!");
                tool.historyRecord(text.System, "Failed to add new production plan!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                tool.historyRecord(text.plan_Added, text.getNewPlanningDetail(u), DateTime.Now, MainDashboard.USER_ID);
                
                //get the last record from tbl_planning
                DataTable lastRecord = dalPlanning.lastRecordSelect();

                foreach (DataRow row in lastRecord.Rows)
                {
                    uPlanningAction.planning_id = Convert.ToInt32(row[dalPlanning.planID]);
                    uPlanningAction.added_date = Convert.ToDateTime(row[dalPlanning.planAddedDate]);
                    uPlanningAction.added_by = Convert.ToInt32(row[dalPlanning.planAddedBy]); ;
                    uPlanningAction.action = text.plan_Added;
                    uPlanningAction.action_detail = text.getNewPlanningDetail(u);
                    uPlanningAction.action_from = "";
                    uPlanningAction.action_to = "";
                    uPlanningAction.note = row[dalPlanning.planNote].ToString() + " [Cavity: " + row[dalPlanning.planCavity].ToString() + "; PW(shot): " + row[dalPlanning.planPW].ToString() + " ;RW(shot): " + row[dalPlanning.planRW].ToString()+"]";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if(!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningAdd)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningAdd)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

            }

            return success;
        }

        //production plan status change
        public bool planningRemarkChange(PlanningBLL u, string oldNote)
        {
            bool success = dalPlanning.remarkUpdate(u);

            if (!success)
            {
                MessageBox.Show("Failed to change production plan's remark!");
                tool.historyRecord(text.System, "Failed to change production plan's remark!", DateTime.Now, MainDashboard.USER_ID);
            }         
            else
            {
                tool.historyRecord(text.plan_remark_change, "PLAN's remark " + u.plan_id + ": " + oldNote + " --> "+u.plan_remark, DateTime.Now, MainDashboard.USER_ID);

                uPlanningAction.planning_id = u.plan_id;
                uPlanningAction.added_date = u.plan_updated_date;
                uPlanningAction.added_by = u.plan_updated_by ;
                uPlanningAction.action = text.plan_remark_change;
                uPlanningAction.action_detail = "";
                uPlanningAction.action_from = oldNote;
                uPlanningAction.action_to = u.plan_remark;
                uPlanningAction.note = "";

                bool actionSaveSuccess = Insert(uPlanningAction);

                if (!actionSaveSuccess)
                {
                    MessageBox.Show("Failed to save planning action data (planningActionDAL_planningStatusChange)");
                    tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningStatusChange)", DateTime.Now, MainDashboard.USER_ID);
                }
            }

            return success;
        }

        public bool planningStatusChange(PlanningBLL u, string oldStatus)
        {
            bool success;

            if (u.plan_status == text.planning_status_cancelled || u.plan_status == text.planning_status_running)
            {
                success = dalPlanning.statusAndRecordingUpdate(u);
            }
            else
            {
                success = dalPlanning.statusUpdate(u);
            }

            if (!success)
            {
                MessageBox.Show("Failed to change production plan's status!");
                tool.historyRecord(text.System, "Failed to change production plan's status!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                tool.historyRecord(text.plan_status_change, "PLAN ID " + u.plan_id + ": " + oldStatus + " --> " + u.plan_status, DateTime.Now, MainDashboard.USER_ID);

                uPlanningAction.planning_id = u.plan_id;
                uPlanningAction.added_date = u.plan_updated_date;
                uPlanningAction.added_by = u.plan_updated_by;
                uPlanningAction.action = text.plan_status_change;
                uPlanningAction.action_detail = "";
                uPlanningAction.action_from = oldStatus;
                uPlanningAction.action_to = u.plan_status;
                uPlanningAction.note = "";

                bool actionSaveSuccess = Insert(uPlanningAction);

                if (!actionSaveSuccess)
                {
                    MessageBox.Show("Failed to save planning action data (planningActionDAL_planningStatusChange)");
                    tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningStatusChange)", DateTime.Now, MainDashboard.USER_ID);
                }
            }

            return success;
        }


        //production plan schedule and pro day change
        public bool planningScheduleAndProDayChange(PlanningBLL u, string oldProDay, string oldProHour, string oldProHourPerDay, string oldStart, string oldEnd)
        {
            bool success = dalPlanning.scheduleAndProDayUpdate(u);

            if (!success)
            {
                MessageBox.Show("Failed to change production plan's schedule or pro day!");
                tool.historyRecord(text.System, "Failed to change production plan's schedule or pro day!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                if(u.production_start_date.Date.ToString() != oldStart)
                {
                    tool.historyRecord(text.plan_schedule_change, "PLAN ID " + u.plan_id + " Start Date: " + oldStart + " --> " + u.production_start_date.Date.ToString(), DateTime.Now, MainDashboard.USER_ID);

                    uPlanningAction.planning_id = u.plan_id;
                    uPlanningAction.added_date = u.plan_updated_date;
                    uPlanningAction.added_by = u.plan_updated_by;
                    uPlanningAction.action = text.plan_schedule_change;
                    uPlanningAction.action_detail = "";
                    uPlanningAction.action_from = oldStart;
                    uPlanningAction.action_to = u.production_start_date.ToShortDateString();
                    uPlanningAction.note = "";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

                if (u.production_end_date.Date.ToString() != oldEnd)
                {
                    tool.historyRecord(text.plan_schedule_change, "PLAN ID " + u.plan_id + " End Date: " + oldEnd + " --> " + u.production_end_date.Date.ToString(), DateTime.Now, MainDashboard.USER_ID);

                    uPlanningAction.planning_id = u.plan_id;
                    uPlanningAction.added_date = u.plan_updated_date;
                    uPlanningAction.added_by = u.plan_updated_by;
                    uPlanningAction.action = text.plan_schedule_change;
                    uPlanningAction.action_detail = "";
                    uPlanningAction.action_from = oldEnd;
                    uPlanningAction.action_to = u.production_end_date.ToShortDateString();
                    uPlanningAction.note = "";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

                if (u.production_day.ToString() != oldProDay)
                {
                    tool.historyRecord(text.plan_proday_change, "PLAN ID " + u.plan_id + " Pro Day: " + oldProDay + " --> " + u.production_day.ToString(), DateTime.Now, MainDashboard.USER_ID);

                    uPlanningAction.planning_id = u.plan_id;
                    uPlanningAction.added_date = u.plan_updated_date;
                    uPlanningAction.added_by = u.plan_updated_by;
                    uPlanningAction.action = text.plan_proday_change;
                    uPlanningAction.action_detail = "";
                    uPlanningAction.action_from = oldProDay;
                    uPlanningAction.action_to = u.production_day.ToString();
                    uPlanningAction.note = "";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

                if (u.production_hour.ToString() != oldProHour)
                {
                    tool.historyRecord(text.plan_prohour_change, "PLAN ID " + u.plan_id + " Pro Hour: " + oldProHour + " --> " + u.production_hour.ToString(), DateTime.Now, MainDashboard.USER_ID);

                    uPlanningAction.planning_id = u.plan_id;
                    uPlanningAction.added_date = u.plan_updated_date;
                    uPlanningAction.added_by = u.plan_updated_by;
                    uPlanningAction.action = text.plan_prohour_change;
                    uPlanningAction.action_detail = "";
                    uPlanningAction.action_from = oldProHour;
                    uPlanningAction.action_to = u.production_hour.ToString();
                    uPlanningAction.note = "";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

                if (u.production_hour_per_day.ToString() != oldProHourPerDay)
                {
                    tool.historyRecord(text.plan_prohourperday_change, "PLAN ID " + u.plan_id + " Pro Hour Per Day: " + oldProHourPerDay + " --> " + u.production_hour_per_day.ToString(), DateTime.Now, MainDashboard.USER_ID);

                    uPlanningAction.planning_id = u.plan_id;
                    uPlanningAction.added_date = u.plan_updated_date;
                    uPlanningAction.added_by = u.plan_updated_by;
                    uPlanningAction.action = text.plan_prohourperday_change;
                    uPlanningAction.action_detail = "";
                    uPlanningAction.action_from = oldProHourPerDay;
                    uPlanningAction.action_to = u.production_hour_per_day.ToString();
                    uPlanningAction.note = "";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningScheduleAndProDayChange)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

            }

            return success;
        }

        //production plan status change
        public bool planningFamilyWithChange(PlanningBLL u, string oldFamilyWith)
        {
            bool success = dalPlanning.familyWithUpdate(u);

            if (!success)
            {
                MessageBox.Show("Failed to change production plan's family with data!");
                tool.historyRecord(text.System, "Failed to change production plan's family with data!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                tool.historyRecord(text.plan_family_with_change, "PLAN ID " + u.plan_id + ": " + oldFamilyWith + " --> " + u.family_with, DateTime.Now, MainDashboard.USER_ID);

                uPlanningAction.planning_id = u.plan_id;
                uPlanningAction.added_date = u.plan_updated_date;
                uPlanningAction.added_by = u.plan_updated_by;
                uPlanningAction.action = text.plan_family_with_change;
                uPlanningAction.action_detail = "";
                uPlanningAction.action_from = oldFamilyWith;
                uPlanningAction.action_to = u.family_with.ToString();
                uPlanningAction.note = "";

                bool actionSaveSuccess = Insert(uPlanningAction);

                if (!actionSaveSuccess)
                {
                    MessageBox.Show("Failed to save planning action data (planningActionDAL_planningStatusChange)");
                    tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningFamilyWithChange)", DateTime.Now, MainDashboard.USER_ID);
                }
            }

            return success;
        }

        //production plan status & schedule change
        public bool planningStatusAndScheduleChange(PlanningBLL u, string presentStatus, DateTime oldStart, DateTime oldEnd, int oldFamilyWith)
        {
            bool success;

            if (u.plan_status == text.planning_status_cancelled || u.plan_status == text.planning_status_running)
            {
                success = dalPlanning.scheduleDataAndRecordingUpdate(u);
            }
            else
            {
                success = dalPlanning.scheduleDataUpdate(u);
            }

            if (!success)
            {
                MessageBox.Show("Failed to change production plan's status & schedule!");
                tool.historyRecord(text.System, "Failed to change production plan's status & schedule!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                if(!presentStatus.Equals(u.plan_status))
                {
                    tool.historyRecord(text.plan_status_change, "PLAN ID " + u.plan_id + ": " + presentStatus + " --> " + u.plan_status, DateTime.Now, MainDashboard.USER_ID);

                    uPlanningAction.planning_id = u.plan_id;
                    uPlanningAction.added_date = u.plan_updated_date;
                    uPlanningAction.added_by = u.plan_updated_by;
                    uPlanningAction.action = text.plan_status_change;
                    uPlanningAction.action_detail = "";
                    uPlanningAction.action_from = presentStatus;
                    uPlanningAction.action_to = u.plan_status;
                    uPlanningAction.note = "";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningStatusChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningStatusChange)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

                if (oldStart != u.production_start_date || oldEnd != u.production_end_date)
                {
                    tool.historyRecord(text.plan_schedule_change, "PLAN ID "+u.plan_id+": "+oldStart.ToShortDateString() + "-" + oldEnd.ToShortDateString() + " --> " + u.production_start_date.ToShortDateString() + "-" +u.production_end_date.ToShortDateString(), DateTime.Now, MainDashboard.USER_ID);

                    uPlanningAction.planning_id = u.plan_id;
                    uPlanningAction.added_date = u.plan_updated_date;
                    uPlanningAction.added_by = u.plan_updated_by;
                    uPlanningAction.action = text.plan_schedule_change;
                    uPlanningAction.action_detail = "";
                    string from = oldStart.ToShortDateString() + "-" + oldEnd.ToShortDateString();
                    string to = u.production_start_date.ToShortDateString() + "-" + u.production_end_date.ToShortDateString();

                    uPlanningAction.action_from = from;
                    uPlanningAction.action_to = to;
                    uPlanningAction.note = "";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningScheduleChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningscheduleChange)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

                if (u.family_with != oldFamilyWith)
                {
                    tool.historyRecord(text.plan_family_with_change, "PLAN ID " + u.plan_id + ": " + oldFamilyWith + " --> " + u.family_with, DateTime.Now, MainDashboard.USER_ID);

                    uPlanningAction.planning_id = u.plan_id;
                    uPlanningAction.added_date = u.plan_updated_date;
                    uPlanningAction.added_by = u.plan_updated_by;
                    uPlanningAction.action = text.plan_family_with_change;
                    uPlanningAction.action_detail = "";
                    uPlanningAction.action_from = oldFamilyWith.ToString();
                    uPlanningAction.action_to = u.family_with.ToString();
                    uPlanningAction.note = "";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningStatusChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningFamilyWithChange(396))", DateTime.Now, MainDashboard.USER_ID);
                    }
                }
            }

            return success;
        }

        //production plan Target Qty Change
        public bool planningTargetQtyChange(PlanningBLL u)
        {
            bool success = dalPlanning.TargetQtyUpdate(u);

            if (!success)
            {
                MessageBox.Show("Failed to update target qty!");
                tool.historyRecord(text.System, "Failed to update target qty!", DateTime.Now, MainDashboard.USER_ID);
            }
            else
            {
                tool.historyRecord(text.plan_Target_Qty_Change, text.getPlanningTargetQtyChangeDetail(u), DateTime.Now, MainDashboard.USER_ID);

                //get the last record from tbl_planning
                DataTable lastRecord = dalPlanning.lastRecordSelect();

                foreach (DataRow row in lastRecord.Rows)
                {
                    uPlanningAction.planning_id = Convert.ToInt32(row[dalPlanning.planID]);
                    uPlanningAction.added_date = Convert.ToDateTime(row[dalPlanning.planAddedDate]);
                    uPlanningAction.added_by = Convert.ToInt32(row[dalPlanning.planAddedBy]); 
                    uPlanningAction.action = text.plan_Target_Qty_Change;
                    uPlanningAction.action_detail = text.getPlanningTargetQtyChangeDetail(u);
                    uPlanningAction.action_from = "";
                    uPlanningAction.action_to = "";
                    uPlanningAction.note = row[dalPlanning.planNote].ToString() + " [Cavity: " + row[dalPlanning.planCavity].ToString() + "; PW(shot): " + row[dalPlanning.planPW].ToString() + " ;RW(shot): " + row[dalPlanning.planRW].ToString() + "]";

                    bool actionSaveSuccess = Insert(uPlanningAction);

                    if (!actionSaveSuccess)
                    {
                        MessageBox.Show("Failed to save planning action data (planningActionDAL_planningTargetQtyChange)");
                        tool.historyRecord(text.System, "Failed to save planning action data (planningActionDAL_planningTargetQtyChange)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }

            }

            return success;
        }

        #endregion

    }
}
