using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

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
        static public bool OEMItemFormOpen = false;
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
        static public int USER_ACCESS_LEVEL = 0;

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

            USER_ACCESS_LEVEL = userPermission;

            string userName = dalUser.getUsername(USER_ID);

            usernameToolStripMenuItem.Text = userName;

            pOToolStripMenuItem.Visible = false;

            if (userPermission >= ACTION_LVL_FIVE)
            {
                sBBToolStripMenuItem.Visible = true;
                OEMItemToolStripMenuItem.Visible = true;
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
                OEMItemToolStripMenuItem.Visible = false;
            }

            if(USER_ID == 24)
            {
                OEMItemToolStripMenuItem.Visible = true;

            }

            myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;


            if (myconnstrng == text.DB_Semenyih && userPermission < ACTION_LVL_FIVE)//|| myconnstrng == text.DB_JunPC
            {
                //Semenyih
                //pMMAToolStripMenuItem.Visible = false;
                //forecastToolStripMenuItem.Visible = false;
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

            //if (myconnstrng == text.DB_Semenyih)//|| myconnstrng == text.DB_JunPC
            //{
            //    //Semenyih
            //    if (!MaterialUsedReportFormOpen)
            //    {
            //        frmMaterialUsedReport frm = new frmMaterialUsedReport();
            //        frm.MdiParent = this;
            //        frm.StartPosition = FormStartPosition.CenterScreen;
            //        frm.WindowState = FormWindowState.Maximized;
            //        frm.Show();
            //        MaterialUsedReportFormOpen = true;
            //    }
            //    else
            //    {
            //        if (Application.OpenForms.OfType<frmMaterialUsedReport>().Count() == 1)
            //        {
            //            Application.OpenForms.OfType<frmMaterialUsedReport>().First().BringToFront();
            //        }
            //    }
            //}
            //else
            //{
            //    //OUG
            //    if (!MaterialUsedReportFormOpen)
            //    {
            //        frmMaterialUsedReport_NEW frm = new frmMaterialUsedReport_NEW();
            //        frm.MdiParent = this;
            //        frm.StartPosition = FormStartPosition.CenterScreen;
            //        frm.WindowState = FormWindowState.Maximized;
            //        frm.Show();
            //        MaterialUsedReportFormOpen = true;
            //    }
            //    else
            //    {
            //        if (Application.OpenForms.OfType<frmMaterialUsedReport_NEW>().Count() == 1)
            //        {
            //            Application.OpenForms.OfType<frmMaterialUsedReport_NEW>().First().BringToFront();
            //        }
            //    }

            //}

           
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
            #region data preload (cancelled)
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
            #endregion

            #region Page preload

            //if (!SBBFormOpen)
            //{
            //    frmLoading.ShowLoadingScreen();
            //    frmSBBVer2 frm = new frmSBBVer2();
            //    frm.MdiParent = this;
            //    frm.StartPosition = FormStartPosition.CenterScreen;
            //    frm.WindowState = FormWindowState.Maximized;
            //    frm.Show();
            //    SBBFormOpen = true;
            //    frmLoading.CloseForm();
            //}
            //else
            //{
            //    if (Application.OpenForms.OfType<frmSBBVer2>().Count() == 1)
            //    {
            //        Application.OpenForms.OfType<frmSBBVer2>().First().BringToFront();
            //    }
            //}

            if (MainDashboard.USER_ID == 10) //|| MainDashboard.USER_ID == 1
            {
                frmOrderRequestNotice frm3 = new frmOrderRequestNotice(this);
                //frm2.StartPosition = FormStartPosition.CenterScreen;
                //frm2.Show();

                // Check if frm2 exists and is not disposed
                if (frm3 != null && !frm3.IsDisposed)
                {
                    // The form exists and is not disposed, so just show it
                    frm3.StartPosition = FormStartPosition.CenterScreen;
                    frm3.ShowDialog();
                }
            }

            int userID = MainDashboard.USER_ID;
            if (userID == 10 || userID == 21 || userID == 23) //|| MainDashboard.USER_ID == 1
            {
                frmMaterialAlertNotice frm2 = new frmMaterialAlertNotice();
                //frm2.StartPosition = FormStartPosition.CenterScreen;
                //frm2.Show();

                // Check if frm2 exists and is not disposed
                if (frm2 != null && !frm2.IsDisposed)
                {
                    // The form exists and is not disposed, so just show it
                    frm2.StartPosition = FormStartPosition.CenterScreen;
                    frm2.ShowDialog();
                }
            }

            if (MainDashboard.USER_ID == 1)
            {
                //// Pop up message to ask user if they want to start sync
                //DialogResult result = MessageBox.Show("Do you want to start sync SMY PMMA item stock (part, child part, material)?", "Sync Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //if (result == DialogResult.Yes)
                //{
                //    // Start adding data to DataTable
                //    DataTable dt_SMYPMMAStock = syncSMYPMMAStock();

                //    // Start sync function (content left blank)
                //    startSyncStockFunction(dt_SMYPMMAStock);
                //}


                //result = MessageBox.Show("Do you want to start sync SMY PMMA Nov Delivery Records?", "Sync Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //if (result == DialogResult.Yes)
                //{
                //    DataTable DT_SMY_TO_PMMA = CreateTransferHistoryDataTable();
                //    startSyncPMMANovDeliveryFunction(DT_SMY_TO_PMMA);
                //}
            }

            if (!frmOrderRequestNotice.PROCCED_TO_ORDER_PAGE)
            {
                ////OUG
                //if (!inOutFormOpen)
                //{
                //    frmLoading.ShowLoadingScreen();
                //    frmInOutVer2 frm = new frmInOutVer2();
                //    frm.MdiParent = this;
                //    frm.StartPosition = FormStartPosition.CenterScreen;
                //    frm.WindowState = FormWindowState.Maximized;
                //    frm.Show();
                //    inOutFormOpen = true;
                //    frmLoading.CloseForm();
                //}
                //else
                //{
                //    if (Application.OpenForms.OfType<frmInOutVer2>().Count() == 1)
                //    {
                //        Application.OpenForms.OfType<frmInOutVer2>().First().BringToFront();
                //    }
                //}
            }


            #endregion


        }


        private DataTable syncSMYPMMAStock()
        {
            DataTable dt_SMYPMMAStock = new DataTable();
            dt_SMYPMMAStock.Columns.Add("stock_item_code", typeof(string));
            dt_SMYPMMAStock.Columns.Add("stock_qty", typeof(decimal));

            // Define the data as a list of tuples
            List<Tuple<string, decimal>> stockData = new List<Tuple<string, decimal>>()
            {
            new Tuple<string, decimal>("ABS PA765(FR)", 0m),
        new Tuple<string, decimal>("C33LDD100", 1120m),
        new Tuple<string, decimal>("C43NC7000", 16024m),
        new Tuple<string, decimal>("P41", 0m),
        new Tuple<string, decimal>("PELDPEB1622", 19800m),
        new Tuple<string, decimal>("PLASTIC CAP 51MM", 20700m),
        new Tuple<string, decimal>("PVC 40P", 0m),
        new Tuple<string, decimal>("A38K1500", 1439m),
        new Tuple<string, decimal>("A0GK1500", 11098m),
        new Tuple<string, decimal>("A0EZ15100", 0m),
        new Tuple<string, decimal>("C92HZF100", 9579m),
        new Tuple<string, decimal>("A53K15K0", 4435m),
        new Tuple<string, decimal>("A95K15K0-B", 3534m),
        new Tuple<string, decimal>("C88RYX080", 2884m),
        new Tuple<string, decimal>("A25Q150V0", 5977m),
        new Tuple<string, decimal>("C54PTB000", 5277m),
        new Tuple<string, decimal>("INS FOAM C SET SMALL", 1483m),
        new Tuple<string, decimal>("INS FOAM C SET BIG", 1640m),
        new Tuple<string, decimal>("A0DK1X000", 4567m),
        new Tuple<string, decimal>("A38K1900", 2980m),
        new Tuple<string, decimal>("A77K15000", 6840m),
        new Tuple<string, decimal>("C30PTB000", 106m),
        new Tuple<string, decimal>("A11M320W0", 13126m),
        new Tuple<string, decimal>("12BFJ", 2921m),
        new Tuple<string, decimal>("C19PTB000", 2754m),
        new Tuple<string, decimal>("C84KXQ000", 550m),
        new Tuple<string, decimal>("PP AZ 564", 1850m),
        new Tuple<string, decimal>("PLASTIC CAP 55MM", 22663m),
        new Tuple<string, decimal>("A76K15000", 6957m),
        new Tuple<string, decimal>("C36P113000", 5700m),
        new Tuple<string, decimal>("C93N6K000", 1827m),
        new Tuple<string, decimal>("ABS 700 314", 1800m),
        new Tuple<string, decimal>("A0BK150G0", 1260m),
        new Tuple<string, decimal>("ABS 450Y MH1", 2525m),
        new Tuple<string, decimal>("ACETAL POM M90-44", 2850m),
        new Tuple<string, decimal>("6014143641", 1144m),
        new Tuple<string, decimal>("6014143644", 1143m),
        new Tuple<string, decimal>("NESTING 442 X 45", 828m),
        new Tuple<string, decimal>("A21Q1X0V0", 2812m),
        new Tuple<string, decimal>("A0BK150R0", 67m),
        new Tuple<string, decimal>("A84K15000-B", 0m),
        new Tuple<string, decimal>("LYR 580 X 290", 414m),
        new Tuple<string, decimal>("LYR 510 X 340", 406m),
        new Tuple<string, decimal>("A41K150K0", 117m),
        new Tuple<string, decimal>("PP TITAN SM 240", 870m),
        new Tuple<string, decimal>("6024153153", 782m),
        new Tuple<string, decimal>("C19PTB/C54PTB", 0m),
        new Tuple<string, decimal>("HIPS HT50(NATURAL)", 3500m),
        new Tuple<string, decimal>("NESTING 492 X 45", 192m),
        new Tuple<string, decimal>("A0BK150V0", 43m),
        new Tuple<string, decimal>("CTR 002", 292m),
        new Tuple<string, decimal>("LYR 360 X 310", 282m),
        new Tuple<string, decimal>("ABS 700 314 B1", 275m),
        new Tuple<string, decimal>("CREASING PAD 400 X 305", 95m),
        new Tuple<string, decimal>("NESTING 405 X 218 X 601 X 218", 52m),
        new Tuple<string, decimal>("CREASING PAD 570 X 190", 161m),
        new Tuple<string, decimal>("CTN 365 X 315 X 230MM", 212m),
        new Tuple<string, decimal>("CTN 585 X 305 X 381", 55m),
        new Tuple<string, decimal>("PP P740J NAT", 306m),
        new Tuple<string, decimal>("LDPE C150Y", 150m),
        new Tuple<string, decimal>("NESTING 496 X 170 X 570 X 170", 81m),
        new Tuple<string, decimal>("LYR 705 X 389", 15m),
        new Tuple<string, decimal>("LYR PAD 560 X 500", 76m),
        new Tuple<string, decimal>("LYR PAD 590 X 370", 53m),
        new Tuple<string, decimal>("LYR 492 X 442", 52m),
        new Tuple<string, decimal>("PP 90791 MB30", 79.8m),
        new Tuple<string, decimal>("C84KXQ100", 0m),
        new Tuple<string, decimal>("CTR 001", 79m),
        new Tuple<string, decimal>("CTN 608 X 413 X 455", 34m),
        new Tuple<string, decimal>("CTN 613 X 312 X 265", 243m),
        new Tuple<string, decimal>("AS PN 127", 50m),
        new Tuple<string, decimal>("CTR 003", 1127m),
        new Tuple<string, decimal>("CTN 590 X 515 X 360", 16m),
        new Tuple<string, decimal>("MWS 00952-7%", 42.64m),
        new Tuple<string, decimal>("CTN 446 X 210 X 496", 40m),
        new Tuple<string, decimal>("ABS 105872-11 MB50", 56.3m),
        new Tuple<string, decimal>("6024135963", 6m),
        new Tuple<string, decimal>("CTR 004", 184m),
        new Tuple<string, decimal>("ABS 60785 MB50", 33.6m),
        new Tuple<string, decimal>("MWS 00139-4%", 34.7m),
        new Tuple<string, decimal>("A0LK150V0", 25m),
        new Tuple<string, decimal>("F 30940M-1%", 34.1m),
        new Tuple<string, decimal>("PB 0.04 X 11 X 16", 25m),
        new Tuple<string, decimal>("A 887549-3%", 22.8m),
        new Tuple<string, decimal>("A 887546-3%", 83m),
        new Tuple<string, decimal>("A 887545-3%", 70.3m),
        new Tuple<string, decimal>("A 887544-3%", 20m),
        new Tuple<string, decimal>("CTN 711 X 394 X 394", 15m),
        new Tuple<string, decimal>("ABS 90710 MB50", 14.2m),
        new Tuple<string, decimal>("A72K15000", 0m),
        new Tuple<string, decimal>("A72K15000", 10m),
        new Tuple<string, decimal>("ABS 9801-5%", 13m),
        new Tuple<string, decimal>("ABS 30841 MB50", 11.1m),
        new Tuple<string, decimal>("ABS 71180 MB50", 14.3m),
        new Tuple<string, decimal>("PP 55733121585-1", 6.3m),
        new Tuple<string, decimal>("PB 0.06 X 14 X 29", 6m),
        new Tuple<string, decimal>("PB 3 X 6", 25m),
        new Tuple<string, decimal>("A 887082-3%", 3.8m),
        new Tuple<string, decimal>("A0LK180G0", 1740m),
        new Tuple<string, decimal>("A0LK160R0", 464m),
        new Tuple<string, decimal>("CTN 590 X 310 X 388 MM", 1m),
        new Tuple<string, decimal>("E28H14", 1m),
        new Tuple<string, decimal>("P13", 1m),
        new Tuple<string, decimal>("P17", 1m),
        new Tuple<string, decimal>("P18", 1m),
        new Tuple<string, decimal>("P20", 1m),
        new Tuple<string, decimal>("P36", 1m),
        new Tuple<string, decimal>("P73", 1m),
        new Tuple<string, decimal>("P74", 1m),
        new Tuple<string, decimal>("P76", 0m),
        new Tuple<string, decimal>("PVC 91449 PT08-1.6%", 1m),
        new Tuple<string, decimal>("PE 74308 PT05", 0.9m),
        new Tuple<string, decimal>("A0BK150A0", 0m),
        new Tuple<string, decimal>("A0LK190A0", 1178m),
        new Tuple<string, decimal>("A21H32000", 1672m),
        new Tuple<string, decimal>("A42K1500", 2720m),
        new Tuple<string, decimal>("A84K15100-B", 2025m),
        new Tuple<string, decimal>("A94M320W0", 0m),
        new Tuple<string, decimal>("A96K1X0K0", 434m),
        new Tuple<string, decimal>("A96K1X0T0", 2506m),
        new Tuple<string, decimal>("ABS 90642 CP", 0m),
        new Tuple<string, decimal>("ABS 920 555", 25m),
        new Tuple<string, decimal>("C24POR100", 0m),
        new Tuple<string, decimal>("C24POR190", 0m),
        new Tuple<string, decimal>("C33L7K000", 2006m),
        new Tuple<string, decimal>("C84KXQ100 W/FOAM", 214m),
        new Tuple<string, decimal>("CTN 520 X 350 X 266", 0m),
        new Tuple<string, decimal>("HEX NUT M6 X 1.00", 5825.98m),
        new Tuple<string, decimal>("HIPS HT57(NATURAL)", 0m),
        new Tuple<string, decimal>("LDPE 260GG", 0m),
        new Tuple<string, decimal>("LDPE 260GG/F410 1/C150Y", 0m),
        new Tuple<string, decimal>("LYR 575 X 300 MM", 0m),
        new Tuple<string, decimal>("PB 14 X 24 X 0.05", 0m),
        new Tuple<string, decimal>("PLASTIC CORE-48MM", 0m),
        new Tuple<string, decimal>("PLASTIC CORE-50MM", 14560m),
        new Tuple<string, decimal>("PLASTIC CORE-66MM", 7630m),
        new Tuple<string, decimal>("PLASTIC CORE-72MM", 0m),
        new Tuple<string, decimal>("PP 6331/600G/G112", 8000m),
        new Tuple<string, decimal>("PP 6331/600G/G112", 0m),
        new Tuple<string, decimal>("PP EPC40R", 18600m)
    };

            // Loop over the data and add rows to the DataTable
            foreach (var item in stockData)
            {
                addRowToTable(dt_SMYPMMAStock, item.Item1, item.Item2);
            }

            return dt_SMYPMMAStock;
        }

        // Helper function to add a row to the DataTable
        private void addRowToTable(DataTable dt, string stock_item_code, decimal stock_qty)
        {
            DataRow dr = dt.NewRow();
            dr["stock_item_code"] = stock_item_code;
            dr["stock_qty"] = stock_qty;
            dt.Rows.Add(dr);
        }

        private DataTable CreateTransferHistoryDataTable()
        {
            DataTable dt_SMY_To_PMMA = new DataTable();

            // Add columns
            dt_SMY_To_PMMA.Columns.Add("trf_hist_item_code", typeof(string));
            dt_SMY_To_PMMA.Columns.Add("trf_hist_from", typeof(string));
            dt_SMY_To_PMMA.Columns.Add("trf_hist_to", typeof(string));
            dt_SMY_To_PMMA.Columns.Add("trf_hist_qty", typeof(int));
            dt_SMY_To_PMMA.Columns.Add("trf_hist_trf_date", typeof(DateTime));

            // Add rows
            dt_SMY_To_PMMA.Rows.Add("V99CAR0K0", "Semenyih", "PMMA", 40, DateTime.Parse("26/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99CAR0R0", "Semenyih", "PMMA", 40, DateTime.Parse("26/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK150V0", "Semenyih", "PMMA", 491, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK150V0", "Semenyih", "PMMA", 250, DateTime.Parse("25/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK160R0", "Semenyih", "PMMA", 575, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK160R0", "Semenyih", "PMMA", 100, DateTime.Parse("22/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK180G0", "Semenyih", "PMMA", 850, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK180G0", "Semenyih", "PMMA", 190, DateTime.Parse("5/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK180G0", "Semenyih", "PMMA", 490, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK180G0", "Semenyih", "PMMA", 850, DateTime.Parse("14/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK180G0", "Semenyih", "PMMA", 526, DateTime.Parse("7/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK180G0", "Semenyih", "PMMA", 1001, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK180G0", "Semenyih", "PMMA", 452, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK180G0", "Semenyih", "PMMA", 350, DateTime.Parse("22/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK190A0", "Semenyih", "PMMA", 720, DateTime.Parse("5/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK190A0", "Semenyih", "PMMA", 885, DateTime.Parse("12/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK190A0", "Semenyih", "PMMA", 935, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK190A0", "Semenyih", "PMMA", 400, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK190A0", "Semenyih", "PMMA", 850, DateTime.Parse("11/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK190A0", "Semenyih", "PMMA", 276, DateTime.Parse("7/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A0LK190A0", "Semenyih", "PMMA", 50, DateTime.Parse("22/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A21Q1X0V0", "Semenyih", "PMMA", 101, DateTime.Parse("5/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A21Q1X0V0", "Semenyih", "PMMA", 100, DateTime.Parse("26/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A25Q150V0", "Semenyih", "PMMA", 490, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A25Q150V0", "Semenyih", "PMMA", 1340, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A25Q150V0", "Semenyih", "PMMA", 1518, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A25Q150V0", "Semenyih", "PMMA", 400, DateTime.Parse("22/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15000-B", "Semenyih", "PMMA", 256, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 850, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 910, DateTime.Parse("5/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 935, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 885, DateTime.Parse("12/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 890, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 850, DateTime.Parse("14/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 931, DateTime.Parse("11/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 802, DateTime.Parse("7/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 664, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 626, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 892, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 750, DateTime.Parse("22/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A84K15100-B", "Semenyih", "PMMA", 256, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A94M320W0", "Semenyih", "PMMA", 1020, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A94M320W0", "Semenyih", "PMMA", 1380, DateTime.Parse("26/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A96K1X0K0", "Semenyih", "PMMA", 200, DateTime.Parse("4/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A96K1X0T0", "Semenyih", "PMMA", 868, DateTime.Parse("4/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A96K1X0T0", "Semenyih", "PMMA", 378, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A96K1X0T0", "Semenyih", "PMMA", 1058, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("A96K1X0T0", "Semenyih", "PMMA", 1296, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("C93N6K000", "Semenyih", "PMMA", 2000, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V02K81000", "Semenyih", "PMMA", 3000, DateTime.Parse("11/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V02K81000", "Semenyih", "PMMA", 3000, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V02K81000", "Semenyih", "PMMA", 3000, DateTime.Parse("5/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V02K81000", "Semenyih", "PMMA", 3000, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPBW100", "Semenyih", "PMMA", 215, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPBW100", "Semenyih", "PMMA", 815, DateTime.Parse("19/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPBW100", "Semenyih", "PMMA", 430, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPBW100", "Semenyih", "PMMA", 266, DateTime.Parse("21/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPBW100", "Semenyih", "PMMA", 334, DateTime.Parse("25/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPCH200", "Semenyih", "PMMA", 375, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPM4100", "Semenyih", "PMMA", 300, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPM4100", "Semenyih", "PMMA", 190, DateTime.Parse("5/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPM4100", "Semenyih", "PMMA", 490, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPM4100", "Semenyih", "PMMA", 850, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPM4100", "Semenyih", "PMMA", 626, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPM4100", "Semenyih", "PMMA", 892, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPM4100", "Semenyih", "PMMA", 100, DateTime.Parse("22/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0KPM4100", "Semenyih", "PMMA", 300, DateTime.Parse("25/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0LKBW200", "Semenyih", "PMMA", 365, DateTime.Parse("19/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0LKBW200", "Semenyih", "PMMA", 330, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0LKBW200", "Semenyih", "PMMA", 600, DateTime.Parse("21/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0LKBZ100", "Semenyih", "PMMA", 215, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0LKBZ100", "Semenyih", "PMMA", 450, DateTime.Parse("19/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0LKBZ100", "Semenyih", "PMMA", 100, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0LKCH000", "Semenyih", "PMMA", 375, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V0LKNN000", "Semenyih", "PMMA", 881, DateTime.Parse("8/11/2024"));

            // Continuing with remaining rows
            dt_SMY_To_PMMA.Rows.Add("V0LKNP000", "Semenyih", "PMMA", 485, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V10RFP000_1", "Semenyih", "PMMA", 9000, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V10RFP000_1", "Semenyih", "PMMA", 4500, DateTime.Parse("26/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V16ATN000", "Semenyih", "PMMA", 500, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V16ATN000-R", "Semenyih", "PMMA", 2200, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V17ATN000-R", "Semenyih", "PMMA", 2200, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V27C61000", "Semenyih", "PMMA", 1000, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V29FAQ200", "Semenyih", "PMMA", 400, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V29FAR200", "Semenyih", "PMMA", 1000, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V48KBW000", "Semenyih", "PMMA", 2400, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V48KBW000", "Semenyih", "PMMA", 2700, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V50BTN0K0", "Semenyih", "PMMA", 1500, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V50BTN0K0-R", "Semenyih", "PMMA", 1200, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V66KAQ100", "Semenyih", "PMMA", 200, DateTime.Parse("7/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V66KAQ100", "Semenyih", "PMMA", 1132, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V66KAQ100", "Semenyih", "PMMA", 485, DateTime.Parse("21/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V66KDN100", "Semenyih", "PMMA", 1080, DateTime.Parse("12/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V66KDN100", "Semenyih", "PMMA", 1050, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V66KDN100", "Semenyih", "PMMA", 1000, DateTime.Parse("14/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V66KDN100", "Semenyih", "PMMA", 965, DateTime.Parse("11/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V66KDN100", "Semenyih", "PMMA", 1162, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V73KBW100", "Semenyih", "PMMA", 215, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V73KBW100", "Semenyih", "PMMA", 815, DateTime.Parse("19/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V73KBW100", "Semenyih", "PMMA", 805, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V73KBW100", "Semenyih", "PMMA", 600, DateTime.Parse("21/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V76P9L1K0", "Semenyih", "PMMA", 2200, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V76P9L1K0", "Semenyih", "PMMA", 3200, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V76P9L1K0", "Semenyih", "PMMA", 1400, DateTime.Parse("19/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V78JQK100-M", "Semenyih", "PMMA", 100, DateTime.Parse("6/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V90TCH000", "Semenyih", "PMMA", 100, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V92KBW000", "Semenyih", "PMMA", 240, DateTime.Parse("19/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V92KBW000", "Semenyih", "PMMA", 480, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V92KBW000", "Semenyih", "PMMA", 225, DateTime.Parse("21/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V96LAR000", "Semenyih", "PMMA", 120, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V96LBW000", "Semenyih", "PMMA", 1540, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V96LBW000", "Semenyih", "PMMA", 770, DateTime.Parse("18/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V96LDN000", "Semenyih", "PMMA", 900, DateTime.Parse("12/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V96LDN000", "Semenyih", "PMMA", 1350, DateTime.Parse("13/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V96LDN000", "Semenyih", "PMMA", 900, DateTime.Parse("14/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V96LDN000", "Semenyih", "PMMA", 900, DateTime.Parse("11/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V96LDN000", "Semenyih", "PMMA", 557, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99CAR0K0", "Semenyih", "PMMA", 1440, DateTime.Parse("19/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99CAR0K0", "Semenyih", "PMMA", 1800, DateTime.Parse("15/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99CAR0K0", "Semenyih", "PMMA", 450, DateTime.Parse("26/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99CBW000", "Semenyih", "PMMA", 1080, DateTime.Parse("14/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99CDP000", "Semenyih", "PMMA", 810, DateTime.Parse("8/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99CJP0M0", "Semenyih", "PMMA", 1080, DateTime.Parse("1/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99PC0000", "Semenyih", "PMMA", 275, DateTime.Parse("20/11/2024"));
            dt_SMY_To_PMMA.Rows.Add("V99PCH100", "Semenyih", "PMMA", 100, DateTime.Parse("20/11/2024"));

            return dt_SMY_To_PMMA;


        }

            
        private void startSyncStockFunction(DataTable dt_SMYPMMAStock)
        {
            itemDAL dalItem = new itemDAL();
            int failedUpdates = 0;
            foreach (DataRow row in dt_SMYPMMAStock.Rows)
            {
                string itemCode = row["stock_item_code"].ToString();//error

                decimal newStock = decimal.TryParse(row["stock_qty"].ToString(), out var stock) ? stock : 0;

                bool success = dalItem.directUpdateFacStock(itemCode, "10", newStock);

                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show($"Failed to add new {itemCode} {newStock} record");
                    failedUpdates++;
                }

            }

            // Notify the user that the sync process is done
            if (failedUpdates == 0)
            {
                MessageBox.Show("Sync process completed successfully!", "Sync Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Sync process completed with {failedUpdates} failed updates.", "Sync Complete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void startSyncPMMANovDeliveryFunction(DataTable dt_SmytoPMMA)
        {
            trfHistDAL dalTrf = new trfHistDAL();
            trfHistBLL utrfHist = new trfHistBLL();
            trfHistDAL daltrfHist = new trfHistDAL();
            DataTable dt_OUG_Nov_Record = dalTrf.SelectByMonthYear(11, 2024);
            DateTime now = DateTime.Now;
            // Use the Select method to filter rows
            string filterExpression = "trf_hist_from = 'Semenyih' AND trf_hist_to = 'PMMA' AND trf_result = 'Passed'";
            DataRow[] filteredRows = dt_OUG_Nov_Record.Select(filterExpression);

            // Create a new DataTable to hold the filtered data
            DataTable dt_Filtered = dt_OUG_Nov_Record.Clone(); // Clone the structure

            // Import the filtered rows into the new DataTable
            foreach (DataRow row in filteredRows)
            {
                dt_Filtered.ImportRow(row);
            }

            foreach(DataRow row in dt_SmytoPMMA.Rows)
            {
                string itemCode = row[dalTrf.TrfItemCode].ToString();
                string qty = row[dalTrf.TrfQty].ToString();
                string trfDate = row[dalTrf.TrfDate].ToString();

                bool ougsametrffound = false;

                foreach(DataRow rowOUG in dt_Filtered.Rows)
                {
                    string itemCodeOUG = rowOUG[dalTrf.TrfItemCode].ToString();
                    string qtyOUG = rowOUG[dalTrf.TrfQty].ToString();
                    string trfDateOUG = rowOUG[dalTrf.TrfDate].ToString();

                    if(itemCode == itemCodeOUG && qty == qtyOUG && trfDate == trfDateOUG)
                    {
                        ougsametrffound = true;
                        break;
                    }
                }

                if(!ougsametrffound)
                {
                    //insert trf data
                    string locationFrom = "Semenyih";
                    string locationTo = "PMMA";

                    utrfHist.trf_table_key = "";
                    utrfHist.trf_hist_item_code = itemCode;
                    utrfHist.trf_hist_from = locationFrom;
                    utrfHist.trf_hist_to = locationTo;
                    utrfHist.trf_hist_qty = float.TryParse(qty, out var i)? i : 0;
                    utrfHist.trf_hist_unit = text.Unit_Piece;

                    utrfHist.trf_hist_trf_date = Convert.ToDateTime(trfDate);
                    utrfHist.trf_hist_note = "[" + dalUser.getUsername(MainDashboard.USER_ID) + "] " + "SYSTEM AUTO SYNC";
                    utrfHist.trf_hist_added_date = now;
                    utrfHist.trf_hist_added_by = MainDashboard.USER_ID;
                    utrfHist.trf_result = "Passed";
                    utrfHist.trf_hist_from_order = 0;

                    //Inserting Data into Database
                    int trfID = daltrfHist.InsertAndGetPrimaryKey(utrfHist);

                    if (trfID == -1)
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to add new transfer record");
                        tool.historyRecord(text.System, "Failed to add new transfer record (InOutEdit)", utrfHist.trf_hist_added_date, MainDashboard.USER_ID);
                    }
                    else
                    {
                        //tool.historyRecord(text.Transfer, text.getTransferDetailString(daltrfHist.getIndexNo(utrfHist), utrfHist.trf_hist_qty, utrfHist.trf_hist_unit, utrfHist.trf_hist_item_code, locationFrom, locationTo), utrfHist.trf_hist_added_date, MainDashboard.USER_ID);
                        tool.historyRecord(text.Transfer, text.getTransferDetailString(trfID, utrfHist.trf_hist_qty, utrfHist.trf_hist_unit, utrfHist.trf_hist_item_code, locationFrom, locationTo), utrfHist.trf_hist_added_date, MainDashboard.USER_ID);
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

                frmInOutReport_ver2 frm = new frmInOutReport_ver2();
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
                if (Application.OpenForms.OfType<frmInOutReport_ver2>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmInOutReport_ver2>().First().BringToFront();
                }
            }
        }

        private void pMMAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PMMAFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmZeroCost frm = new frmZeroCost
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
                if (Application.OpenForms.OfType<frmZeroCost>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmZeroCost>().First().BringToFront();
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
                        //MdiParent = this,
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
                        //MdiParent = this,
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
                frmSBBVer2 frm = new frmSBBVer2();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                SBBFormOpen = true;
                //frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmSBBVer2>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmSBBVer2>().First().BringToFront();
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

        private void OEMItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!OEMItemFormOpen)
            {
                frmOEMStockCount frm = new frmOEMStockCount();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                OEMItemFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmOEMStockCount>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOEMStockCount>().First().BringToFront();
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

                frmMaterialAlertNotice frm2 = new frmMaterialAlertNotice();
                frm2.StartPosition = FormStartPosition.CenterScreen;
                frm2.Show();

                // Check if frm2 exists and is not disposed
                if (frm2 != null && !frm2.IsDisposed)
                {
                    // The form exists and is not disposed, so just show it
                    frm2.StartPosition = FormStartPosition.CenterScreen;
                    frm2.Show();
                }
                else
                {
                    // The form is either null or disposed, so create a new instance and show it
                    frm2 = new frmMaterialAlertNotice();
                    frm2.StartPosition = FormStartPosition.CenterScreen;
                    frm2.Show();
                }
            }

        }

       
    }
}
