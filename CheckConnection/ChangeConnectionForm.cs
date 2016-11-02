using System;
using System.Windows.Forms;
using System.Linq;
using log4net;
using System.Collections.Generic;

using CheckConnection.Methods;
using CheckConnection.Model;

namespace CheckConnection
{
    public partial class ChangeConnectionForm : Form
    {
        private WMIInterface wmi;
        private ConnectionParam connparam;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ChangeConnectionForm(WMIInterface wmiparam)
        {
            wmi = wmiparam;
            InitializeComponent();

            ipAddressControl.Text = Properties.Settings.Default.IPAddress; 
            NetMaskControl.Text = Properties.Settings.Default.SubnetMask;
            GatewayControl1.Text = Properties.Settings.Default.Gateway;
        }

        public ChangeConnectionForm(WMIInterface wmiparam, string pconnname)
        {            
            wmi = wmiparam;
            ConnectionParamManager cpmgr = new ConnectionParamManager(wmi);
            connparam = cpmgr.GetItem(pconnname);

            InitializeComponent();
        }

        public ChangeConnectionForm(WMIInterface wmiparam, ConnectionParam pconnparam)
        {
            wmi = wmiparam;
            connparam = pconnparam;

            InitializeComponent();
        }

        private void ChangeConnectionForm_Load(object sender, EventArgs e)
        {            

            ipAddressControl.Text = connparam.Connection.Ip_Address_v4;
            NetMaskControl.Text = connparam.Connection.IPSubnetMask;
            DHCPServerControl.Text = connparam.Connection.DHCPServer;
            textBoxDNSDomain.Text = connparam.Connection.DNSDomain;

            if (connparam.Gateway_list != null)
            {
                if (connparam.Gateway_list.Count > 0)
                    GatewayControl1.Text = connparam.Gateway_list[0].IPGateway;
                if (connparam.Gateway_list.Count > 1)
                    GatewayControl2.Text = connparam.Gateway_list[1].IPGateway;
            }

            if (connparam.DNS_list != null)
            {
                if (connparam.DNS_list.Count > 0)
                    DNSControl1.Text = connparam.DNS_list[0].DNSServer;
                if (connparam.DNS_list.Count > 1)
                    DNSControl2.Text = connparam.DNS_list[1].DNSServer;
            }

            if (connparam.Connection.DHCP_Enabled == "True")
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

            connparam.Connection.DHCP_Enabled = (radioButtonDHCP.Checked) ? "True" : "False";
            log.InfoFormat("connparam.Connection.DHCP_Enabled = {0}", connparam.Connection.DHCP_Enabled);

            connparam.Connection.Ip_Address_v4 = ipAddressControl.Text;
            log.InfoFormat("connparam.Connection.Ip_Address_v4 = {0}", connparam.Connection.Ip_Address_v4);

            connparam.Connection.IPSubnetMask = NetMaskControl.Text;
            log.InfoFormat("connparam.Connection.IPSubnetMask= {0}", connparam.Connection.IPSubnetMask);

            connparam.Connection.DHCPServer = DHCPServerControl.Text;
            log.InfoFormat("connparam.Connection.DHCPServer = {0}", connparam.Connection.DHCPServer);

            connparam.Connection.DNSDomain = textBoxDNSDomain.Text;
            log.InfoFormat("connparam.Connection.DNSDomain = {0}", connparam.Connection.DNSDomain);

            log.InfoFormat("DNSControl1.Text = {0}", DNSControl1.Text);

            if (connparam.DNS_list != null)
            {
                log.Info("if connparam.DNS_list != null, Clear");
                connparam.DNS_list.Clear();
            }
            log.Info("connparam.DNS_list = null");
            connparam.DNS_list = null;
            
            if (connparam.Gateway_list != null)
            {
                log.Info("if connparam.Gateway_list != null, Clear");
                connparam.Gateway_list.Clear();
            }
            log.Info("connparam.Gateway_list = null");
            connparam.Gateway_list = null;

            if (DNSControl1.Text != "...")
            {
                log.Info("in if DNSControl1.Text != \"...\"");                
                if (connparam.DNS_list == null) 
                {
                    log.Info("connparam.DNS_list == null");
                    log.Info("Before connparam.DNS_list.Add");
                    connparam.DNS_list = new List<DNS>(2);
                    connparam.DNS_list.Add(new DNS() { DNSServer = DNSControl1.Text, Connection_Id = connparam.Connection.Id });
                    log.Info("After connparam.DNS_list.Add");
                }
                else
                {
                    log.InfoFormat("connparam.DNS_list[0].DNSServer = {0}", connparam.DNS_list[0].DNSServer);
                    connparam.DNS_list[0].DNSServer = DNSControl1.Text;                    
                }                
                log.InfoFormat("connparam.DNS_list[0].DNSServer = {0}", connparam.DNS_list[0].DNSServer);                              
            }                     
            else
            {
                log.Info("in if DNSControl1.Text == \"...\"");
                if (connparam.DNS_list != null)
                {
                    log.Info("connparam.DNS_list != null");
                    if (connparam.DNS_list.Count > 0)
                    {
                        log.InfoFormat("RemoveAt(0),connparam.DNS_list.Count = {0}", connparam.DNS_list.Count);
                        connparam.DNS_list.RemoveAt(0);
                    }
                }
                else log.Info("connparam.DNS_list == null");
            }
            
            if (DNSControl2.Text != "...")
            {
                log.Info("in if DNSControl2.Text != \"...\"");
                if (connparam.DNS_list == null)                    
                {
                    log.Info("connparam.DNS_list == null");
                    log.Info("Before connparam.DNS_list.Add");
                    connparam.DNS_list = new List<DNS>(2);
                    connparam.DNS_list.Add(new DNS() { DNSServer = DNSControl2.Text, Connection_Id = connparam.Connection.Id });
                    log.Info("After connparam.DNS_list.Add");
                }
                else
                {
                    if (connparam.DNS_list.Count == 1)
                    {
                        if (DNSControl2.Text != connparam.DNS_list[0].DNSServer)
                        {
                            log.InfoFormat("connparam.DNS_list.Count = {0}", connparam.DNS_list.Count);
                            log.Info("Before connparam.DNS_list.Add");
                            connparam.DNS_list.Add(new DNS() { DNSServer = DNSControl2.Text, Connection_Id = connparam.Connection.Id });
                            log.Info("After connparam.DNS_list.Add");
                        }
                    }
                    else
                    {
                        log.InfoFormat("connparam.DNS_list[1].DNSServer = {0}", connparam.DNS_list[0].DNSServer);
                        connparam.DNS_list[1].DNSServer = DNSControl2.Text;
                    }
                }
                log.InfoFormat("connparam.DNS_list.Count = {0}", connparam.DNS_list.Count);
            }
            else
            {
                log.Info("in if DNSControl1.Text == \"...\"");
                if (connparam.DNS_list != null)
                {
                    log.Info("connparam.DNS_list != null");
                    if (connparam.DNS_list.Count > 1)
                    {
                        log.InfoFormat("RemoveAt(),connparam.DNS_list.Count = {0}", connparam.DNS_list.Count);
                        connparam.DNS_list.RemoveAt(connparam.DNS_list.Count - 1);
                    }
                }
                else log.Info("connparam.DNS_list == null");
            }            
            
            if (GatewayControl1.Text != "...")
            {
                if (connparam.Gateway_list == null)
                {
                    log.Info("connparam.Gateway_list == null");
                    log.Info("Before connparam.Gateway_list.Add");
                    connparam.Gateway_list = new List<Gateway>(2);
                    connparam.Gateway_list.Add(new Gateway() { IPGateway = GatewayControl1.Text, Connection_Id = connparam.Connection.Id });
                    log.Info("After connparam.Gateway_list.Add");
                }
                else
                {
                    log.InfoFormat("Gateway_list[0].DNSServer = {0}", connparam.Gateway_list[0].IPGateway);
                    connparam.Gateway_list[0].IPGateway = GatewayControl1.Text;
                }
                log.InfoFormat("connparam.Gateway_list[0].IPGateway = {0}", connparam.Gateway_list[0].IPGateway);
            }
            else
            {
                log.Info("in if GatewayControl1.Text == \"...\"");
                if (connparam.Gateway_list != null)
                {
                    log.Info("connparam.Gateway_list != null");
                    if (connparam.Gateway_list.Count > 0)
                    {
                        log.InfoFormat("RemoveAt(0),connparam.Gateway_list.Count = {0}", connparam.Gateway_list.Count);
                        connparam.Gateway_list.RemoveAt(0);
                    }
                }
                else log.Info("connparam.Gateway_list == null");
            }

            if (GatewayControl2.Text != "...")
            {
                if (connparam.Gateway_list == null)
                {
                    log.Info("connparam.Gateway_list == null");
                    log.Info("Before connparam.Gateway_list.Add");
                    connparam.Gateway_list = new List<Gateway>(2);
                    connparam.Gateway_list.Add(new Gateway() { IPGateway = GatewayControl2.Text, Connection_Id = connparam.Connection.Id });
                    log.Info("After connparam.Gateway_list.Add");
                }
                else
                {
                    if (connparam.Gateway_list.Count == 1)
                    {
                        log.InfoFormat("connparam.Gateway_list.Count = {0}", connparam.Gateway_list.Count);
                        log.Info("Before connparam.Gateway_list.Add");
                        connparam.Gateway_list.Add(new Gateway() { IPGateway = GatewayControl2.Text, Connection_Id = connparam.Connection.Id });
                        log.Info("After connparam.Gateway_list.Add");
                    }
                    else { 
                        log.InfoFormat("Gateway_list[0].IPGateway = {0}", connparam.Gateway_list[0].IPGateway);
                        connparam.Gateway_list[1].IPGateway = GatewayControl2.Text;
                    }
                }
                log.InfoFormat("connparam.Gateway_list.Count = {0}", connparam.Gateway_list.Count);
            }
            else
            {
                log.Info("in if GatewayControl2.Text == \"...\"");
                if (connparam.Gateway_list != null)
                {
                    log.Info("connparam.Gateway_list != null");
                    if (connparam.Gateway_list.Count > 1)
                    {
                        log.InfoFormat("RemoveAt(),connparam.Gateway_list.Count = {0}", connparam.Gateway_list.Count);
                        connparam.Gateway_list.RemoveAt(connparam.Gateway_list.Count - 1);
                    }
                }
                else log.Info("connparam.Gateway_list == null");
            }
            
            log.Info("Before SaveConnectionParam");
            ConnectionParamManager cpmgr = new ConnectionParamManager(wmi);
            ret = cpmgr.SaveItem(connparam); //wmi.SaveConnectionParam(connparam);
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
