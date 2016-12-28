using System;
using System.Collections.Generic;
using System.Activities;
using System.Threading;

using System.Windows.Forms;

namespace WorkflowLib
{
    public class WorkFlowApp
    {
        public static void Run(string adaptername)
        {

            AutoResetEvent syncEvent = new AutoResetEvent(false);
            AutoResetEvent idleEvent = new AutoResetEvent(false);

            var inputs = new Dictionary<string, object>() { { "adaptername", adaptername } };

            WorkflowApplication wfApp =
                new WorkflowApplication(new Flowchart.CheckConnection(), inputs);

            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                string ret = e.Outputs["ret"].ToString();
                string userinput = e.Outputs["UserAnswer1"].ToString();

                string mess = "Подключения =" + ret+ ";Выбор пользователя=" + userinput;  
                MessageBox.Show(mess, "", MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);

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
                idleEvent.Set();
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
