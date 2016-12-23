using System;
using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IWMINetworkAdapterManager
    {
        NetworkAdapter GetItem(Func<NetworkAdapter, bool> predicate);
        //bool Enabled(string conn);
    }
}
