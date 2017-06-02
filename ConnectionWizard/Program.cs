using CheckConnection.Methods;
using Common;
using System;
using System.Windows.Forms;

namespace ConnectionWizard
{
    class Program //: NinjectProgram
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
            //Kernel = new StandardKernel(new Bindings());
            IocKernel.Initialize(new Bindings());

            Application.Run(new MainWizard(/*db, wmi, png*/));
        }
    }
}
