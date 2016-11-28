using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Management;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    class WMIConnectionManager
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private WMIInterface _wmi;

        public WMIConnectionManager(WMIInterface pwmi)
        {
            _wmi = pwmi;
        }

        public int GetNetworkDevicesConfig()
        {
           const string IPEnabled_query = "SELECT * FROM Win32_NetworkAdapterConfiguration";
           //  + " WHERE IPEnabled = 'TRUE'";
           return _wmi.QueryWMI(IPEnabled_query);
        }

        public List<Connection> GetItems()
            {
                int Conn_id = 0;
                List<Connection> Connection_list = new List<Connection>(10);

                foreach (ManagementObject mo in _wmi.GetManagementObjectCollection())
                {
                    try
                    {
                        Connection item = new Connection();

                        if (mo["Description"] != null)
                        {
                            item.Name = mo["Description"].ToString();
                            log.InfoFormat("{0}, IPEnabled={1}", item.Name, mo["IPEnabled"].ToString());
                        }

                        item.Id = Conn_id;
                        item.Date = DateTime.Now;

                        if (mo["DHCPEnabled"] != null)
                            item.DHCP_Enabled = mo["DHCPEnabled"].ToString();

                        if (mo["IPAddress"] != null)
                        {
                            string[] addresses = (string[])mo["IPAddress"];
                            item.Ip_Address_v4 = addresses[0];
                            if (addresses.Length > 1)
                                item.Ip_Address_v6 = addresses[1];
                        }

                        if (mo["MACAddress"] != null)
                            item.MAC = mo["MACAddress"].ToString();

                        if (mo["DNSDomain"] != null)
                            item.DNSDomain = mo["DNSDomain"].ToString();

                        if (mo["IPSubnet"] != null)
                        {
                            string[] subnets = (string[])mo["IPSubnet"];
                            foreach (string ipsubnet in subnets)
                            {
                                item.IPSubnetMask = ipsubnet;
                                break;
                            }
                        }

                        if (mo["DefaultIPGateway"] != null)
                        {
                            string[] defaultgateways = (string[])mo["DefaultIPGateway"];
                            foreach (string defaultipgateway in defaultgateways)
                            {
                                item.IPGateway = item.IPGateway + defaultipgateway + "; ";
                            }
                            item.IPGateway = item.IPGateway.Substring(0, item.IPGateway.Length - 2);
                        }

                        if (mo["DNSServerSearchOrder"] != null)
                        {
                            string[] DNSarray = (string[])mo["DNSServerSearchOrder"];
                            foreach (string dns in DNSarray)
                            {
                                item.DNSServer = item.DNSServer + dns + "; ";
                            }
                            item.DNSServer = item.DNSServer.Substring(0, item.DNSServer.Length - 2);
                        }


                        if (mo["DHCPServer"] != null)
                            item.DHCPServer = mo["DHCPServer"].ToString();

                        Connection_list.Add(item);
                        Conn_id++;
                    }
                    catch (Exception)
                    {
                        Connection item = new Connection();
                        item.Name = "empty";
                        item.Id = Conn_id;
                        Connection_list.Add(item);
                    }
                    //Только одно подключение
                    //break;
                }

                return Connection_list.OrderByDescending(p => p.Ip_Address_v4).ToList(); ;
            }

        public Connection GetItemByName(string connname)
        {
            int Conn_id = 0;
            Connection item = new Connection();
            ManagementObject mo = _wmi.GetManagementObject(connname);

            if (mo != null)
            {
                try
                {
                    if (mo["Description"] != null)
                        item.Name = mo["Description"].ToString();

                    item.Id = Conn_id;
                    item.Date = DateTime.Now;

                    if (mo["DHCPEnabled"] != null)
                        item.DHCP_Enabled = mo["DHCPEnabled"].ToString();

                    if (mo["IPAddress"] != null)
                    {
                        string[] addresses = (string[])mo["IPAddress"];
                        item.Ip_Address_v4 = addresses[0];
                        if (addresses.Length > 1)
                            item.Ip_Address_v6 = addresses[1];
                    }

                    if (mo["MACAddress"] != null)
                        item.MAC = mo["MACAddress"].ToString();

                    if (mo["DNSDomain"] != null)
                        item.DNSDomain = mo["DNSDomain"].ToString();

                    if (mo["IPSubnet"] != null)
                    {
                        string[] subnets = (string[])mo["IPSubnet"];
                        foreach (string ipsubnet in subnets)
                        {
                            item.IPSubnetMask = ipsubnet;
                            break;
                        }
                    }

                    if (mo["DefaultIPGateway"] != null)
                    {
                        string[] defaultgateways = (string[])mo["DefaultIPGateway"];
                        foreach (string defaultipgateway in defaultgateways)
                        {
                            item.IPGateway = item.IPGateway + defaultipgateway + "; ";
                        }
                        item.IPGateway = item.IPGateway.Substring(0, item.IPGateway.Length - 2);
                    }

                    if (mo["DNSServerSearchOrder"] != null)
                    {
                        string[] DNSarray = (string[])mo["DNSServerSearchOrder"];
                        foreach (string dns in DNSarray)
                        {
                            item.DNSServer = item.DNSServer + dns + "; ";
                        }
                        item.DNSServer = item.DNSServer.Substring(0, item.DNSServer.Length - 2);
                    }

                    if (mo["DHCPServer"] != null)
                        item.DHCPServer = mo["DHCPServer"].ToString();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Ошибка при чтении параметров подключения", ex);
                }
            }

            return item;
        }
    }
}
