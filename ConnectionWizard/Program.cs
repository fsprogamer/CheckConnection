using System;
using System.Windows.Forms;

using ConnectionWizard.Methods;
using CheckConnection.Methods;
using PingForm.Methods;

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
            Application.Run(new MainWizard(/*db, wmi, png*/));
        }
    }
}
