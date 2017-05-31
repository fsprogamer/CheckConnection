using CheckConnection.Model;
using CheckConnectionWpf.Data;
using System.Collections.Generic;

namespace CheckConnectionWpf.Views
{
    interface ICompareConnectionsView
    {
        //void LoadActiveConnection(Connection connection);
        //void LoadHistoryConnection(Connection connection);
        void LoadConnections(List<CompareConnection> connection);
    }
}
