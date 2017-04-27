using System;
using System.Reflection;
using System.Windows.Forms;
using CheckConnection.Methods;
using CheckConnection;
using Common;
using Ninject;

namespace CheckConnection
{
    /*static */class Program: NinjectProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"CheckConnection.exe.log4net"));//Если файл с настройками в папке с exe файлом        

            // Ninject Initialization
            Kernel = new StandardKernel(new Bindings());

            var modeForm = new ModeForm();
            Application.Run(modeForm);
            //var DisplayConn = new DisplayConnections(/*wmi*/);
            //DisplayConn.StartPosition = FormStartPosition.WindowsDefaultLocation;            
            //Application.Run(DisplayConn);            
        }
    }
}
