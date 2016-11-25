using System;
using System.Management;
using log4net;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public partial class WMIManager: WMIInterface
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ManagementObjectCollection moCollection;

        private const string IPEnabled_query = "SELECT * FROM Win32_NetworkAdapterConfiguration";
        //  + " WHERE IPEnabled = 'TRUE'";

        private const string CurrentAccount_query = "SELECT * FROM Win32_Account where Name='svfrolov'";
        public WMIManager()
        {
            QueryWMI(IPEnabled_query);
        }

        private int QueryWMI(string query)
        {
            int ret = 0; 
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(query);
            moCollection = moSearch.Get();
            if(moCollection!=null)
             ret = moCollection.Count;
            return ret;
        }

        public int GetNetworkDevicesConfig()
        {
           return QueryWMI(IPEnabled_query);
        }

        public int GetCurrentAccounts()
        {
            return QueryWMI(CurrentAccount_query);
        }

        public ManagementObjectCollection GetManagementObjectCollection()
        {
            return moCollection;
        }

        public ManagementObject GetManagementObject(string connname)
        {
            foreach (ManagementObject mo in moCollection)
            {
                string description = mo["Description"] as string;
                if (string.Compare(description, connname, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    return mo;
                }
            }
            return null;
        }
    }
}
