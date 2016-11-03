using System;

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
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"CheckConnection.exe.log4net"));//Если файл с настройками в папке с exe файлом        
            
            WMIInterface wmi = new WMIManager();
            Application.Run(new DisplayConnections(wmi));            
        }
    }
}
