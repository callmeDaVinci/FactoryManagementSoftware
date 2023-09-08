using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FactoryManagementSoftware.Module;
using System.Windows.Forms;
using FactoryManagementSoftware.UI;

namespace FactoryManagementSoftware.DAL
{
    class habitDAL
    {
        #region data string name getter

        public string BelongTo { get; } = "belong_to";
        public string HabitName { get; } = "habit_name";
        public string HabitData { get; } = "habit_data";
        public string AddedDate { get; } = "added_date";
        public string AddedBy { get; } = "added_by";
        public string UpdatedDate { get; } = "updated_date";
        public string UpdatedBy { get; } = "updated_by";

        #endregion

        #region variable/class object declare

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        Text text = new Text();
        Tool tool = new Tool();

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
                String sql = @"SELECT * FROM tbl_habit ";
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

        public DataTable SelectOrderByBelongTo()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_habit ORDER BY belong_to ASC";
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

        public DataTable SelectOrderByHabitName()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_habit ORDER BY habit_name ASC";
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

        public bool Insert(habitBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_habit 
                            (" + BelongTo + ","
                            + HabitName + ","
                            + HabitData + ","
                            + AddedDate + ","
                            + AddedBy + ") VALUES" +
                            "(@belong_to," +
                            "@habit_name," +
                            "@habit_data," +
                            "@added_date," +
                            "@added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@belong_to", u.belong_to);
                cmd.Parameters.AddWithValue("@habit_name", u.habit_name);
                cmd.Parameters.AddWithValue("@habit_data", u.habit_data);
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

        #endregion

        #region Update data in Database

        public bool Update(habitBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_habit SET
                                habit_data=@habit_data,
                                updated_date=@updated_date,
                                updated_by=@updated_by
                                WHERE belong_to=@belong_to AND habit_name=@habit_name";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@belong_to", u.belong_to);
                cmd.Parameters.AddWithValue("@habit_name", u.habit_name);
                cmd.Parameters.AddWithValue("@habit_data", u.habit_data);
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

        #endregion

        #region Search

        public DataTable HabitSearch(string belongTo)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_habit WHERE belong_to = @belong_to";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@belong_to", belongTo);
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

        public DataTable HabitSearch(string belongTo, string habitName)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_habit WHERE belong_to = @belong_to AND habit_name = @habit_name";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@belong_to", belongTo);
                cmd.Parameters.AddWithValue("@habit_name", habitName);
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

        public DataTable HabitSearch(string belongTo, string habitName_1, string habitName_2)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_habit WHERE belong_to = @belong_to AND (habit_name = @habit_name_1 OR habit_name = @habit_name_2)";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@belong_to", belongTo);
                cmd.Parameters.AddWithValue("@habit_name_1", habitName_1);
                cmd.Parameters.AddWithValue("@habit_name_2", habitName_2);
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

        public DataTable HabitSearch(string belongTo, string habitName_1,int userID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_habit WHERE belong_to = @belong_to AND habit_name = @habit_name_1 AND added_by = @AddedBy";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@belong_to", belongTo);
                cmd.Parameters.AddWithValue("@habit_name_1", habitName_1);
                cmd.Parameters.AddWithValue("@AddedBy", userID);
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

        #region History Record

        public bool HabitInsertAndHistoryRecord(habitBLL u)
        {
            DataTable dt_Record = HabitSearch(u.belong_to, u.habit_name);
            DateTime date = DateTime.Now;
            string oldData= "NULL";
            bool success = false;

            if (dt_Record.Rows.Count > 0)
            {
                //update
                u.updated_date = u.added_date;
                u.updated_by = u.added_by;

                foreach(DataRow row in dt_Record.Rows)
                {
                    oldData = row[HabitData].ToString();
                }
                success = Update(u);
            }
            else
            {
                //insert
                success = Insert(u);
            }

            if (!success)
            {
                MessageBox.Show("Failed to insert habit data!");
                tool.historyRecord(text.System, "Failed to insert habit data! (habitDAL : 364)", date, MainDashboard.USER_ID);
            }
            else
            {
                tool.historyRecord(text.habit_insert, "["+u.belong_to+"]"+u.habit_name+": "+oldData+" --> "+u.habit_data, date, MainDashboard.USER_ID);
            }

            return success;
        }

        public bool HabitInsertAndHistoryRecordWithUserID(habitBLL u)
        {
            DataTable dt_Record = HabitSearch(u.belong_to, u.habit_name, u.added_by);
            DateTime date = DateTime.Now;
            string oldData = "NULL";
            bool success = false;

            if (dt_Record.Rows.Count > 0)
            {
                //update
                u.updated_date = u.added_date;
                u.updated_by = u.added_by;

                foreach (DataRow row in dt_Record.Rows)
                {
                    oldData = row[HabitData].ToString();
                }
                success = Update(u);
            }
            else
            {
                //insert
                success = Insert(u);
            }

            if (!success)
            {
                MessageBox.Show("Failed to insert habit data!");
                tool.historyRecord(text.System, "Failed to insert habit data! (habitDAL : 364)", date, MainDashboard.USER_ID);
            }
            else
            {
                tool.historyRecord(text.habit_insert, "[" + u.belong_to + "]" + u.habit_name + ": " + oldData + " --> " + u.habit_data, date, MainDashboard.USER_ID);
            }

            return success;
        }

        #endregion
    }
}
