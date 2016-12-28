using System.Collections.Generic;
using System;
using System.Management;
using System.Windows.Forms;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IWMIConnectionManager
    {
        Connection GetItem(Func<Connection, bool> predicate);

        //Connection GetItem(DataGridView dgv);

        List<Connection> GetItems();

        int SaveItem(Connection conn);

        IWMIManagementObjectRepo mo_repo { get; set; }
    }
}
