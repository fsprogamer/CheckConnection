using CheckConnection.Model;
using System.Collections.Generic;

namespace CheckConnectionWpf.Data
{
    class CompareConnectionsRepository
    {
        public Connection ActiveConnection { get; set; }
        public Connection HistoryConnection { get; set; }
        public List<CompareConnection> ComparedConnections {
            get { return CompareConnection.GetDifference( ActiveConnection, HistoryConnection); }
        }
    }
}
