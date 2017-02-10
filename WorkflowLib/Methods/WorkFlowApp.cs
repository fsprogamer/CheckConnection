using System;
using System.Collections.Generic;
using System.Activities;
using System.Threading;

using System.Windows.Forms;
using System.IO;

namespace WorkflowLib
{
    public class WorkFlowApp
    {
        public static void Run(string[] log)
        {
            AutoResetEvent syncEvent = new AutoResetEvent(false);
            AutoResetEvent idleEvent = new AutoResetEvent(false);

            var inputs = new Dictionary<string, object>() { { "log", log } };

            WorkflowApplication wfApp =
                new WorkflowApplication(new Flowchart.CheckConnection(), inputs);

            // Configure the persistence store.  
            //wfApp.InstanceStore = store;

            // Add a StringWriter to the extensions. This captures the output  
            // from the WriteLine activities so we can display it in the form.  
            StringWriter sw = new StringWriter();
            wfApp.Extensions.Add(sw);


            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                //string ret = e.Outputs["ret"].ToString();
                //string userinput = e.Outputs["UserAnswer1"].ToString();

                //string mess = "Подключения =" + ret+ ";Выбор пользователя=" + userinput;  
                //MessageBox.Show(mess, "", MessageBoxButtons.OK,
                //                          MessageBoxIcon.Information);

                syncEvent.Set();
            };

            wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
            {
                Console.WriteLine(e.Reason);
                syncEvent.Set();
            };

            wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                Console.WriteLine(e.UnhandledException.ToString());
                return UnhandledExceptionAction.Terminate;
            };

            wfApp.Idle = delegate (WorkflowApplicationIdleEventArgs e)
            {


                var writers = e.GetInstanceExtensions<StringWriter>();
                foreach (var writer in writers)
                {
                    //string str = writer.ToString();                    
                    //System.Array.Resize(ref log, log.Length + 1);
                    //log[log.Length - 1] = writer.ToString(); ;
                }
                idleEvent.Set();
            };

            wfApp.PersistableIdle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                // Send the current WriteLine outputs to the status window.  
                var writers = e.GetInstanceExtensions<StringWriter>();
                foreach (var writer in writers)
                {
                    //UpdateStatus(writer.ToString());
                }
                return PersistableIdleAction.Unload;
            };

            wfApp.Run();

            // Loop until the workflow completes.
            WaitHandle[] handles = new WaitHandle[] { syncEvent, idleEvent };
            while (WaitHandle.WaitAny(handles) != 0)
            {
                // Gather the user input and resume the bookmark.
                bool validEntry = false;
                while (!validEntry)
                {
                    //int Guess;
                    //if (!Int32.TryParse(Console.ReadLine(), out Guess))
                    //{
                    //    Console.WriteLine("Please enter an integer.");
                    //}
                    //else
                    //{
                    //    validEntry = true;
                    //    wfApp.ResumeBookmark("EnterGuess", Guess);
                    //}
                }
            }
        }
    }
}
