using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    interface IConnectionManager
    {
        Connection GetConnection(int id);
        IList<Connection> GetConnections();
        IList<Connection> GetConnections(int Offset, int Pagesize);
        IList<Connection> GetConnectionsByName(string Name, int Offset, int Pagesize);
        int GetConnectionsAmount();
        int GetConnectionsAmountByName(string Name);
        int SaveConnection(Connection conn);
        int DeleteConnection(int id);
        int GetLastInsertRowId();
        int GetDiffInDays();
    }
}
