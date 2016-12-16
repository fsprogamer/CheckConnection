using System.Linq;
using System.Management;

namespace CheckConnection.Methods
{
    class WMIManagementObjectRepo : GenericWMIRepo<ManagementObject>
    {
        public int ret = 0;
        public WMIManagementObjectRepo(string query) : base(query)
        {
            int ret = 0;

            //ManagementObjectSearcher moSearch = new ManagementObjectSearcher("root\\CIMV2", query);
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher("root\\wmi", query);
            
            Context = moSearch.Get().Cast<ManagementObject>().ToList();

            //Context = moSearch.Get();
            if (Context != null)
                ret = Context.Count;
        }
    }
}
