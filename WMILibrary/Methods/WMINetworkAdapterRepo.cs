using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class WMINetworkAdapterRepo : GenericWMIRepo<NetworkAdapter>, IWMINetworkAdapterRepo
    {
        private WMIManagementObjectRepo _mo_repo;

        public WMIManagementObjectRepo mo_repo
        {
            get { return _mo_repo; }
            set { _mo_repo = value; }
        }
        public WMINetworkAdapterRepo() : base("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapter")
        {
            //int Conn_id = 0;
            mo_repo = new WMIManagementObjectRepo(this._scope, this._query);
            Context = new List<NetworkAdapter>(mo_repo.Context.Count);

            foreach (ManagementObject mo in mo_repo.GetItems(m => true))
            {
                try
                {
                    NetworkAdapter item = new NetworkAdapter();

                    if (mo["Name"] != null)                    
                       item.Name = mo["Name"].ToString();                                            

                    if (mo["NetConnectionID"] != null)
                        item.NetConnectionID = mo["NetConnectionID"].ToString();

                    if (mo["NetConnectionStatus"] != null)
                        item.NetConnectionID = mo["NetConnectionStatus"].ToString();

                    if (mo["NetConnectionStatus"] != null)
                        item.NetConnectionStatus = (uint)mo["NetConnectionStatus"];

                    if (mo["NetEnabled"] != null)
                        item.NetEnabled = (bool)mo["NetEnabled"];                    

                    if (mo["Status"] != null)
                        item.Status = mo["Status"].ToString();

                    log.InfoFormat("{0}, IPEnabled={1}", item.Name, item.NetEnabled.ToString());

                    Context.Add(item);
                }
                catch (Exception)
                {
                    log.Error("Ошибка чтения значений NetworkAdapter");
                }

            }         
        }

    }
}
