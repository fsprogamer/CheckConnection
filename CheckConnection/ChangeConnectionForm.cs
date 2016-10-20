using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

using CheckConnection.Methods;
using CheckConnection.Model;

namespace CheckConnection
{
    public partial class ChangeConnectionForm : Form
    {
        private WMIInterface wmi;
        private ConnectionParam _connparam;

        public ChangeConnectionForm(WMIInterface wmiparam)
        {
            wmi = wmiparam;
            InitializeComponent();

            ipAddressControl.Text = Properties.Settings.Default.IPAddress; 
            NetMaskControl.Text = Properties.Settings.Default.SubnetMask;
            GatewayControl1.Text = Properties.Settings.Default.Gateway;
        }

        public ChangeConnectionForm(WMIInterface wmiparam, ConnectionParam connparam)
        {
            _connparam = connparam;
            wmi = wmiparam;

            InitializeComponent();

            ipAddressControl.Text = _connparam.Connection.Ip_Address_v4;
            NetMaskControl.Text = _connparam.Connection.IPSubnetMask;

            if (_connparam.Gateway_list != null)
            {
                if (_connparam.Gateway_list.Count > 0)
                    GatewayControl1.Text = _connparam.Gateway_list[0].IPGateway;
                if (_connparam.Gateway_list.Count > 1)
                    GatewayControl2.Text = _connparam.Gateway_list[1].IPGateway;
            }

            if (_connparam.DNS_list != null)
            {
                if (_connparam.DNS_list.Count > 0)
                    DNSControl1.Text = _connparam.DNS_list[0].DNSServer;
                if (_connparam.DNS_list.Count > 1)
                    DNSControl2.Text = _connparam.DNS_list[1].DNSServer;
            }

            if (_connparam.Connection.DHCP_Enabled == "True")
            {
                radioButtonDHCP.Checked = true;
                radioButtonStaticIP.Checked = false;
            }
            else
            {
                radioButtonDHCP.Checked = false;
                radioButtonStaticIP.Checked = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (radioButtonStaticIP.Checked == true)
            {
                int ret = 0;
                ret = wmi.setStaticIP(ipAddressControl.Text,
                                      NetMaskControl.Text
                                      );

                this.Cursor = Cursors.Default;
                if (ret == 1) {
                    var message = MessageBox.Show("Подключение изменено", "", MessageBoxButtons.OK,
                                                   MessageBoxIcon.Information);
                }
            }
            else {
                if (radioButtonDHCP.Checked == true)
                {
                    int ret = 0;
                    ret = wmi.setDinamicIP();
                    this.Cursor = Cursors.Default;
                    if (ret == 1)
                    {
                        var message = MessageBox.Show("Подключение изменено", "", MessageBoxButtons.OK,
                                                       MessageBoxIcon.Information);
                    }
                }
            }
            
        }
    }
}
