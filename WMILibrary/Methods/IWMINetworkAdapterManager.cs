using System;
using System.Collections.Generic;
using CheckConnection.Model;
using System.Windows.Forms;


namespace CheckConnection.Methods
{
    public interface IWMINetworkAdapterManager
    {
        NetworkAdapter GetItem(Func<NetworkAdapter, bool> predicate);
        List<Connection> GetItems();
        Connection GetItem(DataGridView dgv);
    }
}
