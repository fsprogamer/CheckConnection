using System.Collections.Generic;
using CheckConnection.Model;
using System.Management;

namespace CheckConnection.Methods
{
    public interface WMIInterface
    {
        ManagementObject GetManagementObject(string connname);
        int GetNetworkDevicesConfig();
        int GetCurrentAccounts();
        ManagementObjectCollection GetManagementObjectCollection();
    }
}
