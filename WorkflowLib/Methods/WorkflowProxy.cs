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
        public List<Connection> GetItems()
        {
            return cmgr.GetItems(p => p.NetConnectionID != null);
        }

        public Connection GetItem(uint Index)
        {
            return cmgr.GetItem(p => p.Index == Index);
        }
        public int Count
        {
           get { return cmgr.GetItems(p => p.NetConnectionID != null).Count; }            
        }
    }
}
