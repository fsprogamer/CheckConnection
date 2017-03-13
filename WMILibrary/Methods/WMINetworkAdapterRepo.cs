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
        public WMINetworkAdapterRepo() : base("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapter") //NetConnectionID, Name, Index, NetConnectionStatus
        {
            //"SELECT * FROM Win32_NetworkAdapter where not name like '%virtual%' and not name like '%hamachi%' and not name like '%1394%'"
            mo_repo = new WMIManagementObjectRepo(this._scope, this._query);
            Context = new List<NetworkAdapter>(mo_repo.Context.Count);

            foreach (ManagementObject mo in mo_repo.GetItems())
            {
                try
                {
                    NetworkAdapter item = new NetworkAdapter();
                                  
                    item.Name = mo["name"]?.ToString();                                            
                    item.NetConnectionID = mo["NetConnectionID"]?.ToString();

                    if (mo["NetConnectionStatus"] != null)
                        item.NetConnectionStatus = (ushort)mo["NetConnectionStatus"];

                    if (mo["Index"] != null)
                        item.Index = (uint)mo["Index"];

                    // В XP GUID нет
                    //if (mo["GUID"] != null)
                    //    item.GUID = mo["GUID"].ToString();

                    //if (mo["netenabled"] != null)
                    //    item.NetEnabled = (bool)mo["netenabled"];

                    log.InfoFormat("{0}", item.Name);
                    log.InfoFormat("NetConnectionID={0}, NetConnectionStatus={1}", item.NetConnectionID, item.NetConnectionStatus.ToString());
                    log.InfoFormat("Index={0}", item.Index.ToString());

                    Context.Add(item);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка чтения значений NetworkAdapter:"+e.Message);
                }

            }         
        }

    }
}
