using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Collections.Specialized;

using Common;

namespace CheckConnection
{
    public partial class RepairForm : FormWithLogger<RepairForm>
    {
        //string[] _text;
        WorkflowApplication wfApp;
        string[] strArray;        

        public RepairForm(string[] text)
        {
            InitializeComponent();
            //_text = text;
            StringCollection AkadoDNS = Properties.Settings.Default.AkadoDNS;            
            strArray = new string[AkadoDNS.Count];
            AkadoDNS.CopyTo(strArray, 0);

            listView.Columns.Add(new ColumnHeader() { Width = listView.Width , Text = "Журнал операций" });
            listView.Dock = DockStyle.None;

            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;
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
                //string ret = e.Outputs["ret"].ToString();
                //string userinput = e.Outputs["UserAnswer1"].ToString();

                //string mess = "Подключения =" + ret + ";Выбор пользователя=" + userinput;
                //MessageBox.Show(mess, "", MessageBoxButtons.OK,
                //                          MessageBoxIcon.Information);

                if (e.CompletionState == ActivityInstanceState.Faulted)
                {
                    UpdateStatus(string.Format("Процесс остановлен. Причина: {0}\r\n{1}",
                        e.TerminationException.GetType().FullName,
                        e.TerminationException.Message));
                }
                else if (e.CompletionState == ActivityInstanceState.Canceled)
                {
                    UpdateStatus("Процесс прерван.");
                }
                else
                {                    
                    //UpdateStatus(mess);
                }

                //syncEvent.Set();
            };

            wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
            {
                UpdateStatus(string.Format("Процесс прерван. Причина: {0}",                             
                             e.Reason.Message)
                            );
                
                //syncEvent.Set();
            };

            wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                UpdateStatus(string.Format("Ошибка: {0}\r\n{1}",
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
            int i = 0;
            int ind = 0;
            string sub_str = null;
            const int list_width = 90;
            string bookmark_name = null;
            // We may be on a different thread so we need to
            // make this call using BeginInvoke.
            if (InvokeRequired)
            {
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus), msg);
            }
            else
            {
                //если работа прервана пользователем, то закладок нет
                try
                {
                    // Inspect the bookmarks
                    foreach (System.Activities.Hosting.BookmarkInfo info in wfApp.GetBookmarks())
                    {
                        bookmark_name = info.BookmarkName;
                    }
                }
                catch(Exception ex)
                {
                    log.ErrorFormat("Bookmark отсутствуют", ex);
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    log.Info(msg);
                    if(msg.IndexOf("\r\n")>0)
                    {
                        msg = msg.Substring(0,msg.Length-2);
                    }
                    while (msg.Length>0) {
                        ind = (msg.Length < list_width) ? msg.Length: msg.IndexOf(" ", list_width);
                        sub_str = msg.Substring(0, ((msg.Length < ind)||(ind<0)) ? msg.Length: ind);
 
                        //--------------------------------------------
                        var listViewItem = new ListViewItem(sub_str);
                        if (bookmark_name == "Result")           
                        {
                            listViewItem.ForeColor = System.Drawing.Color.Crimson;
                            listViewItem.Font = new System.Drawing.Font(listView.Font, System.Drawing.FontStyle.Bold);
                        }
                       
                        listView.Items.Add(listViewItem);
                        //--------------------------------------------

                        msg = msg.Substring(sub_str.Length, msg.Length-sub_str.Length);
                        i++;
                        sub_str = null;
                        ind = 0; 
                    }
                    listView.Refresh();

                    //-------------------------------------------------------

                }

                //если работа прервана пользователем, то закладок нет 
                if (!String.IsNullOrEmpty(bookmark_name))
                    wfApp.ResumeBookmark(/*"Show"*/bookmark_name, "");
            }
        }

    }
}
