using System.Windows;
using System.Text;
using System.IO;
using System;
using log4net;

namespace CheckConnectionWpf.Methods
{
    public static class WinObjMethods
    {
        public static string GetDBConnectionString(ILog log)
        {
            string conn_string;
            if (CheckConnectionWpf.Properties.Settings.Default.DBConnectionString == "Connections.db")
            {
                StringBuilder sb = new StringBuilder(System.IO.Path.GetTempPath());
                sb.Append(System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().ProcessName));
                conn_string = sb.ToString();

                // Determine whether the directory exists.
                if (!Directory.Exists(conn_string))
                {
                    try
                    {
                        DirectoryInfo di = Directory.CreateDirectory(conn_string);
                    }
                    catch (Exception e)
                    {
                        string CantMakeDir = e.Message + Environment.NewLine + conn_string;
                        log.Info(CantMakeDir);
                        MessageBox.Show(CantMakeDir, "Ошибка",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
                sb.Append("\\");
                sb.Append(CheckConnectionWpf.Properties.Settings.Default.DBConnectionString);

                conn_string = sb.ToString();
            }
            else
            {
                conn_string = CheckConnectionWpf.Properties.Settings.Default.DBConnectionString;
            }
            return conn_string;
        }
 
    }
}
