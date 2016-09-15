using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckConnection.Methods;

namespace CheckConnection
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

            DBInterface db = new DBMethods();
            WMIInterface wmi = new WMIMethods();
            Application.Run(new DisplayConnections(db,wmi));
        }
    }
}
