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
    class orderActionDAL
    {
        
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        ordDAL dalOrd = new ordDAL();
        orderActionBLL uOrderAction = new orderActionBLL();
        Tool tool = new Tool();
        Text text = new Text();

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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

        #region Insert Data in Database

        public bool Insert(orderActionBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_order_action 
                                (ord_id, trf_id, added_date, added_by, action, action_detail, action_from, action_to, note, active)    
                         VALUES (@ord_id, @trf_id, @added_date, @added_by, @action, @action_detail, @action_from, @action_to, @note, @active)";

                SqlCommand cmd = new SqlCommand(sql, conn);
            
                cmd.Parameters.AddWithValue("@ord_id", u.ord_id);
                cmd.Parameters.AddWithValue("@trf_id", u.trf_id);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion

        #region order action

        private void setActionData(int orderID,int trfID, string action, string content, string from, string to , string note, bool ifActive)
        {
            uOrderAction.ord_id = orderID;
            uOrderAction.trf_id = trfID;
            uOrderAction.added_date = DateTime.Now;
            uOrderAction.added_by = MainDashboard.USER_ID;
            uOrderAction.action = action;
            uOrderAction.action_detail = content;
            uOrderAction.action_from = from;
            uOrderAction.action_to = to;
            uOrderAction.note = note;
            uOrderAction.active = ifActive;

            
        }

        private void setActionData(int orderID, int trfID, string action, string content, string from, string to, string note, bool ifActive, string DONo, string LotNo)
        {
            uOrderAction.ord_id = orderID;
            uOrderAction.trf_id = trfID;
            uOrderAction.added_date = DateTime.Now;
            uOrderAction.added_by = MainDashboard.USER_ID;
            uOrderAction.action = action;
            uOrderAction.action_detail = content;
            uOrderAction.action_from = from;
            uOrderAction.action_to = to;
            uOrderAction.note = note;
            uOrderAction.active = ifActive;
            uOrderAction.do_no = DONo;
            uOrderAction.lot_no = LotNo;
        }

        //order request
        public bool orderRequest(int orderID, string note)
        {
            bool success = false;
            float orderQty = 0;
            string unit = "";
            string itemCode = "";
            string date = "";

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

                    orderQty = Convert.ToSingle(ord["ord_qty"]);
                    unit = ord["ord_unit"].ToString();
                    itemCode = ord["ord_item_code"].ToString();
                    date = ord["ord_required_date"].ToString();
                }
            }
            else//action record exist,add another action
            {
                setActionData(orderID, -1,"REQUEST", "", "", "", note, true);
            }

            success = Insert(uOrderAction);

            if (!success)
            {
                MessageBox.Show("Failed to make order request action(orderActionDAL)");
                tool.historyRecord(text.System, "Failed to make order request action", DateTime.Now, MainDashboard.USER_ID);
                
            }
            else
            {
                tool.historyRecord(text.OrderRequest, text.getOrderStatusChangeString(uOrderAction.ord_id), DateTime.Now, MainDashboard.USER_ID);
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
                setActionData(orderID,-1,"APPROVE/EDIT", "", "", "", note, true);
                success = Insert(uOrderAction);
            }

            if (!success)
            {
                MessageBox.Show("Failed to make order approve action.");
                tool.historyRecord(text.System, "Failed to make order approve action(orderActionDAL)", DateTime.Now, MainDashboard.USER_ID);

            }
            else
            {
                tool.historyRecord(text.OrderApprove, text.getOrderStatusChangeString(orderID), DateTime.Now, MainDashboard.USER_ID);
            }

            return success;
        }

        //order receive
        public bool orderReceive(int orderID, int trfID,string content, string from, string to, string note)
        {
            bool success = false;

            if (orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, trfID, "RECEIVE", content, from, to, note, true);
                success = Insert(uOrderAction);
            }

            if (!success)
            {
                MessageBox.Show("Failed to make order receive action");
                tool.historyRecord(text.System, "Failed to make order receive action(orderActionDAL)", DateTime.Now, MainDashboard.USER_ID);

            }
            else
            {
                tool.historyRecord(text.OrderReceive, text.getOrderStatusChangeString(orderID), DateTime.Now, MainDashboard.USER_ID);
            }

            return success;
        }

        //order follow up action
        public bool orderFollowUp(int orderID, string actionDate, string feedback, string from, string to)
        {
            bool success = false;

            if (orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, -1, "FOLLOW UP ACTION", actionDate+": "+feedback, from, to, "", true);
                success = Insert(uOrderAction);
            }

            if (!success)
            {
                MessageBox.Show("Failed to save order follow up action note");
                tool.historyRecord(text.System, "Failed to save order follow up action note(orderActionDAL)", DateTime.Now, MainDashboard.USER_ID);

            }
            else
            {
                tool.historyRecord(text.OrderFollowUp, text.getOrderStatusChangeString(orderID), DateTime.Now, MainDashboard.USER_ID);
            }

            return success;
        }

        //edit order information, like change stock in/out qty, location of from/to, order qty , order required date, etc 
        public bool orderEdit(int orderID, int trfID, int actionID, string content, string from, string to, string note)
        {
            bool success = false;
            if (orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, trfID,"EDIT", content, from, to, "", true);
            }
            if (actionID == -1)
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
                MessageBox.Show("Failed to make order edit action");
                tool.historyRecord(text.System, "Failed to make order edit action(orderActionDAL)", DateTime.Now, MainDashboard.USER_ID);

            }
            else
            {
                tool.historyRecord(text.OrderActionEdit, text.getOrderStatusChangeString(orderID), DateTime.Now, MainDashboard.USER_ID);
            }
            return success;
        }

        //close order when fully received
        public bool orderClose(int orderID, string note)
        {
            bool success = false;

            if (orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, -1,"CLOSED", "", "", "", note, true);
                success = Insert(uOrderAction);
            }

            if (!success)
            {
                MessageBox.Show("Failed to make order close action");
                tool.historyRecord(text.System, "Failed to make order close action(orderActionDAL)", DateTime.Now, MainDashboard.USER_ID);

            }
            else
            {
                tool.historyRecord(text.OrderClose, text.getOrderStatusChangeString(orderID), DateTime.Now, MainDashboard.USER_ID);
            }

            return success;
        }

        public bool orderReOpen(int orderID, string note)
        {
            bool success = false;

            if (orderID > 0)
            {
                //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
                setActionData(orderID, -1, "REOPEN", "", "", "", note, true);
                success = Insert(uOrderAction);
            }

            if (!success)
            {
                MessageBox.Show("Failed to make order reopen action");
                tool.historyRecord(text.System, "Failed to make reopen action(orderActionDAL)", DateTime.Now, MainDashboard.USER_ID);

            }
            else
            {
                tool.historyRecord(text.OrderClose, text.getOrderStatusChangeString(orderID), DateTime.Now, MainDashboard.USER_ID);
            }

            return success;
        }

        //order cancel
        public bool orderCancel(int orderID, int trfID, string note)
        {
            bool success = false;
          
            //(int orderID, string action, string content, string from, string to , string note, bool ifActive)
            setActionData(orderID, trfID, "CANCEL", "", "", "", note, false);
  
            success = Insert(uOrderAction);

            if (!success)
            {
                MessageBox.Show("Failed to add order cancel action");
                tool.historyRecord(text.System, "Failed to make order cancel action(orderActionDAL)", DateTime.Now, MainDashboard.USER_ID);

            }
            else
            {
                deactivatePreviousAction(orderID);
                tool.historyRecord(text.OrderCancel, text.getOrderStatusChangeString(orderID), DateTime.Now, MainDashboard.USER_ID);

            }

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
