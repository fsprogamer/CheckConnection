using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace ChCLauncher
{
    class Program
    {
        const string pressEnter = "Для продолжения нажмите Enter.";
        static void Main(string[] args)
        {
            string fileName = @".\CheckConnection.exe";
            
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.
            ProcessStartInfo info = new ProcessStartInfo(fileName);

            CopySqliteDll();

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
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(pressEnter);
                    Console.ReadLine();
                }
            }
            
        }

        private static void CopySqliteDll()
        {
            string FromFile;
            string ToFile = @".\sqlite3.dll";

            if (System.Environment.Is64BitOperatingSystem == true)
                FromFile = @".\x64\sqlite3.dll";
            else
                FromFile = @".\x86\sqlite3.dll";

            try
            {
                if (File.Exists(ToFile))
                    File.Delete(ToFile);
                File.Copy(FromFile, ToFile);
                Console.WriteLine($"sqlite3.dll replace {FromFile} to {ToFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(pressEnter);
                Console.ReadLine();
            }
        }
    }
}
