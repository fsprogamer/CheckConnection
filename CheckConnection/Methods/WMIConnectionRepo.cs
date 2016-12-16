using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class WMIConnectionRepo : GenericWMIRepo<Connection>//, IWMIConnectionRepo
    {
        public WMIConnectionRepo() : base("SELECT * FROM Win32_NetworkAdapterConfiguration")
        {
            int Conn_id = 0;
            WMIManagementObjectRepo mo_repo = new WMIManagementObjectRepo(this._query);
            Context = new List<Connection>(mo_repo.Context.Count);

            foreach (ManagementObject mo in mo_repo.GetItems(m=>m.Properties["Description"].Value!=null))
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

                    Context.Add(item);
                    Conn_id++;
                }
                catch (Exception)
                {
                    log.Error("Ошибка чтения значений Connection");
                }

            }
            Context = Context.OrderByDescending(p => p.Ip_Address_v4).ToList();
        }

        public List<Connection> GetItems() {            
            return Context;
        }
    }
}
