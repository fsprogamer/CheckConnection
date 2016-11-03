using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Management;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class ConnectionParamManager
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private WMIInterface _wmi;

        public ConnectionParamManager(WMIInterface pwmi)
        {
            _wmi = pwmi;
        }
        public ConnectionParam GetItem(string pconnname)
        {
            return GetItems().Where(p => p.Connection.Name == pconnname).First<ConnectionParam>();
        }

        public List<ConnectionParam> GetItems()
        {
            //DNS и Gateway связаны с подключением через Connection_id
            //При чтении через WMI Connection_id формируется искусственно
            int Connection_id = 0;
            List<ConnectionParam> connparam_list = new List<ConnectionParam>(10);

            foreach (ManagementObject mo in _wmi.GetManagementObjectCollection())
            {
                ConnectionParam connparam = new ConnectionParam();
                try
                {
                    Connection item = new Connection();

                    if (mo["Description"] != null)
                        item.Name = mo["Description"].ToString();

                    item.Id = Connection_id;
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
                        connparam.Gateway_list = new List<Gateway>(2);

                        foreach (string defaultipgateway_str in defaultgateways)
                        {
                            Gateway gtw = new Gateway
                            {
                                IPGateway = defaultipgateway_str,
                                Connection_Id = Connection_id
                            };
                            connparam.Gateway_list.Add(gtw);

                            item.IPGateway += defaultipgateway_str + "; ";
                        }
                        item.IPGateway = item.IPGateway.Substring(0, item.IPGateway.Length - 2);
                    }

                    if (mo["DNSServerSearchOrder"] != null)
                    {
                        string[] DNSarray = (string[])mo["DNSServerSearchOrder"];
                        int Order_Id = 0;

                        connparam.DNS_list = new List<DNS>(2);

                        foreach (string dns_str in DNSarray)
                        {
                            DNS _dns = new DNS
                            {
                                DNSServer = dns_str,
                                Connection_Id = Connection_id,
                                Order_Id = Order_Id
                            };
                            connparam.DNS_list.Add(_dns);

                            item.DNSServer += dns_str + "; ";

                            Order_Id++;
                        }
                        item.DNSServer = item.DNSServer.Substring(0, item.DNSServer.Length - 2);
                    }

                    if (mo["DHCPServer"] != null)
                        item.DHCPServer = mo["DHCPServer"].ToString();

                    connparam.Connection = item;
                }
                catch (Exception ex)
                {
                    log.Error("Ошибка при чтении параметров подключения", ex);
                    Connection item = new Connection();
                    item.Name = "empty";
                    item.Id = Connection_id;
                    connparam.Connection = item;
                }

                connparam_list.Add(connparam);
                Connection_id++;
            }

            return connparam_list.OrderByDescending(p => p.Connection.Ip_Address_v4).ToList(); ;
        }

        public int SaveItem(ConnectionParam param)
        {
            int ret = 0;

            MObject objMO = new MObject(_wmi.GetManagementObject(param.Connection.Name));

            if (objMO.IpEnabled())
            {
                if (param.Connection.DHCP_Enabled == "True")
                {
                    log.Info("Before setDinamicIP");
                    objMO.setDinamicIP();
                    log.Info("After setDinamicIP");
                }
                else
                {
                    log.Info("Before setStaticIP");
                    objMO.setStaticIP(param.Connection.Ip_Address_v4, param.Connection.IPSubnetMask);
                    log.Info("After setStaticIP");

                    log.Info("Before setDNSDomain");
                    if (!String.IsNullOrEmpty(param.Connection.DNSDomain))
                        objMO.setDNSDomain(param.Connection.DNSDomain);
                    log.Info("After setDNSDomain");

                    log.Info("Before setGateway");
                    List<string> sGateway = new List<string>(2);
                    if (param.Gateway_list != null)
                    {
                        if ((param.Gateway_list[0] != null) &&
                        (!String.IsNullOrEmpty(param.Gateway_list[0].IPGateway))
                        )
                        {
                            sGateway.Add(param.Gateway_list[0].IPGateway);
                            if (param.Gateway_list.Count > 1)
                            {
                                if ((param.Gateway_list[1] != null) &&
                                (!String.IsNullOrEmpty(param.Gateway_list[1].IPGateway))
                                )
                                    sGateway.Add(param.Gateway_list[1].IPGateway);
                            }
                            objMO.setGateway(sGateway.ToArray());
                        }
                    }
                    log.Info("After setGateway");
                }

                log.Info("Before SetDNSServerSearchOrder");

                List<string> sDns = new List<string>(2);
                if (param.DNS_list != null)
                {
                    if ((param.DNS_list[0] != null) &&
                    (!String.IsNullOrEmpty(param.DNS_list[0].DNSServer))
                    )
                    {
                        sDns.Add(param.DNS_list[0].DNSServer);
                        if (param.DNS_list.Count > 1)
                        {
                            if ((param.DNS_list[1] != null) &&
                                (!String.IsNullOrEmpty(param.DNS_list[1].DNSServer))
                                )
                                sDns.Add(param.DNS_list[1].DNSServer);
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
       
    }
}
