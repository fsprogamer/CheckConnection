using System;
using SQLite;

namespace ReestrUser
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
        //[NotNull]
        public string DefaultIPGateways { get; set; }


        //[Ignore]
        //public string FullName
        //{
        //    get
        //    {
        //        return string.Format(
        //            "{0} {1}",
        //            LastName,
        //            FirstName
        //        );
        //    }
        //}

        //public override string ToString()
        //{
        //    return string.Format(
        //        "{0}: {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}",
        //        Id,
        //        Name,
        //        Date.ToString("dd-MM-yyyy"),
        //        MAC,
        //        Ip_Address_v4,
        //        Ip_Address_v6,
        //        DHCP_Enabled,
        //        DHCPServer,
        //        DNSDomain,
        //        IPSubnetMask,
        //        DefaultIPGateways
        //    );
        //}

    }
}
