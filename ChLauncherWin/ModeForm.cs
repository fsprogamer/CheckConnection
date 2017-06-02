using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System;
using System.Windows.Forms;
using log4net;

namespace ChLauncherWin
{
    public partial class ModeForm: Form
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ModeForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CopySqliteDll();
        }

        private void Start(uint mode)
        { 
            string fileName = @".\CheckConnection.exe";
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.
            ProcessStartInfo info = new ProcessStartInfo(fileName, mode.ToString());      

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
                        log.Error("Start (win32ex)", win32ex);
                        info = new ProcessStartInfo(fileName);
                        info.UseShellExecute = true;
                        info.Verb = "";
                        Process.Start(info);
                        log.Info("After process start.");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Start process", ex);
                }
            }
            
        }

        private void CopySqliteDll()
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
                log.Info($"sqlite3.dll replace {FromFile} to {ToFile}");
            }
            catch (Exception ex)
            {
                log.Error("Copy sqlite3.dll", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start((radioButtonDiagnose.Checked==true)?(uint)0: 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
