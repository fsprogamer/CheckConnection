using CheckConnection.Methods;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;

using CheckConnection.Model;

using Common;

namespace WorkflowLib
{

    //public class WMIMediumTypeManagerProxy
    //{
    //    IWMIMediumTypeManager cmgr = new WMIMediumTypeManager();

    //    public IMObjectManager GetMOByName(string name)
    //    {
    //        IMObjectManager objMO = new MObjectManager(new WMIConnectionManager().mo_repo.GetItem(p => p.Properties["Description"].Value.ToString() == name));
    //        return objMO;
    //    }
    //}

    public class WMIConnectionManagerProxy: ClassWithLog
    {
        IWMIConnectionManager cmgr = new WMIConnectionManager();
        public int Count
        {
            get { return cmgr.GetItems(p => p.NetConnectionID != null).Count; }
        }

        public IMObjectManager GetMOByName(string name)
        {
            IMObjectManager objMO = new MObjectManager(new WMIConnectionManager().mo_repo.GetItem(p => p.Properties["Description"].Value.ToString() == name));
            return objMO;
        }

        public List<Connection> GetItems()
        {            
            return cmgr.GetItems(p => p.NetConnectionID != null);
        }
    }

    public class WMINetworkAdapterManagerProxy : ClassWithLog
    {
        IWMINetworkAdapterManager cmgr = new WMINetworkAdapterManager();
        List<string> exceptionConnNameLst;
        const string exConnName = "ExceptionConnName";

        public WMINetworkAdapterManagerProxy() {
                      
            exceptionConnNameLst = new List<string>(new ConfigManager().GetStringArray(exConnName));
            foreach (string ex in exceptionConnNameLst)
            {
                log.InfoFormat("Exception name: {0}", ex);
            }
        }

        public IMObjectManager GetMOByName(string name)
        {
            IMObjectManager objMO = null;
            log.InfoFormat("before GetMOByName, {0}", name);
            try
            {
                objMO = new MObjectManager(cmgr.mo_repo.GetItem(p => p.Properties["Name"].Value.ToString() == name));
                log.InfoFormat("after GetMOByName, {0}", name);                
            }
            catch (Exception ex)
            {
                log.Error("cmgr.mo_repo.GetItem", ex);
            }

            return objMO;
        }

        public IMObjectManager GetMOByIndex(uint index)
        {
            IMObjectManager objMO = null;
            log.InfoFormat("before GetMOByIndex, {0}", index);
            try
            {
                objMO = new MObjectManager(cmgr.mo_repo.GetItem(p => (uint)p.Properties["Index"].Value == index));
                log.InfoFormat("after GetMOByIndex, {0}", index);
            }
            catch (Exception ex)
            {
                log.Error("cmgr.mo_repo.GetItem", ex);
            }

            return objMO;
        }

        public IMObjectManager GetConnectionMOByIndex(uint index)
        {
            IMObjectManager objMO = null;
            log.InfoFormat("before GetConnectionMOByIndex, {0}", index);
            try
            {
                objMO = new MObjectManager(cmgr.mo_con_repo.GetItem(p => (uint)p.Properties["Index"].Value == index));
                log.InfoFormat("after GetConnectionMOByIndex, {0}", index);
            }
            catch (Exception ex)
            {
                log.Error("cmgr.mo_repo.GetItem", ex);
            }

            return objMO;
        }

        public int EnableAdapter(uint index)
        {                        
            return GetMOByIndex(index).EnableAdapter();
        }

        public int DisableAdapter(uint index)
        {
            return GetMOByIndex(index).DisableAdapter();
        }

        public int EnableAdapter(string name)
        {
            return GetMOByName(name).EnableAdapter();
        }

        public int DisableAdapter(string name)
        {
            return GetMOByName(name).DisableAdapter();
        }
        
        public int RenewDHCPLease(uint index)
        {
            return GetConnectionMOByIndex(index).RenewDHCPLease();
        }
        public int setDinamicIP(uint index)
        {
            return GetConnectionMOByIndex(index).setDinamicIP();
        }
        public List<Connection> GetItems()
        {
            /*исключаем new string[] { "virtual", "hamachi", "1394" }*/
            //List<Connection> tmp = cmgr.GetItems(p => p.NetConnectionID != null).OrderBy(s => s.Ip_Address_v4).ToList();
            return Filter(cmgr.GetItems(p => p.NetConnectionID != null).OrderBy(s => s.Ip_Address_v4).ToList(),
                          exceptionConnNameLst
                         );
        }

        private List<Connection> Filter(List<Connection> conn_list, List<string> ex_list)
        {
            bool find = false;
            List<Connection> ret = new List<Connection>(conn_list.Count);
            foreach (Connection conn in conn_list)
            {
                foreach (string ex in ex_list)
                {
                    if (conn.NetConnectionID.IndexOf(ex, StringComparison.OrdinalIgnoreCase) >= 0)
                        find = true;
                }

                if(find == false)
                    ret.Add(conn);

                find = false;
            }
            return ret;
        }

        public Connection GetItem(uint Index)
        {
            return cmgr.GetItem(p => p.Index == Index);
        }
        public int Count
        {
           /*исключаем new string[] { "virtual", "hamachi", "1394" }*/  
           get { //return cmgr.GetItems(p => p.NetConnectionID != null).Count;
                return Filter(cmgr.GetItems(p => p.NetConnectionID != null),
                              exceptionConnNameLst
                             ).Count;
               }            
        }        
    }

  
    public class WMIServiceManagerProxy : ClassWithLog
    {
        IWMIServiceManager cmgr = new WMIServiceManager();
        public IMObjectManager GetMOByName(string name)
        {
            IMObjectManager objMO = null;
            log.InfoFormat("before GetMOByName, {0}", name);
            try
            {
                objMO = new MObjectManager(cmgr.mo_repo.GetItem(p => p.Properties["Name"].Value.ToString() == name));
                log.InfoFormat("after GetMOByName, {0}", name);
            }
            catch (Exception ex)
            {
                log.Error("cmgr.mo_repo.GetItem", ex);
            }

            return objMO;
        }
        public int StartService(string name)
        {
            return GetMOByName(name).StartService();
        }

        public int StopService(string name)
        {
            return GetMOByName(name).StopService();
        }

        public string GetState(string name)
        {
            return cmgr.GetItem(p => p.Name == name).State;
        }
    }
    //NameServer = {"","11.11.11.11,22.22.22.22"}
    public class RegestryDNSManagerProxy : RegistryManager<string>
    {        
        public RegestryDNSManagerProxy(string key):base("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces\\" + key)
        {
            
        }
    }
    //ProxyEnable = {0,1}
    public class RegestryProxyEnableManagerProxy : RegistryManager<int>
    {
        public RegestryProxyEnableManagerProxy() : base("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\" )
        {
            
        }
    }
}
