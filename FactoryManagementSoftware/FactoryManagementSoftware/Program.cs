using FactoryManagementSoftware.UI;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new frmItemReport());

            //Application.Run(new frmMachineSchedule());

            Application.Run(new MainDashboard(1));

            //Application.Run(new frmLogIn());

            //Application.Run(new frmForecastReport_NEW());


            //Application.Run(new frmAddItem());
            //Application.Run(new frmNewInOut());
        }
    }
}
