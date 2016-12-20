using System.Linq;
using System.Management;

namespace CheckConnection.Methods
{
    public class WMIManagementObjectRepo : GenericWMIRepo<ManagementObject>, IWMIManagementObjectRepo
    {
        public int ret = 0;
        public WMIManagementObjectRepo(string scope, string query) : base(query, scope)
        {
            int ret = 0;
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(scope, query);

            Context = moSearch.Get().Cast<ManagementObject>().ToList();

            //Context = moSearch.Get();
            if (Context != null)
                ret = Context.Count;
        }
    }
}
