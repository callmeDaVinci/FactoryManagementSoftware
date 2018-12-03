using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class orderActionDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        ordDAL dalOrd = new ordDAL();
        orderActionBLL uOrderAction = new orderActionBLL();

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
                String sql = "SELECT * FROM tbl_order_action";
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

        public DataTable Select(int orderID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_order_action WHERE ord_id = @orderID";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@orderID", orderID);

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

        public DataTable SelectByActionID(int actionID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_order_action WHERE order_action_id = @order_action_id";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@order_action_id", actionID);

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

        #endregion

        #region Insert Data in Database

        public bool Insert(orderActionBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_order_action 
                                (ord_id, added_date, added_by, action, action_detail, action_from, action_to, note, active)    
                         VALUES (@ord_id, @added_date, @added_by, @action, @action_detail, @action_from, @action_to, @note, @active)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_id", u.ord_id);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@action", u.action);
                cmd.Parameters.AddWithValue("@action_detail", u.action_detail);
                cmd.Parameters.AddWithValue("@action_from", u.action_from);
                cmd.Parameters.AddWithValue("@action_to", u.action_to);
                cmd.Parameters.AddWithValue("@note", u.note);
                cmd.Parameters.AddWithValue("@active", u.active);

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

        #region order action

        private void setActionData(int orderID, string action, string content, string from, string to , string note, bool ifActive)
        {
            uOrderAction.ord_id = orderID;
            uOrderAction.added_date = DateTime.Now;
            uOrderAction.added_by = 0;
            uOrderAction.action = action;
            uOrderAction.action_detail = content;
            uOrderAction.action_from = from;
            uOrderAction.action_to = to;
            uOrderAction.note = note;
            uOrderAction.active = ifActive;
        }

        //order request
        public bool orderRequest(int orderID, string note)
        {
            bool success = false;

            if (orderID < 0)//create a new action record after new order has created
            {
                //get the last record from tbl_ord
                DataTable lastRecord = dalOrd.lastRecordSelect();

                foreach (DataRow ord in lastRecord.Rows)
                {
                    uOrderAction.ord_id = Convert.ToInt32(ord["ord_id"]);
                    uOrderAction.added_date = Convert.ToDateTime(ord["ord_added_date"]);
                    uOrderAction.added_by = Convert.ToInt32(ord["ord_added_by"]); ;
                    uOrderAction.action = "REQUEST";
                    uOrderAction.action_detail = "";
                    uOrderAction.action_from = "";
                    uOrderAction.action_to = "";
                    uOrderAction.note = ord["ord_note"].ToString();
                    uOrderAction.active = true;
                }
            }
            else//action record exist,add another action
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, "REQUEST", "", "", "", note, true);
            }

            success = Insert(uOrderAction);

            if (!success)
            {
                MessageBox.Show("Failed to make order request action");
            }
            return success;
        }

        //order approve
        public bool orderApprove(int orderID, string note)
        {
            bool success = false;

             if(orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, "APPROVE", "", "", "", note, true);
            }

            success = Insert(uOrderAction);

            if (!success)
            {
                MessageBox.Show("Failed to make order request action");
            }

            return success;
        }

        //order receive
        public bool orderReceive(int orderID, string content, string from, string to, string note)
        {
            bool success = false;

            if (orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, "RECEIVE", content, from, to, note, true);
            }

            success = Insert(uOrderAction);

            if (!success)
            {
                MessageBox.Show("Failed to make order request action");
            }

            return success;
        }

        //edit order information, like change stock in/out qty, location of from/to, order qty , order required date, etc 
        public bool orderEdit(int orderID, int actionID, string content, string from, string to, string note)
        {
            bool success = false;

            if(orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, "EDIT", content, from, to, "", true);
            }

            if(actionID == -1)
            {
                success = Insert(uOrderAction);
            }
            else
            {
                //get data by action id
                success = true;
            }
            

            if (!success)
            {
                MessageBox.Show("Failed to make order request action");
            }
            return success;
        }

        //close order when fully received
        public bool orderClose(int orderID, string content, string note)
        {
            bool success = false;

            if (orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, "CLOSED", "", "", "", note, true);
            }

            success = Insert(uOrderAction);

            if (!success)
            {
                MessageBox.Show("Failed to make order request action");
            }

            return success;
        }

        //order cancel
        public bool orderCancel(int orderID, string note)
        {
            bool success = false;
          
            //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
            setActionData(orderID, "CANCEL", "", "", "", note, false);
  
            success = Insert(uOrderAction);

            if (!success)
            {
                MessageBox.Show("Failed to add order cancel action");
            }
            else
            {
                deactivatePreviousAction(orderID);
            }

            return success;
        }

        //return item before canceling the order when already received 
        public bool orderReturn(int orderID, string content, string from, string to, string note)
        {
            bool success = false;

            return success;
        }

        public void deactivate(int actionID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE [dbo].[tbl_order_action]
                                SET [active] = 0
                                WHERE order_action_id = @actionID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@actionID", actionID);
                

                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        
        private void deactivatePreviousAction(int orderID)
        {
            int actionID;

            DataTable dt = Select(orderID);
            
            if(dt.Rows.Count > 0)
            {
                foreach (DataRow action in dt.Rows)
                {
                    actionID = Convert.ToInt32(action["order_action_id"]);
                    deactivate(actionID);                   
                }
            }
        }

        public bool checkIfActive(int actionID)
        {
            bool result = false;

            DataTable dt = SelectByActionID(actionID);

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow action in dt.Rows)
                {
                    result = Convert.ToBoolean(action["active"]);
                }
            }

            return result;
        }

        #endregion
    }
}
