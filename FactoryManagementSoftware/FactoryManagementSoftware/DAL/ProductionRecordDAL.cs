using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FactoryManagementSoftware.DAL
{
    class ProductionRecordDAL
    {
        #region data string name getter
        public string SheetID { get; } = "sheet_id";
        public string PlanID { get; } = "plan_id";
        public string ProDate { get; } = "production_date";
        public string Shift { get; } = "shift";
        public string ProLotNo { get; } = "production_lot_no";
        public string RawMatLotNo { get; } = "raw_mat_lot_no";
        public string ColorMatLotNo { get; } = "color_mat_lot_no";
        public string MeterStart { get; } = "meter_start";
        public string MeterEnd { get; } = "meter_end";
        public string LastShiftBalance { get; } = "last_shift_balance";
        public string CurrentShiftBalance { get; } = "current_shift_balance";
        public string FullBox { get; } = "full_box";
        public string TotalProduced { get; } = "total_produced";
        public string TotalReject { get; } = "total_reject";
        public string RejectPercentage { get; } = "reject_percentage";
        public string UpdatedDate { get; } = "updated_date";
        public string UpdatedBy { get; } = "updated_by";
        public string Active { get; } = "active";
        public string PackagingQty { get; } = "packaging_qty";
        public string PackagingCode { get; } = "packaging_code";

        public string ProTime { get; } = "time";
        public string ProOperator { get; } = "operator";
        public string ProMeterReading { get; } = "meter_reading";


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
                                ON tbl_plan.plan_id = tbl_production_record.plan_id";

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
                            (" + PlanID + ","
                            + ProDate + ","
                            + Shift + ","
                            + ProLotNo + ","
                            + RawMatLotNo + ","
                            + ColorMatLotNo + ","
                            + MeterStart + ","
                            + MeterEnd + ","
                            + LastShiftBalance + ","
                            + CurrentShiftBalance + ","
                            + FullBox + ","
                            + TotalProduced + ","
                            + TotalReject + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ","
                            + Active + ","
                            + PackagingQty + ","
                            + PackagingCode + ") VALUES" +
                            "(@plan_id," +
                            "@production_date," +
                            "@shift," +
                            "@production_lot_no," +
                            "@raw_mat_lot_no," +
                            "@color_mat_lot_no," +
                            "@meter_start," +
                            "@meter_end," +
                            "@last_shift_balance," +
                            "@current_shift_balance," +
                            "@full_box," +
                            "@total_produced," +
                            "@total_reject," +
                            "@updated_date," +
                             "@updated_by," +
                            "@active," +
                            "@packaging_qty," +
                            "@packaging_code)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@production_date", u.production_date);
                cmd.Parameters.AddWithValue("@shift", u.shift);
                cmd.Parameters.AddWithValue("@production_lot_no", u.production_lot_no);
                cmd.Parameters.AddWithValue("@raw_mat_lot_no", u.raw_mat_lot_no);
                cmd.Parameters.AddWithValue("@color_mat_lot_no", u.color_mat_lot_no);
                cmd.Parameters.AddWithValue("@meter_start", u.meter_start);
                cmd.Parameters.AddWithValue("@meter_end", u.meter_end);
                cmd.Parameters.AddWithValue("@last_shift_balance", u.last_shift_balance);
                cmd.Parameters.AddWithValue("@current_shift_balance", u.current_shift_balance);
                cmd.Parameters.AddWithValue("@full_box", u.full_box);
                cmd.Parameters.AddWithValue("@total_produced", u.total_produced);
                cmd.Parameters.AddWithValue("@total_reject", u.total_reject);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
                cmd.Parameters.AddWithValue("@active", u.active);
                cmd.Parameters.AddWithValue("@packaging_code", u.packaging_code);
                cmd.Parameters.AddWithValue("@packaging_qty", u.packaging_qty);

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
                            + PlanID + "=@plan_id,"
                            + ProDate + "=@production_date,"
                            + Shift + "=@shift,"
                            + ProLotNo + "=@production_lot_no,"
                            + RawMatLotNo + "=@raw_mat_lot_no,"
                            + ColorMatLotNo + "=@color_mat_lot_no,"
                            + MeterStart + "=@meter_start,"
                            + MeterEnd + "=@meter_end,"
                            + LastShiftBalance + "=@last_shift_balance,"
                            + CurrentShiftBalance + "=@current_shift_balance,"
                            + FullBox + "=@full_box,"
                            + TotalProduced + "=@total_produced,"
                            + TotalReject + "=@total_reject,"
                            + Active + "=@active,"
                            + PackagingCode + "=@packaging_code,"
                            + PackagingQty + "=@packaging_qty,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE sheet_id=@sheet_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@sheet_id", u.sheet_id);
                cmd.Parameters.AddWithValue("@plan_id", u.plan_id);
                cmd.Parameters.AddWithValue("@production_date", u.production_date);
                cmd.Parameters.AddWithValue("@shift", u.shift);
                cmd.Parameters.AddWithValue("@production_lot_no", u.production_lot_no);
                cmd.Parameters.AddWithValue("@raw_mat_lot_no", u.raw_mat_lot_no);
                cmd.Parameters.AddWithValue("@color_mat_lot_no", u.color_mat_lot_no);
                cmd.Parameters.AddWithValue("@meter_start", u.meter_start);
                cmd.Parameters.AddWithValue("@meter_end", u.meter_end);
                cmd.Parameters.AddWithValue("@last_shift_balance", u.last_shift_balance);
                cmd.Parameters.AddWithValue("@current_shift_balance", u.current_shift_balance);
                cmd.Parameters.AddWithValue("@full_box", u.full_box);
                cmd.Parameters.AddWithValue("@total_produced", u.total_produced);
                cmd.Parameters.AddWithValue("@total_reject", u.total_reject);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
                cmd.Parameters.AddWithValue("@active", u.active);
                cmd.Parameters.AddWithValue("@packaging_code", u.packaging_code);
                cmd.Parameters.AddWithValue("@packaging_qty", u.packaging_qty);

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

        #endregion
    }
}
