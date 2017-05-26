using CheckConnection.Model;
using System;

namespace CheckConnectionWpf.Models
{
    public class CompareConnectionsEventArgs : EventArgs
    {
        public Connection firstConnection { get; set; }
        public Connection secondConnection { get; set; }
        public CompareConnectionsEventArgs(Connection firstconnection, Connection secondconnection)
        {
            firstConnection = firstconnection;
            secondConnection = secondconnection;
        }
    }
}
