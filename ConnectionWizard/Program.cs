using System;
using System.Windows.Forms;

using ConnectionWizard.Methods;


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

            DBInterface db = new ConnectionWizard.Methods.DBMethods();
            Application.Run(new MainWizard(db));
        }
    }
}
