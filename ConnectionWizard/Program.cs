using System;
using System.Windows.Forms;

using ConnectionWizard.Methods;
using CheckConnection.Methods;

namespace ConnectionWizard
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

            ConnectionWizard.Methods.DBInterface db = new ConnectionWizard.Methods.DBMethods();
            WMIInterface wmi = new WMIMethods();
            Application.Run(new MainWizard(db,wmi));
        }
    }
}
