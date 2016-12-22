using System;
using System.Collections.Generic;
using System.Windows.Forms;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{
    public class WMIConnectionManager:ClassWithLog, IWMIConnectionManager
    {        
        private readonly IWMIConnectionRepo _repository;
        
        public WMIConnectionManager()
        {
            _repository = new WMIConnectionRepo();            
        }

        public IWMIManagementObjectRepo mo_repo
        {
            get { return _repository.mo_repo; }
            set { mo_repo = value; }
        }

        public Connection GetItem(Func<Connection, bool> predicate)
        {
            return _repository.GetItem(predicate);
        }

        public List<Connection> GetItems()
        {
            return new List<Connection>(_repository.GetItems());
        }

        public int SaveItem(Connection conn)
        {
            int ret = 0;

            IMObjectManager objMO = new MObjectManager(_repository.mo_repo.GetItem(p => p.Properties["Description"].Value.ToString() == conn.Name) /*.GetManagementObject(param.Connection.Name)*/);

            if (objMO.IpEnabled)
            {
                if (conn.DHCP_Enabled == "True")
                {
                    log.Info("Before setDinamicIP");
                    objMO.setDinamicIP();
                    log.Info("After setDinamicIP");
                }
                else
                {
                    log.Info("Before setStaticIP");
                    objMO.setStaticIP(conn.Ip_Address_v4, conn.IPSubnetMask);
                    log.Info("After setStaticIP");

                    log.Info("Before setDNSDomain");
                    if (!String.IsNullOrEmpty(conn.DNSDomain))
                        objMO.setDNSDomain(conn.DNSDomain);
                    log.Info("After setDNSDomain");

                    log.Info("Before setGateway");
                    List<string> sGateway = new List<string>(2);
                    if (conn.Gateway_list != null)
                    {
                        if ((conn.Gateway_list[0] != null) &&
                        (!String.IsNullOrEmpty(conn.Gateway_list[0].IPGateway))
                        )
                        {
                            sGateway.Add(conn.Gateway_list[0].IPGateway);
                            if (conn.Gateway_list.Count > 1)
                            {
                                if ((conn.Gateway_list[1] != null) &&
                                (!String.IsNullOrEmpty(conn.Gateway_list[1].IPGateway))
                                )
                                    sGateway.Add(conn.Gateway_list[1].IPGateway);
                            }
                            objMO.setGateway(sGateway.ToArray());
                        }
                    }
                    log.Info("After setGateway");
                }

                log.Info("Before SetDNSServerSearchOrder");

                List<string> sDns = new List<string>(2);
                if (conn.DNS_list != null)
                {
                    if ((conn.DNS_list[0] != null) &&
                    (!String.IsNullOrEmpty(conn.DNS_list[0].DNSServer))
                    )
                    {
                        sDns.Add(conn.DNS_list[0].DNSServer);
                        if (conn.DNS_list.Count > 1)
                        {
                            if ((conn.DNS_list[1] != null) &&
                                (!String.IsNullOrEmpty(conn.DNS_list[1].DNSServer))
                                )
                                sDns.Add(conn.DNS_list[1].DNSServer);
                        }
                        objMO.setDNSServerSearchOrder(sDns.ToArray());
                    }
                }
                else
                {//Удаляем все DNS, передаем пустышку
                    objMO.setDNSServerSearchOrder(sDns.ToArray());
                }
                log.Info("After SetDNSServerSearchOrder");

                ret = 1;
            }
            return ret;
        }

        public List<DNS> setDNSServerSearchOrder(string strdns)
        {
            int i = 0;
            List<DNS> DNS_list = null;
            log.Info("before setDNSServerSearchOrder");
            if (!string.IsNullOrEmpty(strdns))
            {
                DNS_list = new List<DNS>(2);
                string[] dns_array = strdns.Split(';');
                foreach (string dns in dns_array)
                {
                    DNS_list.Add(new DNS { DNSServer = dns, Order_Id = i });
                    i++;
                }
            }
            log.Info("after setDNSServerSearchOrder");
            return DNS_list;
        }
        public List<Gateway> setGateway(string strgateway)
        {
            List<Gateway> Gateway_list = null;
            log.Info("before setGateway");
            if (!string.IsNullOrEmpty(strgateway))
            {
                Gateway_list = new List<Gateway>(2);
                string[] gateway_array = strgateway.Split(';');
                foreach (string gateway in gateway_array)
                {
                    Gateway_list.Add(new Gateway { IPGateway = gateway });
                }
            }
            log.Info("after setGateway");
            return Gateway_list;
        }

        public Connection GetItem(DataGridView dgv)
        {
            log.Info("before GetItem");

            int selectedRow = 0;
            if (dgv.SelectedRows.Count > 0)
                selectedRow = dgv.SelectedRows[0].Index;
            
            if (dgv.RowCount > 0)
            {
                string Name = dgv.Rows[selectedRow].Cells["Name"].Value.ToString();

                Connection conn = new Connection();

                conn.Name = Name;

                if (dgv.Rows[selectedRow].Cells["Ip_Address_v4"].Value != null)
                    conn.Ip_Address_v4 = dgv.Rows[selectedRow].Cells["Ip_Address_v4"].Value.ToString();

                if (dgv.Rows[selectedRow].Cells["IpSubnetMask"].Value != null)
                    conn.IPSubnetMask = dgv.Rows[selectedRow].Cells["IpSubnetMask"].Value.ToString();

                if (dgv.Rows[selectedRow].Cells["DNSDomain"].Value != null)
                    conn.DNSDomain = dgv.Rows[selectedRow].Cells["DNSDomain"].Value.ToString();

                if (dgv.Rows[selectedRow].Cells["DHCP_Enabled"].Value != null)
                    conn.DHCP_Enabled = dgv.Rows[selectedRow].Cells["DHCP_Enabled"].Value.ToString();

                if (dgv.Rows[selectedRow].Cells["DNSServer"].Value != null)
                    conn.DNS_list = setDNSServerSearchOrder(dgv.Rows[selectedRow].Cells["DNSServer"].Value.ToString());

                if (dgv.Rows[selectedRow].Cells["IPGateway"].Value != null)
                    conn.Gateway_list = setGateway(dgv.Rows[selectedRow].Cells["IPGateway"].Value.ToString());

                return conn;
            }
            log.Info("after GetItem");
            return null;
        }

    }
}
