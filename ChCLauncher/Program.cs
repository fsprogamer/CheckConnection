using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ChCLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"..\..\..\CheckConnection\bin\Debug\CheckConnection.exe";
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.
            ProcessStartInfo info;
            info = new ProcessStartInfo(fileName);

            info.UseShellExecute = true;
            info.Verb = "runas";
            try
            {
                Process.Start(info);
            }
            catch (Win32Exception win32ex)
            {
                try
                {
                    if (win32ex.NativeErrorCode == ERROR_CANCELLED)
                    {
                        Console.WriteLine("Exception,{0}", win32ex.NativeErrorCode.ToString());
                        info = new ProcessStartInfo(fileName);
                        info.UseShellExecute = true;
                        info.Verb = "";
                        Process.Start(info);
                        Console.WriteLine("After process start.");
                    }
                }
                catch (Exception ex)
                {

                }
            }
            
        }
    }
}
