using System;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace CheckConnection.Model
{
    public class NetworkAdapter : INameEntity
    {
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string NetConnectionID { get; set; }
        [NotNull]
        public uint NetConnectionStatus { get; set; }
        [NotNull]
        public bool NetEnabled { get; set; }
        [NotNull]
        public string Status { get; set; }
    }
}
