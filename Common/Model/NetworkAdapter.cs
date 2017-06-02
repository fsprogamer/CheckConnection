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
        public ushort NetConnectionStatus { get; set; }
        [NotNull]
        public uint Index { get; set; }        
        [NotNull]
        public bool NetEnabled { get; set; }
        //[NotNull]
        public string GUID { get; set; }
    }
}
