using System.Linq;
using System.Management;
using System;

namespace CheckConnection.Methods
{
    public class WMIManagementObjectRepo : GenericWMIRepo<ManagementObject>, IWMIManagementObjectRepo
    {
        public int ret = 0;
        public WMIManagementObjectRepo(string scope, string query) : base(query, scope)
        {
            int ret = 0;
            log.Info("before WMIManagementObjectRepo constuctor");
            try
            {
                ManagementObjectSearcher moSearch = new ManagementObjectSearcher(scope, query);
                log.Info("after ManagementObjectSearcher");

                Context = moSearch.Get().Cast<ManagementObject>().ToList();

                //Context = moSearch.Get();
                if (Context != null)
                    ret = Context.Count;
            }
            catch(Exception e)
            {
                log.Error("exception ManagementObjectSearcher ToList()", e);
            }            
            
            log.Info("after WMIManagementObjectRepo constuctor");
        }
    }
}
