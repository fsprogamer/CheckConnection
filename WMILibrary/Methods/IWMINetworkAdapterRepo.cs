using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IWMINetworkAdapterRepo : IGenericWMIRepo<NetworkAdapter>
    {
        WMIManagementObjectRepo mo_repo { get; set; }
    }
}
