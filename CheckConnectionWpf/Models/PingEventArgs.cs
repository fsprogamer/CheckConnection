using System;
using System.Net.NetworkInformation;

namespace CheckConnectionWpf.Models
{
    public class PingEventArgs : EventArgs
    {
        public string Destination { get; set; }

        public PingReply Reply;

        public PingEventArgs(string destination)
        {
            Destination = destination;
        }
    }

}
