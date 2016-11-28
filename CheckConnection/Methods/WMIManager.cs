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

        public WMIManager()
        {            
        }

        public int QueryWMI(string query)
        {
            int ret = 0; 
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(query);
            moCollection = moSearch.Get();
            if(moCollection!=null)
             ret = moCollection.Count;
            return ret;
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
