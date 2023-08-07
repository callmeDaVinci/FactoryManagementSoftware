using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.DAL
{
    class ProductionRecordDAL
    {
        #region data string name getter
        public string SheetID { get; } = "sheet_id";
        public string JobNo { get; } = "plan_id";
        public string ProDate { get; } = "production_date";
        public string Shift { get; } = "shift";
        public string ProLotNo { get; } = "production_lot_no";
        public string MacNo { get; } = "mac_no";
        public string RawMatLotNo { get; } = "raw_mat_lot_no";
        public string ColorMatLotNo { get; } = "color_mat_lot_no";
        public string MeterStart { get; } = "meter_start";
        public string MeterEnd { get; } = "meter_end";
        public string LastShiftBalance { get; } = "last_shift_balance";
        public string CurrentShiftBalance { get; } = "current_shift_balance";
        public string FullBox { get; } = "full_box";
        public string TotalProduced { get; } = "total_produced";
        public string TotalReject { get; } = "total_reject";
        public string TotalActualReject { get; } = "total_actual_reject";
        public string RejectPercentage { get; } = "reject_percentage";
        public string UpdatedDate { get; } = "updated_date";
        public string UpdatedBy { get; } = "updated_by";
        public string Active { get; } = "active";
        public string PackagingQty { get; } = "packaging_qty";
        public string PackagingCode { get; } = "packaging_code";
        public string PackagingMax { get; } = "packaging_max";
        public string PackagingStockOut { get; } = "packaging_stock_out";


        public string ParentQty { get; } = "parent_qty";

        public string DefectRemark { get; } = "defect_remark";
        public string RejectQty { get; } = "reject_qty";

        public string ProTime { get; } = "time";
        public string ProOperator { get; } = "operator";
        public string ProMeterReading { get; } = "meter_reading";
        public string ParentCode { get; } = "parent_code";
        public string Note { get; } = "note";
        public string directIn { get; } = "directIn";
        public string directOut { get; } = "directOut";
        #endregion

        #region variable/class object declare

        static readonly string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

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
                String sql = @"SELECT * FROM tbl_production_record 
                                INNER JOIN tbl_plan 
                                ON tbl_plan.plan_id = tbl_production_record.plan_id 
                                ORDER BY tbl_production_record.plan_id ASC, tbl_production_record.production_date ASC, tbl_production_record.sheet_id ASC";

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

        public DataTable SelectActiveDailyJobRecordOnly()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_production_record 
                                INNER JOIN tbl_plan 
                                ON tbl_plan.plan_id = tbl_production_record.plan_id 
                                WHERE tbl_plan.recording = 1
                                ORDER BY tbl_production_record.plan_id ASC, tbl_production_record.production_date ASC, tbl_production_record.sheet_id ASC";

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

        public DataTable SelectActiveAndRunningJob()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_production_record 
                                INNER JOIN tbl_plan 
                                ON tbl_plan.plan_id = tbl_production_record.plan_id 
                                WHERE tbl_plan.recording = 1
                                ORDER BY tbl_production_record.plan_id ASC, tbl_production_record.production_date ASC, tbl_production_record.sheet_id ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@running",new Text().planning_status_running);
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
        public DataTable SelectWithItemInfo()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_production_record 
                                INNER JOIN tbl_plan 
                                ON tbl_plan.plan_id = tbl_production_record.plan_id
                                INNER JOIN tbl_item
                                ON tbl_plan.part_code = tbl_item.item_code
                                WHERE tbl_production_record.active = @active
                                ORDER BY tbl_production_record.production_date DESC, tbl_production_record.sheet_id DESC";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@active", 1);

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

        public DataTable SelectWithItemInfoSortByItem()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_production_record 
                                INNER JOIN tbl_plan 
                                ON tbl_plan.plan_id = tbl_production_record.plan_id
                                INNER JOIN tbl_item
                                ON tbl_plan.part_code = tbl_item.item_code
                                WHERE tbl_production_record.active = @active
                                ORDER BY tbl_plan.part_code ASC";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@active", 1);

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

        public DataTable ProductionRecordSelect(ProductionRecordBLL u)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_production_record 
                                INNER JOIN tbl_plan 
                                ON tbl_plan.plan_id = tbl_production_record.plan_id WHERE tbl_production_record.sheet_id = @sheet_id";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);

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

        public DataTable MeterRecordSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_production_meter_reading";

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

        public DataTable MeterRecordSelect(ProductionRecordBLL u)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_production_meter_reading 
                               WHERE sheet_id = @sheet_id";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);

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

        public DataTable DefectRemarkRecordSelect(ProductionRecordBLL u)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_production_defect_remark
                               WHERE sheet_id = @sheet_id";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);

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

        public DataTable PackagingRecordSelect(int sheetID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_pro_packaging
                               WHERE sheet_id = @sheet_id";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("@sheet_id", sheetID);

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

        public DataTable ParentRecordSelect(int sheetID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_pro_parent
                               WHERE sheet_id = @sheet_id";

                //INNER JOIN tbl_production_meter_reading  ON tbl_production_record.sheet_id = tbl_production_meter_reading.sheet_id
                //ORDER BY tbl_plan.machine_id ASC, tbl_plan.production_start_date ASC, tbl_plan.production_End_date ASC, tbl_production_record.sheet_id ASC
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("@sheet_id", sheetID);

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

        public DataTable LastRecordSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT TOP 1 * FROM tbl_production_record ORDER BY sheet_id DESC";
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

        public int GetLastInsertedSheetID()
        {
            int ID = -1;

            DataTable lastInsertedData = LastRecordSelect();

            foreach (DataRow row in lastInsertedData.Rows)
            {
                ID = Convert.ToInt32(row[SheetID]);
            }

            return ID;
        }

        #endregion

        #region Insert Data in Database

        public bool InsertProductionRecord(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            u.active = true;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_production_record 
                            (" + JobNo + ","
                            + ProDate + ","
                            + Shift + ","
                            + ProLotNo + ","
                            + MacNo + ","
                            + RawMatLotNo + ","
                            + ColorMatLotNo + ","
                            + MeterStart + ","
                            + MeterEnd + ","
                            + LastShiftBalance + ","
                            + CurrentShiftBalance + ","
                            + FullBox + ","
                            + TotalProduced + ","
                            + TotalReject + ","
                            + TotalActualReject + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ","
                            + Active + ","
                            + PackagingQty + ","
                            + PackagingCode + ","
                            + directIn + ","
                             + directOut + ","
                            + ParentCode + ","
                            + Note + ") VALUES" +
                            "(@plan_id," +
                            "@production_date," +
                            "@shift," +
                            "@production_lot_no," +
                            "@mac_no," +
                            "@raw_mat_lot_no," +
                            "@color_mat_lot_no," +
                            "@meter_start," +
                            "@meter_end," +
                            "@last_shift_balance," +
                            "@current_shift_balance," +
                            "@full_box," +
                            "@total_produced," +
                            "@total_reject," +
                            "@total_actual_reject," +
                            "@updated_date," +
                             "@updated_by," +
                            "@active," +
                            "@packaging_qty," +
                             "@packaging_code," +
                              "@directIn," +
                               "@directOut," +
                                "@parent_code," +
                            "@note)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@production_date", u.production_date);
                cmd.Parameters.AddWithValue("@shift", u.shift);
                cmd.Parameters.AddWithValue("@production_lot_no", u.production_lot_no);
                cmd.Parameters.AddWithValue("@mac_no", u.mac_no);
                cmd.Parameters.AddWithValue("@raw_mat_lot_no", u.raw_mat_lot_no);
                cmd.Parameters.AddWithValue("@color_mat_lot_no", u.color_mat_lot_no);
                cmd.Parameters.AddWithValue("@meter_start", u.meter_start);
                cmd.Parameters.AddWithValue("@meter_end", u.meter_end);
                cmd.Parameters.AddWithValue("@last_shift_balance", u.last_shift_balance);
                cmd.Parameters.AddWithValue("@current_shift_balance", u.current_shift_balance);
                cmd.Parameters.AddWithValue("@full_box", u.full_box);
                cmd.Parameters.AddWithValue("@total_produced", u.total_produced);
                cmd.Parameters.AddWithValue("@total_reject", u.total_reject);
                cmd.Parameters.AddWithValue("@total_actual_reject", u.total_actual_reject);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
                cmd.Parameters.AddWithValue("@active", u.active);
                cmd.Parameters.AddWithValue("@packaging_code", u.packaging_code);
                cmd.Parameters.AddWithValue("@packaging_qty", u.packaging_qty);
                cmd.Parameters.AddWithValue("@parent_code", u.parent_code);
                cmd.Parameters.AddWithValue("@note", u.note);
                cmd.Parameters.AddWithValue("@directIn", u.directIn);
                cmd.Parameters.AddWithValue("@directOut", u.directOut);

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

        public bool InsertSheetMeter(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_production_meter_reading
                            (" + SheetID + ","
                            + ProTime + ","
                            + ProOperator + ","
                            + ProMeterReading + ") VALUES" +
                            "(@sheet_id," +
                            "@time," +
                            "@production_operator," +
                            "@meter_reading)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@time", u.time);
                cmd.Parameters.AddWithValue("@production_operator", u.production_operator);
                cmd.Parameters.AddWithValue("@meter_reading", u.meter_reading);

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

        public bool InsertSheetDefectRemark(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_production_defect_remark
                            (" + SheetID + ","
                            + ProTime + ","
                            + DefectRemark + ","
                            + RejectQty + ") VALUES" +
                            "(@sheet_id," +
                            "@time," +
                            "@defect_remark," +
                            "@reject_qty)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@time", u.time);
                cmd.Parameters.AddWithValue("@defect_remark", u.defect_remark);
                cmd.Parameters.AddWithValue("@reject_qty", u.reject_qty);

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

        public bool InsertProductionPackaging(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_pro_packaging
                            (" + SheetID + ","
                            + PackagingCode + ","
                            + PackagingQty + ","
                            + PackagingMax + ","
                            + PackagingStockOut + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@sheet_id," +
                            "@packaging_code," +
                            "@packaging_qty," +
                             "@packaging_max," +
                             "@packaging_stock_out," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@packaging_code", u.packaging_code);
                cmd.Parameters.AddWithValue("@packaging_qty", u.packaging_qty);
                cmd.Parameters.AddWithValue("@packaging_max", u.packaging_max);
                cmd.Parameters.AddWithValue("@packaging_stock_out", u.packaging_stock_out);
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

        public bool InsertProductionParentData(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_pro_parent
                            (" + SheetID + ","
                            + ParentCode + ","
                            + ParentQty + ") VALUES" +
                            "(@sheet_id," +
                            "@parent_code," +
                             "@parent_qty," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@parent_code", u.parent_code);
                cmd.Parameters.AddWithValue("@parent_qty", u.parent_qty);
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

        public bool TempCommand(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_production_record 
                            SET "
                            + Active + "=@active";

                SqlCommand cmd = new SqlCommand(sql, conn);

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

        public bool ProductionRecordUpdate(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            u.active = true;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_production_record 
                            SET "
                            + JobNo + "=@plan_id,"
                            + ProDate + "=@production_date,"
                            + Shift + "=@shift,"
                            + ProLotNo + "=@production_lot_no,"
                            + MacNo + "=@mac_no,"
                            + RawMatLotNo + "=@raw_mat_lot_no,"
                            + ColorMatLotNo + "=@color_mat_lot_no,"
                            + MeterStart + "=@meter_start,"
                            + MeterEnd + "=@meter_end,"
                            + LastShiftBalance + "=@last_shift_balance,"
                            + CurrentShiftBalance + "=@current_shift_balance,"
                            + FullBox + "=@full_box,"
                            + TotalProduced + "=@total_produced,"
                            + TotalReject + "=@total_reject,"
                            + TotalActualReject + "=@total_actual_reject,"
                            + Active + "=@active,"
                            + PackagingCode + "=@packaging_code,"
                            + PackagingQty + "=@packaging_qty,"
                            + ParentCode + "=@parent_code,"
                            + Note + "=@note,"
                            + directIn + "=@directIn,"
                             + directOut + "=@directOut,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE sheet_id=@sheet_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@production_date", u.production_date);
                cmd.Parameters.AddWithValue("@shift", u.shift);
                cmd.Parameters.AddWithValue("@production_lot_no", u.production_lot_no);
                cmd.Parameters.AddWithValue("@mac_no", u.mac_no);
                cmd.Parameters.AddWithValue("@raw_mat_lot_no", u.raw_mat_lot_no);
                cmd.Parameters.AddWithValue("@color_mat_lot_no", u.color_mat_lot_no);
                cmd.Parameters.AddWithValue("@meter_start", u.meter_start);
                cmd.Parameters.AddWithValue("@meter_end", u.meter_end);
                cmd.Parameters.AddWithValue("@last_shift_balance", u.last_shift_balance);
                cmd.Parameters.AddWithValue("@current_shift_balance", u.current_shift_balance);
                cmd.Parameters.AddWithValue("@full_box", u.full_box);
                cmd.Parameters.AddWithValue("@total_produced", u.total_produced);
                cmd.Parameters.AddWithValue("@total_reject", u.total_reject);
                cmd.Parameters.AddWithValue("@total_actual_reject", u.total_actual_reject);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
                cmd.Parameters.AddWithValue("@active", u.active);
                cmd.Parameters.AddWithValue("@packaging_code", u.packaging_code);
                cmd.Parameters.AddWithValue("@packaging_qty", u.packaging_qty);
                cmd.Parameters.AddWithValue("@parent_code", u.parent_code);
                cmd.Parameters.AddWithValue("@note", u.note);
                cmd.Parameters.AddWithValue("@directIn", u.directIn);
                cmd.Parameters.AddWithValue("@directOut", u.directOut);

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

        public bool ReactiveSheetData(ProductionRecordBLL u)
        {
            u.active = true;
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_production_record 
                            SET "
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by,"
                            + Active + "=@active" +
                            " WHERE sheet_id=@sheet_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
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

        public bool RemoveSheetData(ProductionRecordBLL u)
        {
            u.active = false;
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_production_record 
                            SET "
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by,"
                            + Active + "=@active" +
                            " WHERE sheet_id=@sheet_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
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

        public bool SheetMeterUpdate(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_production_meter_reading
                            SET "
                            + ProMeterReading + "=@meter_reading,"
                            + ProOperator + "=@production_operator" +
                            " WHERE sheet_id=@sheet_id AND time=@time";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@time", u.time);
                cmd.Parameters.AddWithValue("@production_operator", u.production_operator);
                cmd.Parameters.AddWithValue("@meter_reading", u.meter_reading);
               
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

        public bool ChangePlanID(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            u.active = true;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_production_record 
                            SET "
                            + JobNo + "=@new_plan_id,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE plan_id=@old_plan_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@new_plan_id", u.new_plan_id);
                cmd.Parameters.AddWithValue("@old_plan_id", u.old_plan_id);
            
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

        #region Delete data in Database

        public bool DeleteMeterData(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_production_meter_reading WHERE sheet_id=@sheet_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);

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

        public bool DeleteDefectData(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_production_defect_remark WHERE sheet_id=@sheet_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);

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

        public bool DeletePackagingData(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_pro_packaging WHERE sheet_id=@sheet_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);

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

        public bool DeleteParentData(ProductionRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_pro_parent WHERE sheet_id=@sheet_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);

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
