using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using System.Data;

namespace FactoryManagementSoftware.UI
{
    public partial class MainDashboard : Form
    {
        static public bool itemFormOpen = false;
        static public bool facFormOpen = false;
        static public bool custFormOpen = false;
        static public bool supplierFormOpen = false;
        static public bool inOutFormOpen = false;
        static public bool catFormOpen = false;
        static public bool ordFormOpen = false;
        static public bool NewOrdFormOpen = false;
        static public bool dataFormOpen = false;
        static public bool MouldFormOpen = false;
        static public bool itemCustFormOpen = false;
        static public bool forecastInputFormOpen = false;
        static public bool forecastReportInputFormOpen = false;
        static public bool joinFormOpen = false;
        static public bool MaterialUsedReportFormOpen = false;
        static public bool stockReportFormOpen = false;
        static public bool userFormOpen = false;
        static public bool historyFormOpen = false;
        static public bool InOutReportFormOpen = false;
        static public bool PMMAFormOpen = false;
        static public bool ProductionFormOpen = false;
        static public bool NEWProductionFormOpen = false;
        static public bool MacScheduleFormOpen = false;
        static public bool DailyJobSheetFormOpen = false;
        static public bool NewDailyJobSheetFormOpen = false;
        static public bool NewDailyJobSheetFormOpenVer3 = false;
        static public bool ProductionReportFormOpen = false;
        static public bool SBBFormOpen = false;
        static public bool OUGFormOpen = false;
        static public bool SBBDeliveredFormOpen = false;
        static public bool OUGPOFormOpen = false;
        static public bool NewItemListFormOpen = false;


        static public int USER_ID = -1;
        static public bool MACHINE_SCHEDULE_SWITCH_TO_OLD_VERSION = false;
        static public readonly int ACTION_LVL_ONE = 1;
        static public readonly int ACTION_LVL_TWO = 2;
        static public readonly int ACTION_LVL_THREE = 3;
        static public readonly int ACTION_LVL_FOUR = 4;
        static public readonly int ACTION_LVL_FIVE = 5;
        static public string myconnstrng;
        userDAL dalUser = new userDAL();
        Text text = new Text();
        Tool tool = new Tool();

        public static MainDashboard Instance { get; private set; }

        public MainDashboard(int userID)
        {

            InitializeComponent();
            Instance = this;
            USER_ID = userID;
            MACHINE_SCHEDULE_SWITCH_TO_OLD_VERSION = false;
            int userPermission = dalUser.getPermissionLevel(USER_ID);
            string userName = dalUser.getUsername(USER_ID);

            usernameToolStripMenuItem.Text = userName;

            pOToolStripMenuItem.Visible = false;

            if (userPermission >= ACTION_LVL_FIVE)
            {
                sBBToolStripMenuItem.Visible = true;
            }

            if (userPermission >= ACTION_LVL_FOUR)
            {
                sBBToolStripMenuItem.Visible = true;
                adminToolStripMenuItem.Visible = true;
                orderToolStripMenuItem1.Visible = true;
                macScheduleToolStripMenuItem.Visible = true;

            }
            else if(userPermission >= ACTION_LVL_TWO)
            {
                forecastToolStripMenuItem.Visible = true;
                adminToolStripMenuItem.Visible = false;
                orderToolStripMenuItem1.Visible = true;
                macScheduleToolStripMenuItem.Visible = true;
            }
            else
            {
                pMMAToolStripMenuItem.Visible = false;
                forecastToolStripMenuItem.Visible = false;
                adminToolStripMenuItem.Visible = false;
                orderToolStripMenuItem1.Visible = true;
                macScheduleToolStripMenuItem.Visible = true;
            }


            myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;


            if (myconnstrng == text.DB_Semenyih && userPermission < ACTION_LVL_FIVE)//|| myconnstrng == text.DB_JunPC
            {
                //Semenyih
                pMMAToolStripMenuItem.Visible = false;
                forecastToolStripMenuItem.Visible = false;
               // macScheduleToolStripMenuItem.Visible = false;
                //dAILYToolStripMenuItem.Visible = false;
                //orderToolStripMenuItem1.Visible = false;
            }
            else if(userPermission < ACTION_LVL_FIVE)
            {
                sBBToolStripMenuItem.Visible = false;
            }
        }

        
        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!itemFormOpen)
            {
                
                frmItem item = new frmItem();
                item.MdiParent = this;
                item.StartPosition = FormStartPosition.CenterScreen;
                item.WindowState = FormWindowState.Maximized;
                item.Show();
                itemFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmItem>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmItem>().First().BringToFront();
                }
            }
        }

        private void facToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!facFormOpen)
            {
                frmFac fac = new frmFac();
                fac.MdiParent = this;
                fac.StartPosition = FormStartPosition.CenterScreen;
                fac.WindowState = FormWindowState.Maximized;
                fac.Show();

                facFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmFac>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmFac>().First().BringToFront();
                }
            }
        }

        private void custToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!custFormOpen)
            {
                frmCust item = new frmCust();
                item.MdiParent = this;
                item.StartPosition = FormStartPosition.CenterScreen;
                item.WindowState = FormWindowState.Maximized;
                item.Show();
                custFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmCust>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmCust>().First().BringToFront();
                }
            }
        }

        private void inOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!inOutFormOpen)
            {
                frmInOut inOut = new frmInOut();
                inOut.MdiParent = this;
                inOut.StartPosition = FormStartPosition.CenterScreen;
                inOut.WindowState = FormWindowState.Maximized;
                inOut.Show();
                inOutFormOpen = true; 
            }
            else
            {
                if (Application.OpenForms.OfType<frmInOut>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmInOut>().First().BringToFront();
                }
            }
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!catFormOpen)
            {
                frmCat cat = new frmCat();
                cat.MdiParent = this;
                cat.StartPosition = FormStartPosition.CenterScreen;
                cat.WindowState = FormWindowState.Maximized;
                cat.Show();
                catFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmCat>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmCat>().First().BringToFront();
                }
            }
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!dataFormOpen)
            {
                frmData data = new frmData();
                data.MdiParent = this;
                data.StartPosition = FormStartPosition.CenterScreen;
                data.WindowState = FormWindowState.Maximized;
                data.Show();
                dataFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmData>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmData>().First().BringToFront();
                }
            }
        }

        private void itemCustToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!itemCustFormOpen)
            {
                frmItemCust frm = new frmItemCust();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                itemCustFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmItemCust>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmItemCust>().First().BringToFront();
                }
            }
        }

        private void dataInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!forecastInputFormOpen)
            {
                frmForecast frm = new frmForecast();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                forecastInputFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmForecast>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmForecast>().First().BringToFront();
                }
            }
        }

        private void forecastReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!forecastReportInputFormOpen)
            {
                frmLoading.ShowLoadingScreen();

                frmForecastReport_NEW frm = new frmForecastReport_NEW();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                forecastReportInputFormOpen = true;
                frmLoading.CloseForm();

            }
            else
            {
                if (Application.OpenForms.OfType<frmForecastReport_NEW>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmForecastReport_NEW>().First().BringToFront();
                }
            }

        }

        private void itemJoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!joinFormOpen)
            {
                frmJoin frm = new frmJoin();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                joinFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmJoin>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmJoin>().First().BringToFront();
                }
            }
        }

        private void MainDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            tool.historyRecord(text.LogOut, text.Success, DateTime.Now, USER_ID);


            // Application.Exit();
            frmLogIn frm = new frmLogIn(dalUser.getUsername(USER_ID));
            frm.Show();
        }

        private void test()
        {
            //if (!ordFormOpen)
            //{
            //    frmLoading.ShowLoadingScreen();
            //    frmOrderAlert_NEW ord = new frmOrderAlert_NEW
            //    {
            //        MdiParent = this,
            //        StartPosition = FormStartPosition.CenterScreen,
            //        WindowState = FormWindowState.Maximized
            //    };
            //    ord.Show();
            //    ordFormOpen = true;
            //    frmLoading.CloseForm();
            //}
            //else
            //{
            //    if (Application.OpenForms.OfType<frmOrderAlert_NEW>().Count() == 1)
            //    {
            //        Application.OpenForms.OfType<frmOrderAlert_NEW>().First().BringToFront();
            //    }
            //}
            if (!ordFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmOrder ord = new frmOrder
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                ord.Show();
                ordFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOrder>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOrder>().First().BringToFront();
                }
            }
        }
        private void orderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!NewOrdFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmOrderAlert_NEW ord = new frmOrderAlert_NEW
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                ord.Show();
                NewOrdFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOrderAlert_NEW>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOrderAlert_NEW>().First().BringToFront();
                }
            }
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!inOutFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmInOutVer2 inOut = new frmInOutVer2
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                inOut.Show();
                inOutFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmInOutVer2>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmInOutVer2>().First().BringToFront();
                }
            }
        }

        private void materialUsedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myconnstrng == text.DB_Semenyih)//|| myconnstrng == text.DB_JunPC
            {
                //Semenyih
                if (!MaterialUsedReportFormOpen)
                {
                    frmMaterialUsedReport frm = new frmMaterialUsedReport();
                    frm.MdiParent = this;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    MaterialUsedReportFormOpen = true;
                }
                else
                {
                    if (Application.OpenForms.OfType<frmMaterialUsedReport>().Count() == 1)
                    {
                        Application.OpenForms.OfType<frmMaterialUsedReport>().First().BringToFront();
                    }
                }
            }
            else
            {
                //OUG
                if (!MaterialUsedReportFormOpen)
                {
                    frmMaterialUsedReport_NEW frm = new frmMaterialUsedReport_NEW();
                    frm.MdiParent = this;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    MaterialUsedReportFormOpen = true;
                }
                else
                {
                    if (Application.OpenForms.OfType<frmMaterialUsedReport_NEW>().Count() == 1)
                    {
                        Application.OpenForms.OfType<frmMaterialUsedReport_NEW>().First().BringToFront();
                    }
                }

            }

           
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!stockReportFormOpen)
            {
                frmStockReport frm = new frmStockReport();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                stockReportFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmStockReport>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmStockReport>().First().BringToFront();
                }
            }
        }

        private void forecastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!forecastInputFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmForecast_NEW frm = new frmForecast_NEW
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                frm.Show();
                forecastInputFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmForecast_NEW>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmForecast_NEW>().First().BringToFront();
                }
            }
        }

        

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            //stockCountListDAL dalStockCountList =  new stockCountListDAL();

            //dalStockCountList.CreateTable();

            //stockCountListBLL uStockCountList = new stockCountListBLL();
            //uStockCountList.list_description = "SEMENYIH STOCK(OUG ITEM)";
            //uStockCountList.default_factory_tbl_code = 10;
            //uStockCountList.isRemoved = false;
            //uStockCountList.remark = "Update test";
            //uStockCountList.updated_by = MainDashboard.USER_ID;
            //uStockCountList.updated_date = DateTime.Now;
            //uStockCountList.tbl_code = 1;

            ////dalStockCountList.Insert(uStockCountList);
            //dalStockCountList.Update(uStockCountList);

            //stockCountListItemDAL dalStockCountListItem = new stockCountListItemDAL();

            //dalStockCountListItem.CreateTable();

            //stockCountListItemBLL uStockCountItem = new stockCountListItemBLL();
            //uStockCountItem.list_tbl_code = 1;
            //uStockCountItem.item_code = "ABS 700 314";
            //uStockCountItem.factory_tbl_code = 10;
            //uStockCountItem.default_out_tbl_code = 7;
            //uStockCountItem.default_in_tbl_code = 3;
            //uStockCountItem.count_unit = text.Unit_Bag;
            //uStockCountItem.unit_conversion_rate = 25;
            //uStockCountItem.remark = "Update Test";
            //uStockCountItem.isRemoved = false;
            //uStockCountItem.updated_date = DateTime.Now;
            //uStockCountItem.updated_by = MainDashboard.USER_ID;
            //uStockCountItem.tbl_code = 1;

            ////dalStockCountListItem.Insert(uStockCountItem);
            //dalStockCountListItem.Update(uStockCountItem);

            // Create an instance of the DAL for stock count list item records
            //stockCountListItemRecordDAL dalStockCountListItemRecord = new stockCountListItemRecordDAL();

            //// Call the CreateTable method to ensure the table exists
            //dalStockCountListItemRecord.CreateTable();

            //// Create an instance of the BLL for a stock count list item record
            //stockCountListItemRecordBLL uStockCountItemRecord = new stockCountListItemRecordBLL();
            //uStockCountItemRecord.list_item_tbl_code = 1; // Set to the list item table code you mentioned
            //uStockCountItemRecord.total_unit_qty = 1; 
            //uStockCountItemRecord.count_unit = "Bag"; // Assuming "text.Unit_Bag" is equivalent to "Bag"
            //uStockCountItemRecord.unit_conversion_rate = 25.0; // Example conversion rate
            //uStockCountItemRecord.total_pcs = 25; // Example total pieces calculated
            //uStockCountItemRecord.remark = "Update stock count entry"; // Remark for the record
            //uStockCountItemRecord.updated_by = MainDashboard.USER_ID; // User ID from MainDashboard
            //uStockCountItemRecord.updated_date = DateTime.Now; // Current date and time
            //uStockCountItemRecord.stock_count_date = DateTime.Now; // Assuming stock count date is also now
            //uStockCountItemRecord.tbl_code = 1;

            //// Call the Insert method to add the record to the database
            ////dalStockCountListItemRecord.Insert(uStockCountItemRecord);
            //dalStockCountListItemRecord.Update(uStockCountItemRecord);


            if (myconnstrng == text.DB_Semenyih || myconnstrng == text.DB_JunPC)//|| myconnstrng == text.DB_JunPC
            {
                //Semenyih
                if (!SBBFormOpen)
                {
                    frmLoading.ShowLoadingScreen();
                    frmSBB frm = new frmSBB();
                    frm.MdiParent = this;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    SBBFormOpen = true;
                    frmLoading.CloseForm();
                }
                else
                {
                    if (Application.OpenForms.OfType<frmSBB>().Count() == 1)
                    {
                        Application.OpenForms.OfType<frmSBB>().First().BringToFront();
                    }
                }
            }
            else
            {
                
                //OUG
                if (!inOutFormOpen)
                {
                    frmLoading.ShowLoadingScreen();
                    frmInOutVer2 frm = new frmInOutVer2();
                    frm.MdiParent = this;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    inOutFormOpen = true;
                    frmLoading.CloseForm();
                }
                else
                {
                    if (Application.OpenForms.OfType<frmInOutVer2>().Count() == 1)
                    {
                        Application.OpenForms.OfType<frmInOutVer2>().First().BringToFront();
                    }
                }

            }


        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!userFormOpen)
            {
                frmUser frm = new frmUser();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                userFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmUser>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmUser>().First().BringToFront();
                }
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!historyFormOpen)
            {
                frmHistory frm = new frmHistory();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                historyFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmHistory>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmHistory>().First().BringToFront();
                }
            }
        }

        private void inOutReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!InOutReportFormOpen)
            {
                frmLoading.ShowLoadingScreen();

                frmInOutReport_NEW frm = new frmInOutReport_NEW();
                frm.MdiParent = this;
                
                frm.StartPosition = FormStartPosition.CenterScreen;
                //frm.WindowState = FormWindowState.Minimized;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                InOutReportFormOpen = true;
                frmLoading.CloseForm();

            }
            else
            {
                if (Application.OpenForms.OfType<frmInOutReport_NEW>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmInOutReport_NEW>().First().BringToFront();
                }
            }
        }

        private void pMMAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PMMAFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmNewPMMA frm = new frmNewPMMA
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                frm.Show();
                PMMAFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmNewPMMA>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmNewPMMA>().First().BringToFront();
                }
            }
        }

        private void MainDashboard_Move(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MACHINE_SCHEDULE_SWITCH_TO_OLD_VERSION)
            {
                if (!ProductionFormOpen)
                {
                    frmLoading.ShowLoadingScreen();
                    frmMachineSchedule frm = new frmMachineSchedule
                    {
                        MdiParent = this,
                        StartPosition = FormStartPosition.CenterScreen,
                        WindowState = FormWindowState.Maximized
                    };
                    frm.Show();
                    ProductionFormOpen = true;
                    frmLoading.CloseForm();
                }
                else
                {
                    if (Application.OpenForms.OfType<frmMachineSchedule>().Count() >= 1)
                    {
                        Application.OpenForms.OfType<frmMachineSchedule>().First().BringToFront();
                    }
                }
            }
            else
            {
                if (!NEWProductionFormOpen)
                {
                    frmLoading.ShowLoadingScreen();
                    frmMachineScheduleVer2 frm = new frmMachineScheduleVer2
                    {
                        MdiParent = this,
                        StartPosition = FormStartPosition.CenterScreen,
                        WindowState = FormWindowState.Maximized
                    };
                    frm.Show();
                    NEWProductionFormOpen = true;
                    frmLoading.CloseForm();
                }
                else
                {
                    if (Application.OpenForms.OfType<frmMachineScheduleVer2>().Count() >= 1)
                    {
                        Application.OpenForms.OfType<frmMachineScheduleVer2>().First().BringToFront();
                    }
                }
            }
          

           
        }

        private void dAILYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

            if (myconnstrng == text.DB_Semenyih || myconnstrng == text.DB_JunPC)
            {
                if (!NewDailyJobSheetFormOpenVer3)
                {
                    frmLoading.ShowLoadingScreen();

                    frmProductionRecordVer3 frm = new frmProductionRecordVer3();
                    frm.MdiParent = this;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();

                    NewDailyJobSheetFormOpenVer3 = true;

                    frmLoading.CloseForm();

                }
                else
                {
                    if (Application.OpenForms.OfType<frmProductionRecordVer3>().Count() == 1)
                    {
                        Application.OpenForms.OfType<frmProductionRecordVer3>().First().BringToFront();
                    }

                }
            }
            else
            {
                if (!NewDailyJobSheetFormOpen)
                {
                    frmLoading.ShowLoadingScreen();

                    frmProductionRecordNewV2 frm = new frmProductionRecordNewV2();
                    frm.MdiParent = this;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    NewDailyJobSheetFormOpen = true;

                    frmLoading.CloseForm();

                }
                else
                {
                    if (Application.OpenForms.OfType<frmProductionRecordNewV2>().Count() == 1)
                    {
                        Application.OpenForms.OfType<frmProductionRecordNewV2>().First().BringToFront();
                    }

                }
            }



        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ProductionReportFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmProductionReportV2 frm = new frmProductionReportV2();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                ProductionReportFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmProductionReportV2>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmProductionReportV2>().First().BringToFront();
                }
            }
        }

        private void sPPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SBBFormOpen)
            {
                //frmLoading.ShowLoadingScreen();
                frmSBB frm = new frmSBB();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                SBBFormOpen = true;
                //frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmSBB>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmSBB>().First().BringToFront();
                }

                //frmLoading.ShowLoadingScreen();
                //frmSBB frm = new frmSBB();
                //frm.MdiParent = this;
                //frm.StartPosition = FormStartPosition.CenterScreen;
                //frm.WindowState = FormWindowState.Maximized;
                //frm.Show();
                //SBBFormOpen = true;
                //frmLoading.CloseForm();
            }

            //if (Application.OpenForms.OfType<frmSBB>().Count() == 1)
            //{
            //    Application.OpenForms.OfType<frmSBB>().First().BringToFront();
            //}
            //else
            //{
            //    frmLoading.ShowLoadingScreen();
            //    frmSBB frm = new frmSBB();
            //    frm.MdiParent = this;
            //    frm.StartPosition = FormStartPosition.CenterScreen;
            //    frm.WindowState = FormWindowState.Maximized;
            //    frm.Show();
            //    SPPFormOpen = true;
            //    frmLoading.CloseForm();
            //}
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolTip_Popup(object sender, PopupEventArgs e)
        {

        }

        private void sBBDeliveredReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SBBDeliveredFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmSBBDeliveredReport frm = new frmSBBDeliveredReport();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                SBBDeliveredFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmSBBDeliveredReport>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmSBBDeliveredReport>().First().BringToFront();
                }
            }
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mouldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!MouldFormOpen)
            {
                frmSBBMould frm = new frmSBBMould();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                MouldFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmSBBMould>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmSBBMould>().First().BringToFront();
                }
            }
        }

        private void pOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!OUGPOFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmOUGPOList frm = new frmOUGPOList();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                OUGPOFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOUGPOList>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOUGPOList>().First().BringToFront();
                }
            }
        }

        private void newItemListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!NewItemListFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmItemMasterList frm = new frmItemMasterList();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                NewItemListFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmItemMasterList>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmItemMasterList>().First().BringToFront();
                }
            }
        }

        private void iTEMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!NewItemListFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmItemMasterList frm = new frmItemMasterList();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                NewItemListFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmItemMasterList>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmItemMasterList>().First().BringToFront();
                }
            }
        }

        private void oLDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!ordFormOpen)
            //{
            //    frmLoading.ShowLoadingScreen();
            //    frmOrderAlert_NEW ord = new frmOrderAlert_NEW
            //    {
            //        MdiParent = this,
            //        StartPosition = FormStartPosition.CenterScreen,
            //        WindowState = FormWindowState.Maximized
            //    };
            //    ord.Show();
            //    ordFormOpen = true;
            //    frmLoading.CloseForm();
            //}
            //else
            //{
            //    if (Application.OpenForms.OfType<frmOrderAlert_NEW>().Count() == 1)
            //    {
            //        Application.OpenForms.OfType<frmOrderAlert_NEW>().First().BringToFront();
            //    }
            //}
            if (!ordFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmOrder ord = new frmOrder
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                ord.Show();
                ordFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOrder>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOrder>().First().BringToFront();
                }
            }
        }

        private void nEWToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!NewOrdFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmOrderAlert_NEW ord = new frmOrderAlert_NEW
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                ord.Show();
                NewOrdFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOrderAlert_NEW>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOrderAlert_NEW>().First().BringToFront();
                }
            }

        }

        private void oRDER20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click_2(object sender, EventArgs e)
        {

        }

        private void usernameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userDAL dalUser = new userDAL();
            userBLL uUser = new userBLL();

            DataTable dt = dalUser.userIDSearch(USER_ID);

            if(dt != null && dt.Rows.Count >= 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    if(row[dalUser.UserID].ToString().Equals(USER_ID.ToString()))
                    {
                        uUser.user_id = USER_ID;
                        uUser.user_name = row[dalUser.Username].ToString();
                        uUser.user_password = row[dalUser.Password].ToString();
                        uUser.user_permissions = int.TryParse(row[dalUser.Permission].ToString(), out int permission) ? permission : 0;

                        break;
                    }
                }

                frmUserEdit frm = new frmUserEdit(uUser);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit
            }
           else
            {
                MessageBox.Show("User data not found!");
            }

           
        }

        private void oldVersionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!ordFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmOrder ord = new frmOrder
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                ord.Show();
                ordFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOrder>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOrder>().First().BringToFront();
                }
            }
        }

        private void newVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!NewOrdFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmOrderAlert_NEW ord = new frmOrderAlert_NEW
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                ord.Show();
                NewOrdFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOrderAlert_NEW>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOrderAlert_NEW>().First().BringToFront();
                }
            }
        }

        private void macScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void newVersionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!NEWProductionFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmMachineScheduleVer2 frm = new frmMachineScheduleVer2
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                frm.Show();
                NEWProductionFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmMachineScheduleVer2>().Count() >= 1)
                {
                    Application.OpenForms.OfType<frmMachineScheduleVer2>().First().BringToFront();
                }
            }
        }

        private void oldVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ProductionFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmMachineSchedule frm = new frmMachineSchedule
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                frm.Show();
                ProductionFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmMachineSchedule>().Count() >= 1)
                {
                    Application.OpenForms.OfType<frmMachineSchedule>().First().BringToFront();
                }
            }
        }

        private void oUGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!OUGFormOpen)
            {
                //frmLoading.ShowLoadingScreen();
                frmOUG frm = new frmOUG();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                OUGFormOpen = true;
                //frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOUG>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOUG>().First().BringToFront();
                }
            }
        }

        private void semenyihToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!NewDailyJobSheetFormOpenVer3)
            {
                frmLoading.ShowLoadingScreen();

                frmProductionRecordVer3 frm = new frmProductionRecordVer3();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();

                NewDailyJobSheetFormOpenVer3 = true;

                frmLoading.CloseForm();

            }
            else
            {
                if (Application.OpenForms.OfType<frmProductionRecordVer3>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmProductionRecordVer3>().First().BringToFront();
                }

            }
        }

        static public void OpenOldVersionMachineSchedule()
        {
            if (!ProductionFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmMachineSchedule frm = new frmMachineSchedule
                {
                    MdiParent = Instance,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                frm.Show();
                ProductionFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmMachineSchedule>().Count() >= 1)
                {
                    Application.OpenForms.OfType<frmMachineSchedule>().First().BringToFront();
                }
            }
        }

        static public void OpenNewVersionMachineSchedule()
        {
            if (!NEWProductionFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmMachineScheduleVer2 frm = new frmMachineScheduleVer2
                {
                    MdiParent = Instance,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                frm.Show();
                NEWProductionFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmMachineScheduleVer2>().Count() >= 1)
                {
                    Application.OpenForms.OfType<frmMachineScheduleVer2>().First().BringToFront();
                }
            }
        }

        private void oldVersionToolStripMenuItem_Click_2(object sender, EventArgs e)
        {

            if (!ProductionFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmMachineSchedule frm = new frmMachineSchedule
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                frm.Show();
                ProductionFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmMachineSchedule>().Count() >= 1)
                {
                    Application.OpenForms.OfType<frmMachineSchedule>().First().BringToFront();
                }
            }
        }

        private void newVersionToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            if (!NEWProductionFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmMachineScheduleVer2 frm = new frmMachineScheduleVer2
                {
                    MdiParent = this,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                frm.Show();
                NEWProductionFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmMachineScheduleVer2>().Count() >= 1)
                {
                    Application.OpenForms.OfType<frmMachineScheduleVer2>().First().BringToFront();
                }
            }

           
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!supplierFormOpen)
            {
                frmSupplier item = new frmSupplier();
                item.MdiParent = this;
                item.StartPosition = FormStartPosition.CenterScreen;
                item.WindowState = FormWindowState.Maximized;
                item.Show();
                supplierFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmSupplier>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmSupplier>().First().BringToFront();
                }
            }
        }

        private void semenyihToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

            if (!NewDailyJobSheetFormOpenVer3)
            {
                frmLoading.ShowLoadingScreen();

                frmProductionRecordVer3 frm = new frmProductionRecordVer3();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();

                NewDailyJobSheetFormOpenVer3 = true;

                frmLoading.CloseForm();

            }
            else
            {
                if (Application.OpenForms.OfType<frmProductionRecordVer3>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmProductionRecordVer3>().First().BringToFront();
                }

            }

          
        }

      

        private void MainDashboard_Shown(object sender, EventArgs e)
        {
            if (myconnstrng == text.DB_Semenyih || myconnstrng == text.DB_JunPC)//|| myconnstrng == text.DB_JunPC
            {
               
            }
            else
            {

                //frmMaterialAlertNotice frm2 = new frmMaterialAlertNotice();
                //frm2.StartPosition = FormStartPosition.CenterScreen;
                //frm2.Show();

                //// Check if frm2 exists and is not disposed
                //if (frm2 != null && !frm2.IsDisposed)
                //{
                //    // The form exists and is not disposed, so just show it
                //    frm2.StartPosition = FormStartPosition.CenterScreen;
                //    frm2.Show();
                //}
                //else
                //{
                //    // The form is either null or disposed, so create a new instance and show it
                //    frm2 = new frmMaterialAlertNotice();
                //    frm2.StartPosition = FormStartPosition.CenterScreen;
                //    frm2.Show();
                //}
            }

        }

       
    }
}
