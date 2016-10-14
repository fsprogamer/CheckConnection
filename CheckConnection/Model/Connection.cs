using System;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace CheckConnection.Model
{
    public class Connection
    {

        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [NotNull, Indexed]
        [Display(Name = "Дата и время")]
        public DateTime Date { get; set; }
        [NotNull]
        [Display(Name = "Название подключения")]
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
        public string IPGateway { get; set; }
        [Ignore]
        [Display(Name = "DNS-серверы...")]
        public string DNSServer { get; set; }
    }

}
