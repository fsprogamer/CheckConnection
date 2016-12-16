using System.Collections.Generic;
using System;
using System.Management;

namespace CheckConnection.Methods
{

    public interface WMIInterface
    {
        ManagementObject GetManagementObject(string connname);
        int QueryWMI(string wmiquery);
        //int GetNetworkDevicesConfig();
        //int GetCurrentAccounts();
        ManagementObjectCollection GetManagementObjectCollection();
    }
}
