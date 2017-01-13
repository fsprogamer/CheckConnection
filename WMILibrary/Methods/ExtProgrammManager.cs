using System;
using System.Diagnostics;

using Common;

namespace CheckConnection.Methods
{
    public class ExtProgrammManager : ClassWithLog
    {
        string _arguments = String.Empty;
        string _filename = String.Empty;

        public ExtProgrammManager(string filename, string arguments)
        {
            _arguments = arguments;
            _filename = filename;
        }

        public string Start(string variable)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo(_filename, _arguments + variable)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            log.InfoFormat("{0} {1}", _filename, _arguments + variable);

            using (Process proc = new Process())
            {
                proc.StartInfo = procStartInfo;
                try
                {
                    proc.Start();
                }
                catch(Exception ex) 
                {
                    log.Error("proc.Start", ex);
                }

                return proc.StandardOutput.ReadToEnd();
            }
       }
   }
}
