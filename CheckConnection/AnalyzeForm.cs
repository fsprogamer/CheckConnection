using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CheckConnection.Methods;
using CheckConnection.Model;
using Common;


namespace CheckConnection
{
    public partial class AnalyzeForm : FormWithLog
    {
        private ConnectionParam _connparam;        
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

        public AnalyzeForm(ConnectionParam connparam)
        {
            InitializeComponent();
            _connparam = connparam;            
        }

        private void AnalyzeForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            log.InfoFormat("_connparam.Connection.Ip_Address_v4 = {0}", _connparam.Connection.Ip_Address_v4);
            analyze = new AnalyzeManager(_connparam.Connection.Ip_Address_v4, _connparam.Connection.DHCP_Enabled);
            log.Info("SetGateway");
            analyze.SetGateway(_connparam.Gateway_list);
            log.Info("SetDNS");
            analyze.SetDNS(_connparam.DNS_list);
        }

        delegate void SetTextDelegate(string ipaddress);

        void SetText(string ipaddress)
        {
            log.Info("DelegateThread запущен");
            string[] result = analyze.GetPingResult(ipaddress);
            if (result != null)
            {
                var listViewItem = new ListViewItem(result);
                if ((result.GetValue(2) != null) && (result.GetValue(2).ToString() == "Успешно"))
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

            if (_connparam.Connection.Ip_Address_v4 != null)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(ts, _connparam.Connection.Ip_Address_v4 );
                }  
            }

            if (_connparam.Gateway_list != null)
            {
                log.Info("_IPGateway != null");
                foreach (Gateway gateway in _connparam.Gateway_list)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(ts, gateway.IPGateway );
                    }
                }
            }

            if (_connparam.DNS_list != null)
            {
                log.Info("_DNS != null");
                foreach (DNS dns in _connparam.DNS_list)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(ts, dns.DNSServer );
                    }
                }
            }

            if (this.InvokeRequired)
            {
                this.BeginInvoke(ts, analyze._ProviderDefaultAddress);
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
