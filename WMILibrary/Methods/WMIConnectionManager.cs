using System;
using System.Collections.Generic;
using System.Windows.Forms;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{
    public class WMIConnectionManager:ClassWithLogger<WMIConnectionManager>, IWMIConnectionManager
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

        public List<Connection> GetItems(Func<Connection, bool> predicate)
        {
            return new List<Connection>(_repository.GetItems(predicate));
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

       
    }
}
