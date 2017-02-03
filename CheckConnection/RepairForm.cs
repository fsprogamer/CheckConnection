using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WorkflowLib;
using Common;
using System.Text;
using System.Collections.Specialized;

namespace CheckConnection
{
    public partial class RepairForm : Form
    {
        string[] _text;
        WorkflowApplication wfApp;
        string[] strArray;         

        public RepairForm(string[] text)
        {
            InitializeComponent();
            _text = text;
            StringCollection AkadoDNS = Properties.Settings.Default.AkadoDNS;            
            strArray = new string[AkadoDNS.Count];
            AkadoDNS.CopyTo(strArray, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RepairForm_Load(object sender, EventArgs e)
        {
            
        }

        private void RepairForm_Shown(object sender, EventArgs e)
        {
            //WorkflowLib.WorkFlowApp.Run(_text);

            var inputs = new Dictionary<string, object>() { { "AkadoDNS", strArray } };

            wfApp = new WorkflowApplication(new WorkflowLib.Flowchart.CheckConnection(), inputs);

            // Configure the instance store, extensions, and 
            // workflow lifecycle handlers.
            ConfigureWorkflowApplication(wfApp);

            // Start the workflow.
            wfApp.Run();
        }

        private void ConfigureWorkflowApplication(WorkflowApplication wfApp)
        {
            // Configure the persistence store.  
            //wfApp.InstanceStore = store;

            // Add a StringWriter to the extensions. This captures the output  
            // from the WriteLine activities so we can display it in the form.  
            StringWriter sw = new StringWriter();
            wfApp.Extensions.Add(sw);


            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                string ret = e.Outputs["ret"].ToString();
                string userinput = e.Outputs["UserAnswer1"].ToString();

                string mess = "Подключения =" + ret + ";Выбор пользователя=" + userinput;
                //MessageBox.Show(mess, "", MessageBoxButtons.OK,
                //                          MessageBoxIcon.Information);

                if (e.CompletionState == ActivityInstanceState.Faulted)
                {
                    UpdateStatus(string.Format("Workflow Terminated. Exception: {0}\r\n{1}",
                        e.TerminationException.GetType().FullName,
                        e.TerminationException.Message));
                }
                else if (e.CompletionState == ActivityInstanceState.Canceled)
                {
                    UpdateStatus("Workflow Canceled.");
                }
                else
                {                    
                    UpdateStatus(mess);
                }

                //syncEvent.Set();
            };

            wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
            {
                UpdateStatus(string.Format("Workflow Aborted. Exception: {0}\r\n{1}",
                             e.Reason.GetType().FullName,
                             e.Reason.Message)
                            );
                
                //syncEvent.Set();
            };

            wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                UpdateStatus(string.Format("Unhandled Exception: {0}\r\n{1}",
                                            e.UnhandledException.GetType().FullName,
                                            e.UnhandledException.Message));
                return UnhandledExceptionAction.Terminate;
            };

            wfApp.Idle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                var writers = e.GetInstanceExtensions<StringWriter>();
                foreach (var writer in writers)
                {
                    UpdateStatus(writer.ToString());
                    StringBuilder sb = writer.GetStringBuilder();
                    sb.Remove(0, sb.Length);
                }

                //idleEvent.Set();
            };

        }

        private delegate void UpdateStatusDelegate(string msg);
        public void UpdateStatus(string msg)
        {
            // We may be on a different thread so we need to
            // make this call using BeginInvoke.
            if (InvokeRequired)
            {
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus), msg);
            }
            else
            {
                if (msg.EndsWith("\r\n"))
                {
                    msg += "\r\n";
                }
                listBox.Items.Add(msg);
                listBox.Refresh();

                wfApp.ResumeBookmark("Show", "");
            }
        }
    }
}
