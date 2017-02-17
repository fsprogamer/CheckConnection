using System;
using System.Windows.Forms;

using System.Collections.Generic;

using CheckConnection.Methods;
using CheckConnection.Model;
using Common;

using log4net;
using Ninject;

namespace CheckConnection
{
    public partial class ChangeConnectionForm : BaseForm
    {
        private Connection conn;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ChangeConnectionForm()
        {
            InitializeComponent();
            
            ipAddressControl.Text = Properties.Settings.Default.IPAddress; 
            NetMaskControl.Text = Properties.Settings.Default.SubnetMask;
            GatewayControl1.Text = Properties.Settings.Default.Gateway;
        }

        public ChangeConnectionForm(string pconnname)
        {
            IWMIConnectionManager cpmgr = Common.NinjectProgram.Kernel.Get<IWMIConnectionManager>();            
            conn = cpmgr.GetItem(p=>p.Name == pconnname);

            InitializeComponent();
        }

        public ChangeConnectionForm(Connection pconn)
        {
            conn = pconn;
            InitializeComponent();
        }

        private void ChangeConnectionForm_Load(object sender, EventArgs e)
        {            

            ipAddressControl.Text = conn.Ip_Address_v4;
            NetMaskControl.Text = conn.IPSubnetMask;
            DHCPServerControl.Text = conn.DHCPServer;
            textBoxDNSDomain.Text = conn.DNSDomain;

            if (conn.Gateway_list != null)
            {
                if (conn.Gateway_list.Count > 0)
                    GatewayControl1.Text = conn.Gateway_list[0].IPGateway;
                if (conn.Gateway_list.Count > 1)
                    GatewayControl2.Text = conn.Gateway_list[1].IPGateway;
            }

            if (conn.DNS_list != null)
            {
                if (conn.DNS_list.Count > 0)
                    DNSControl1.Text = conn.DNS_list[0].DNSServer;
                if (conn.DNS_list.Count > 1)
                    DNSControl2.Text = conn.DNS_list[1].DNSServer;
            }

            if (conn.DHCP_Enabled == "True")
            {
                radioButtonDHCP.Checked = true;
                radioButtonStaticIP.Checked = false;
            }
            else
            {
                radioButtonDHCP.Checked = false;
                radioButtonStaticIP.Checked = true;
            }

            SetReadonly();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            int ret = 0;            
            this.Cursor = Cursors.WaitCursor;

            conn.DHCP_Enabled = (radioButtonDHCP.Checked) ? "True" : "False";
            log.InfoFormat("conn.DHCP_Enabled = {0}", conn.DHCP_Enabled);

            conn.Ip_Address_v4 = ipAddressControl.Text;
            log.InfoFormat("conn.Ip_Address_v4 = {0}", conn.Ip_Address_v4);

            conn.IPSubnetMask = NetMaskControl.Text;
            log.InfoFormat("conn.IPSubnetMask= {0}", conn.IPSubnetMask);

            conn.DHCPServer = DHCPServerControl.Text;
            log.InfoFormat("conn.DHCPServer = {0}", conn.DHCPServer);

            conn.DNSDomain = textBoxDNSDomain.Text;
            log.InfoFormat("conn.DNSDomain = {0}", conn.DNSDomain);

            log.InfoFormat("DNSControl1.Text = {0}", DNSControl1.Text);

            if (conn.DNS_list != null)
            {
                log.Info("if conn.DNS_list != null, Clear");
                conn.DNS_list.Clear();
            }
            log.Info("conn.DNS_list = null");
            conn.DNS_list = null;
            
            if (conn.Gateway_list != null)
            {
                log.Info("if conn.Gateway_list != null, Clear");
                conn.Gateway_list.Clear();
            }
            log.Info("conn.Gateway_list = null");
            conn.Gateway_list = null;

            if (DNSControl1.Text != "...")
            {
                log.Info("in if DNSControl1.Text != \"...\"");                
                if (conn.DNS_list == null) 
                {
                    log.Info("conn.DNS_list == null");
                    log.Info("Before conn.DNS_list.Add");
                    conn.DNS_list = new List<DNS>(2);
                    conn.DNS_list.Add(new DNS() { DNSServer = DNSControl1.Text, Connection_Id = conn.Id });
                    log.Info("After conn.DNS_list.Add");
                }
                else
                {
                    log.InfoFormat("conn.DNS_list[0].DNSServer = {0}", conn.DNS_list[0].DNSServer);
                    conn.DNS_list[0].DNSServer = DNSControl1.Text;                    
                }                
                log.InfoFormat("conn.DNS_list[0].DNSServer = {0}", conn.DNS_list[0].DNSServer);                              
            }                     
            else
            {
                log.Info("in if DNSControl1.Text == \"...\"");
                if (conn.DNS_list != null)
                {
                    log.Info("conn.DNS_list != null");
                    if (conn.DNS_list.Count > 0)
                    {
                        log.InfoFormat("RemoveAt(0),conn.DNS_list.Count = {0}", conn.DNS_list.Count);
                        conn.DNS_list.RemoveAt(0);
                    }
                }
                else log.Info("conn.DNS_list == null");
            }
            
            if (DNSControl2.Text != "...")
            {
                log.Info("in if DNSControl2.Text != \"...\"");
                if (conn.DNS_list == null)                    
                {
                    log.Info("conn.DNS_list == null");
                    log.Info("Before conn.DNS_list.Add");
                    conn.DNS_list = new List<DNS>(2);
                    conn.DNS_list.Add(new DNS() { DNSServer = DNSControl2.Text, Connection_Id = conn.Id });
                    log.Info("After conn.DNS_list.Add");
                }
                else
                {
                    if (conn.DNS_list.Count == 1)
                    {
                        if (DNSControl2.Text != conn.DNS_list[0].DNSServer)
                        {
                            log.InfoFormat("conn.DNS_list.Count = {0}", conn.DNS_list.Count);
                            log.Info("Before conn.DNS_list.Add");
                            conn.DNS_list.Add(new DNS() { DNSServer = DNSControl2.Text, Connection_Id = conn.Id });
                            log.Info("After conn.DNS_list.Add");
                        }
                    }
                    else
                    {
                        log.InfoFormat("conn.DNS_list[1].DNSServer = {0}", conn.DNS_list[0].DNSServer);
                        conn.DNS_list[1].DNSServer = DNSControl2.Text;
                    }
                }
                log.InfoFormat("conn.DNS_list.Count = {0}", conn.DNS_list.Count);
            }
            else
            {
                log.Info("in if DNSControl1.Text == \"...\"");
                if (conn.DNS_list != null)
                {
                    log.Info("conn.DNS_list != null");
                    if (conn.DNS_list.Count > 1)
                    {
                        log.InfoFormat("RemoveAt(),conn.DNS_list.Count = {0}", conn.DNS_list.Count);
                        conn.DNS_list.RemoveAt(conn.DNS_list.Count - 1);
                    }
                }
                else log.Info("conn.DNS_list == null");
            }            
            
            if (GatewayControl1.Text != "...")
            {
                if (conn.Gateway_list == null)
                {
                    log.Info("conn.Gateway_list == null");
                    log.Info("Before conn.Gateway_list.Add");
                    conn.Gateway_list = new List<Gateway>(2);
                    conn.Gateway_list.Add(new Gateway() { IPGateway = GatewayControl1.Text, Connection_Id = conn.Id });
                    log.Info("After conn.Gateway_list.Add");
                }
                else
                {
                    log.InfoFormat("Gateway_list[0].DNSServer = {0}", conn.Gateway_list[0].IPGateway);
                    conn.Gateway_list[0].IPGateway = GatewayControl1.Text;
                }
                log.InfoFormat("conn.Gateway_list[0].IPGateway = {0}", conn.Gateway_list[0].IPGateway);
            }
            else
            {
                log.Info("in if GatewayControl1.Text == \"...\"");
                if (conn.Gateway_list != null)
                {
                    log.Info("conn.Gateway_list != null");
                    if (conn.Gateway_list.Count > 0)
                    {
                        log.InfoFormat("RemoveAt(0),conn.Gateway_list.Count = {0}", conn.Gateway_list.Count);
                        conn.Gateway_list.RemoveAt(0);
                    }
                }
                else log.Info("conn.Gateway_list == null");
            }

            if (GatewayControl2.Text != "...")
            {
                if (conn.Gateway_list == null)
                {
                    log.Info("conn.Gateway_list == null");
                    log.Info("Before conn.Gateway_list.Add");
                    conn.Gateway_list = new List<Gateway>(2);
                    conn.Gateway_list.Add(new Gateway() { IPGateway = GatewayControl2.Text, Connection_Id = conn.Id });
                    log.Info("After conn.Gateway_list.Add");
                }
                else
                {
                    if (conn.Gateway_list.Count == 1)
                    {
                        log.InfoFormat("conn.Gateway_list.Count = {0}", conn.Gateway_list.Count);
                        log.Info("Before conn.Gateway_list.Add");
                        conn.Gateway_list.Add(new Gateway() { IPGateway = GatewayControl2.Text, Connection_Id = conn.Id });
                        log.Info("After conn.Gateway_list.Add");
                    }
                    else { 
                        log.InfoFormat("Gateway_list[0].IPGateway = {0}", conn.Gateway_list[0].IPGateway);
                        conn.Gateway_list[1].IPGateway = GatewayControl2.Text;
                    }
                }
                log.InfoFormat("conn.Gateway_list.Count = {0}", conn.Gateway_list.Count);
            }
            else
            {
                log.Info("in if GatewayControl2.Text == \"...\"");
                if (conn.Gateway_list != null)
                {
                    log.Info("conn.Gateway_list != null");
                    if (conn.Gateway_list.Count > 1)
                    {
                        log.InfoFormat("RemoveAt(),conn.Gateway_list.Count = {0}", conn.Gateway_list.Count);
                        conn.Gateway_list.RemoveAt(conn.Gateway_list.Count - 1);
                    }
                }
                else log.Info("conn.Gateway_list == null");
            }
            
            log.Info("Before SaveConnectionParam");
            WMIConnectionManager cpmgr = new WMIConnectionManager();
            ret = cpmgr.SaveItem(conn); //wmi.SaveConnectionParam(conn);
            log.Info("After SaveConnectionParam");

            this.Cursor = Cursors.Default;
            if (ret == 1)
            {
                string mess = "Параметры подключения изменены";
                log.Info(mess);
                MessageBox.Show(mess, "", MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
            }
            else
            {
                string mess = "Параметры подключения измененить не удалось"; ;
                log.Info(mess);
                MessageBox.Show(mess, "", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);                
            }
        }

        private void radioButtonDHCP_CheckedChanged(object sender, EventArgs e)
        {
            SetReadonly();
        }

        private void SetReadonly()
        {
            if (radioButtonDHCP.Checked == true)
            {
                ipAddressControl.ReadOnly = true;
                NetMaskControl.ReadOnly = true;
                DHCPServerControl.ReadOnly = true;
                textBoxDNSDomain.ReadOnly = true;

                //DNSControl1.ReadOnly = true;
                //DNSControl2.ReadOnly = true;

                GatewayControl1.ReadOnly = true;
                GatewayControl2.ReadOnly = true;
            }
            else
            {
                ipAddressControl.ReadOnly = false;
                NetMaskControl.ReadOnly = false;
                DHCPServerControl.ReadOnly = false;
                textBoxDNSDomain.ReadOnly = false;

                //DNSControl1.ReadOnly = false;
                //DNSControl2.ReadOnly = false;

                GatewayControl1.ReadOnly = false;
                GatewayControl2.ReadOnly = false;
            }
        }

    }
}
