using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class WMIConnectionRepo : GenericWMIRepo<Connection>, IWMIConnectionRepo
    {
        private WMIManagementObjectRepo _mo_repo;

        public WMIManagementObjectRepo mo_repo
        {
            get { return _mo_repo; }
            set { _mo_repo = value; }
        }
        public WMIConnectionRepo() : base("root\\CIMV2", "SELECT Description, Index, IPEnabled, DHCPEnabled, IPAddress, MACAddress, DNSDomain, IPSubnet, DefaultIPGateway, DNSServerSearchOrder, DHCPServer FROM Win32_NetworkAdapterConfiguration")
        {
            int Conn_id = 0;
            mo_repo = new WMIManagementObjectRepo(this._scope, this._query);
            Context = new List<Connection>(mo_repo.Context.Count);

            foreach (ManagementObject mo in mo_repo.GetItems(m=>m.Properties["Description"].Value!=null))
            {
                try
                {
                    //Set colDrives = oWMI.ExecQuery(@"ASSOCIATORS OF {Win32_NetworkAdapterConfiguration.DeviceID='"
                    //    & oPartition.DeviceID & "'} WHERE ResultClass=Win32_NetworkAdapter");

                    Connection item = new Connection();

                    if (mo["Index"] != null)
                    {
                        item.Index = (uint)mo["Index"];
                        log.InfoFormat("Index={0}", mo["Index"].ToString());
                    }

                    if (mo["Description"] != null)
                    {
                        item.Name = mo["Description"].ToString();
                        log.InfoFormat("{0}, IPEnabled={1}", item.Name, mo["IPEnabled"].ToString());
                    }
                    
                    item.Id = Conn_id;
                    item.Date = DateTime.Now;

                    if (mo["DHCPEnabled"] != null)
                    {
                        item.DHCP_Enabled = mo["DHCPEnabled"].ToString();
                        log.InfoFormat("DHCP_Enabled={0}", mo["DHCPEnabled"].ToString());
                    }

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
                        item.Gateway_list = new List<Gateway>(2);

                        foreach (string defaultipgateway_str in defaultgateways)
                        {
                            Gateway gtw = new Gateway
                            {
                                IPGateway = defaultipgateway_str,
                                Connection_Id = Conn_id
                            };
                            item.Gateway_list.Add(gtw);

                            item.IPGateway += defaultipgateway_str + "; ";
                        }
                        item.IPGateway = item.IPGateway.Substring(0, item.IPGateway.Length - 2);
                    }

                    if (mo["DNSServerSearchOrder"] != null)
                    {
                        string[] DNSarray = (string[])mo["DNSServerSearchOrder"];
                        int Order_Id = 0;

                        item.DNS_list = new List<DNS>(2);

                        foreach (string dns_str in DNSarray)
                        {
                            DNS _dns = new DNS
                            {
                                DNSServer = dns_str,
                                Connection_Id = Conn_id,
                                Order_Id = Order_Id
                            };
                            item.DNS_list.Add(_dns);

                            item.DNSServer += dns_str + "; ";

                            Order_Id++;
                        }
                        item.DNSServer = item.DNSServer.Substring(0, item.DNSServer.Length - 2);
                    }

                    if (mo["DHCPServer"] != null)
                        item.DHCPServer = mo["DHCPServer"].ToString();

                    Context.Add(item);
                    Conn_id++;
                }
                catch (Exception e)
                {
                    log.Error("Ошибка чтения значений Connection: ",e);
                }

            }
            Context = Context.OrderByDescending(p => p.NetConnectionID).ToList();
        }
         
    }
}
