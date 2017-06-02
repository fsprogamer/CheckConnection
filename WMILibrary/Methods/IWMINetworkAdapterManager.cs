using System;
using System.Collections.Generic;
using CheckConnection.Model;
using System.Windows.Forms;
using System.Management;


namespace CheckConnection.Methods
{
    public interface IWMINetworkAdapterManager
    {
        //NetworkAdapter GetItem(Func<NetworkAdapter, bool> predicate);
        Connection GetItem(Func<Connection, bool> predicate);
        //List<Connection> GetItems();
        List<Connection> GetItems(Func<NetworkAdapter, bool> predicate);
        Connection GetItem(DataGridView dgv);
        //ManagementObject GetMOItem(Func<ManagementObject, bool> predicate);

        IWMIManagementObjectRepo mo_repo
        {
            get;
            set;
        }

        IWMIManagementObjectRepo mo_con_repo
        {
            get;
            set;
        }
    }
}
