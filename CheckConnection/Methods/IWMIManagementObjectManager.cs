using System.Management;
using System.Collections.Generic;
using System;

namespace CheckConnection.Methods
{
    public interface IWMIManagementObjectManager
    {
        List<ManagementObject> GetItems();
        ManagementObject GetItem(Func<ManagementObject, bool> predicate);
        int Count { get; }
    }
}
