using CheckConnection.Model;
using System;

namespace CheckConnectionWpf.Models
{
    public class ConnectionEventArgs : EventArgs
    {
        public Connection Connection { get; set; }
        public ConnectionEventArgs(Connection connection)
        {
            Connection = connection;
        }
    }
}
