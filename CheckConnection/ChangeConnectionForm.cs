using System;
using System.Windows.Forms;
using System.Linq;
using log4net;

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
            connparam = wmi.GetNetworkDevices().Where(p => p.Connection.Name == pconnname).First<ConnectionParam>();

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

            if (DNSControl1.Text != "...")
            {
                if (connparam.DNS_list.Count == 0)
                {
                    connparam.DNS_list.Add(new DNS() { DNSServer = DNSControl1.Text, Connection_Id = connparam.Connection.Id });
                }
                else
                {
                    connparam.DNS_list[0].DNSServer = DNSControl1.Text;
                    
                }
                log.InfoFormat("connparam.DNS_list[0].DNSServer = {0}", connparam.DNS_list[0].DNSServer);
            }
            else
            {
                if (connparam.DNS_list.Count == 1)
                {
                    connparam.DNS_list.RemoveAt(1);
                }
            }

            if (DNSControl2.Text != "...")
            {
                if (connparam.DNS_list.Count == 1)
                {
                    connparam.DNS_list.Add(new DNS() { DNSServer = DNSControl2.Text, Connection_Id = connparam.Connection.Id });
                }
                else
                {
                    connparam.DNS_list[1].DNSServer = DNSControl2.Text;

                }
                log.InfoFormat("connparam.DNS_list[1].DNSServer = {0}", connparam.DNS_list[1].DNSServer);
            }
            else
            {
                if (connparam.DNS_list.Count == 2) {
                    connparam.DNS_list.RemoveAt(2);
                }
            }

            if (GatewayControl1.Text != "...")
            {
                if (connparam.Gateway_list.Count == 0)
                {
                    connparam.Gateway_list.Add(new Gateway() { IPGateway = GatewayControl1.Text, Connection_Id = connparam.Connection.Id });
                }
                else
                {
                    connparam.Gateway_list[0].IPGateway = GatewayControl1.Text;
                }
                log.InfoFormat("connparam.Gateway_list[0].IPGateway = {0}", connparam.Gateway_list[0].IPGateway);
            }
            else
            {
                if (connparam.Gateway_list.Count == 1)
                {
                    connparam.Gateway_list.RemoveAt(1);
                }
            }

            if (GatewayControl2.Text != "...")
            {
                if (connparam.Gateway_list.Count == 1)
                {
                    connparam.Gateway_list.Add(new Gateway() { IPGateway = GatewayControl2.Text, Connection_Id = connparam.Connection.Id });
                }
                else
                {
                    connparam.Gateway_list[1].IPGateway = GatewayControl2.Text;
                }
                log.InfoFormat("connparam.Gateway_list[1].IPGateway = {0}", connparam.Gateway_list[1].IPGateway);
            }
            else
            {
                if (connparam.Gateway_list.Count == 2)
                {
                    connparam.Gateway_list.RemoveAt(2);
                }
            }

            ret = wmi.SaveConnectionParam(connparam);

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

                DNSControl1.ReadOnly = true;
                DNSControl2.ReadOnly = true;

                GatewayControl1.ReadOnly = true;
                GatewayControl2.ReadOnly = true;
            }
            else
            {
                ipAddressControl.ReadOnly = false;
                NetMaskControl.ReadOnly = false;
                DHCPServerControl.ReadOnly = false;
                textBoxDNSDomain.ReadOnly = false;

                DNSControl1.ReadOnly = false;
                DNSControl2.ReadOnly = false;

                GatewayControl1.ReadOnly = false;
                GatewayControl2.ReadOnly = false;
            }
        }

    }
}
