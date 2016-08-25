﻿using System;
using SQLite;

namespace CheckConnection.Model
{
    class Connection
    {

        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [NotNull, Indexed]
        public DateTime Date { get; set; }
        [NotNull]
        public string Name { get; set; }
        //[NotNull]
        //public string Type { get; set; }
        //[NotNull]
        public string MAC { get; set; }
        //[NotNull]
        public string Ip_Address_v4 { get; set; }
        //[NotNull]
        public string Ip_Address_v6 { get; set; }
        //[NotNull]
        public string DHCP_Enabled { get; set; }
        //[NotNull]
        public string DHCPServer { get; set; }
        //[NotNull]
        public string DNSDomain { get; set; }
        //[NotNull]
        public string IPSubnetMask { get; set; }
        [Ignore]
        public string DefaultIPGateways { get; set; }
        [Ignore]
        public string DNS { get; set; }
    }

    class TObj
    {
        [Ignore]
        public string Ip_Address { get; set; }
        [Ignore]
        public string DNS { get; set; }
    }

    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
