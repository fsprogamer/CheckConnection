using CheckConnection.Model;
using System.Collections.Generic;

namespace CheckConnectionWpf.Views
{
    interface ICompareConnectionsView
    {
        void LoadActiveConnection(Connection connection);
        void LoadHistoryConnection(Connection connection);
    }
}
