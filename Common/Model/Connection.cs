using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckConnection.Model
{
    public class Connection : Entity, INameEntity, IEquatable<Connection>
    {
        public static string listDelimetr = "; ";
        //[PrimaryKey, AutoIncrement, Unique]
        //public int Id { get; set; }
        [NotNull, Indexed]
        [Display(Name = "Дата и время")]
        public DateTime Date { get; set; }
        [Indexed]
        [Display(Name = "Название подключения")]
        public string NetConnectionID { get; set; }
        [NotNull, Indexed]
        [Display(Name = "Название адаптера")]
        public string Name { get; set; }
        [Display(Name = "MAC адрес")]
        public string MAC { get; set; }
        [Display(Name = "IP адрес")]
        public string Ip_Address_v4 { get; set; }
        [Display(Name = "IP адрес v6")]
        public string Ip_Address_v6 { get; set; }
        [Display(Name = "DHCP включен")]
        public string DHCP_Enabled { get; set; }
        [Display(Name = "DHCP сервер")]
        public string DHCPServer { get; set; }
        [Display(Name = "Основной DNS-суффикс")]
        public string DNSDomain { get; set; }
        [Display(Name = "Маска подсети")]
        public string IPSubnetMask { get; set; }
        [Ignore]        
        [Display(Name = "Шлюзы...")]
        public string IPGateway {
            get
            {
                string lIPGateway = null;

                if (Gateway_list?.Count > 0)
                {
                    foreach (Gateway gtw in Gateway_list)
                    {
                        lIPGateway += gtw.IPGateway + listDelimetr;
                    }
                    if (lIPGateway.Length > 2)
                        lIPGateway = lIPGateway.Substring(0, lIPGateway.Length - 2);
                }
                return lIPGateway;
            }
        }
        [Ignore]
        [Display(Name = "DNS-серверы...")]
        public string DNSServer {
            get
            {
                string lDNSServer = null;

                if (DNS_list?.Count > 0)
                {
                    foreach (DNS dns in DNS_list)
                    {
                        lDNSServer += dns.DNSServer + listDelimetr;
                    }
                    if (lDNSServer.Length > 2)
                        lDNSServer = lDNSServer.Substring(0, lDNSServer.Length - 2);
                }
                return lDNSServer;
            }
        }
        [Ignore]
        public List<DNS> DNS_list { get; set; }
        [Ignore]
        public List<Gateway> Gateway_list { get; set; }
        public ushort NetConnectionStatus { get; set; }
        public bool NetEnabled { get; set; }                
        [NotNull]
        [Display(Name = "Index")]
        public uint Index { get; set; } //связь networkadapter и networkadapterconfiguration
        public string GUID { get; set; }

        public bool Equals(Connection other)
        {
            if (other == null) return false;

            if (this.Name != other.Name) return false;
            if (this.NetConnectionID != other.NetConnectionID) return false;
            if (this.MAC != other.MAC) return false;

            if (this.Ip_Address_v4 != other.Ip_Address_v4) return false;
            if (this.Ip_Address_v6 != other.Ip_Address_v6) return false;
            if (this.DHCP_Enabled != other.DHCP_Enabled) return false;
            if (this.DHCPServer != other.DHCPServer) return false;
            if (this.DNSDomain != other.DNSDomain) return false;
            if (this.IPSubnetMask != other.IPSubnetMask) return false;
            
            if (this.IPGateway != other.IPGateway) return false;
            if (this.DNSServer != other.DNSServer) return false;

            return true;
        }
              
    }
}
