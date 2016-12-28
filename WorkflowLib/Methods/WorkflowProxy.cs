using CheckConnection.Methods;
using System.Linq;
using System.Linq.Expressions;

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

    public class WMIConnectionManagerProxy
    {
        IWMIConnectionManager cmgr = new WMIConnectionManager();
        public int Count()
        {
            return cmgr.GetItems().Count;
        }

        public IMObjectManager GetMOByName(string name)
        {
            IMObjectManager objMO = new MObjectManager(new WMIConnectionManager().mo_repo.GetItem(p => p.Properties["Description"].Value.ToString() == name));
            return objMO;
        }
    }

    public class WMINetworkAdapterManagerProxy
    {
        IWMINetworkAdapterManager cmgr = new WMINetworkAdapterManager();

        public IMObjectManager GetMOByName(string name)
        {
            IMObjectManager objMO = new MObjectManager(new WMINetworkAdapterManager().mo_repo.GetItem(p => p.Properties["Description"].Value.ToString() == name));
            return objMO;
        }

        public int EnableAdapter(string name)
        {
            
            return GetMOByName(name).EnableAdapter();
        }
    }
}
