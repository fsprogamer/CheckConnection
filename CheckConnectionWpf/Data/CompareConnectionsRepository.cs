using CheckConnection.Model;

namespace CheckConnectionWpf.Data
{
    class CompareConnectionsRepository
    {
        public Connection ActiveConnection { get; set; }
        public Connection HistoryConnection { get; set; }
    }
}
