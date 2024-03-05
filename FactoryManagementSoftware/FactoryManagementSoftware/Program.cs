using FactoryManagementSoftware.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryManagementSoftware
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new frmSBBBodyCalculation());
            //Application.Run(new frmMaterialAlertNotice());

            //Application.Run(new frmDOManagement());

            //Application.Run(new frmDOEditing());

            //Application.Run(new frmStockCountManagement());

            //Application.Run(new frmPlanningVer2dot1("R 120 141 375 96-OLD",1));

            //Application.Run(new frmReadSemenyihData());

            Application.Run(new MainDashboard(1));

            //Application.Run(new frmLogIn());

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
