using System.Collections.Generic;
using System;
using System.Management;
using System.Windows.Forms;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IWMIServiceManager
    {
        Service GetItem(Func<Service, bool> predicate);

        List<Service> GetItems();

        List<Service> GetItems(Func<Service, bool> predicate);

        IWMIManagementObjectRepo mo_repo { get; set; }
    }
}
