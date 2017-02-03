using System;
using System.Windows.Forms;

using ConnectionWizard.Methods;
using CheckConnection.Methods;
using PingLib.Methods;
using Common;
using Ninject;

namespace ConnectionWizard
{
    class Program : NinjectProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ninject Initialization
            Kernel = new StandardKernel(new Bindings());

            Application.Run(new MainWizard(/*db, wmi, png*/));
        }
    }
}
