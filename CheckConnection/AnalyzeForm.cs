using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CheckConnection.Methods;
using CheckConnection.Model;
using Common;
using log4net;

namespace CheckConnection
{
    public partial class AnalyzeForm : FormWithLogger<AnalyzeForm>
    {
        private Connection _conn;        
        private AnalyzeManager analyze;       
        
        //public delegate int BinaryOp(int data, int time);

        //static int DelegateThread(int data, int time)
        //{
        //    Console.WriteLine("DelegateThread запущен");
        //    // Делаем задержку, для эмуляции длительной операции
        //    Thread.Sleep(time);
        //    Console.WriteLine("DelegateThread завершен");
        //    return ++data;
        //}

        //private void OnGetHostEntry(IAsyncResult ar)
        //{
        //    try
        //    {
        //        ListViewItem.ListViewSubItem hostNameItem = ar.AsyncState as ListViewItem.ListViewSubItem;
        //        ThreadSwitch delg = delegate {
        //            hostNameItem.Text = Dns.EndGetHostEntry(ar).HostName;
        //        };

        //        this.Invoke(delg);
        //    }
        //    catch (SocketException ex)
        //    {
        //        Trace.WriteLine(ex.ToString());
        //    }
        //}

        public AnalyzeForm(Connection conn)
        {
            InitializeComponent();
            _conn = conn;

            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;
        }

        private void AnalyzeForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            log.InfoFormat("conn.Ip_Address_v4 = {0}", _conn.Ip_Address_v4);
            analyze = new AnalyzeManager(_conn);
        }

        delegate void SetTextDelegate(string ipaddress);

        void SetText(string ipaddress)
        {
            log.Info("DelegateThread запущен");
            string[] result = analyze.GetPingResult(ipaddress);
            if (result != null)
            {
                var listViewItem = new ListViewItem(result);
                if ((result.GetValue(2) != null) && 
                    (result.GetValue(2).ToString() == "Успешно")||(result.GetValue(2).ToString() == "Success")
                   )
                {
                    listViewItem.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    listViewItem.BackColor = System.Drawing.Color.Crimson;
                    listViewItem.ForeColor = System.Drawing.Color.WhiteSmoke;
                }
                listViewResults.Items.Add(listViewItem);
            }
            listBoxConclusion.DataSource = analyze.MakeConclusion();
            listViewResults.Refresh();            
            log.Info("DelegateThread завершен");            
        }

        private void FillLisView()
        {
            SetTextDelegate ts = SetText;

            if (_conn.Ip_Address_v4 != null)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(ts, _conn.Ip_Address_v4 );
                }  
            }

            if (_conn.Gateway_list != null)
            {
                log.Info("_IPGateway != null");
                foreach (Gateway gateway in _conn.Gateway_list)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(ts, gateway.IPGateway );
                    }
                }
            }

            if (_conn.DNS_list != null)
            {
                log.Info("_DNS != null");
                foreach (DNS dns in _conn.DNS_list)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(ts, dns.DNSServer );
                    }
                }
            }

            if (this.InvokeRequired)
            {
                this.BeginInvoke(ts, analyze.ProviderDefaultAddress);
            }            
        }

        private void AnalyzeForm_Shown(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(FillLisView));            
            t.Start();

            analyze.CompareWithStandartParam();
            
            this.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }            

    }
}
