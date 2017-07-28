using CheckConnection.Model;
using System;
using System.ComponentModel;

namespace CheckConnectionWpf.Data
{
    public class LocalConnection
    {        
        public bool DHCP_Enabled
        {
            get;
            set;
        }
        public string[] Ip_Address_v4
        {
            get;
            set;
        }
        public string[] IPSubnetMask
        {
            get;
            set;
        }
        public string[] DHCPServer
        {
            get;
            set;
        }
        public string DNSDomain
        {
            get;
            set;
        }
        public string[][] DNS_list
        {
            get;
            set;
        }
        public string[][] Gateway_list
        {
            get;
            set;
        }
    }

    //public string[] IpAddress
    //{
    //    get { return _connection.Ip_Address_v4?.Split('.'); }
    //    set
    //    {
    //        _connection.Ip_Address_v4 = string.Join(".", value);
    //    }
    //}
    
    public class ChangeConnectionRepository : INotifyPropertyChanged
    {
        private LocalConnection _connection;
        public ChangeConnectionRepository(Connection connection)
        {
            _connection = new LocalConnection();
            _connection.DHCP_Enabled = Convert.ToBoolean(connection.DHCP_Enabled);
            _connection.Ip_Address_v4 = connection.Ip_Address_v4?.Split('.');
            _connection.IPSubnetMask = connection.IPSubnetMask?.Split('.');
            _connection.DHCPServer = connection.DHCPServer?.Split('.');
            _connection.DNSDomain = connection.DNSDomain;
            if (connection.DNS_list?.Count > 0)
            {
                _connection.DNS_list = new string[connection.DNS_list.Count][];
                _connection.DNS_list[0] = connection.DNS_list[0].DNSServer?.Split('.');
                if (connection.DNS_list.Count > 1)
                    _connection.DNS_list[1] = connection.DNS_list[1].DNSServer?.Split('.');
            }
            if (connection.Gateway_list?.Count > 0)
            {
                _connection.Gateway_list = new string[connection.Gateway_list.Count][];
                _connection.Gateway_list[0] = connection.Gateway_list[0].IPGateway?.Split('.');
                if (connection.Gateway_list.Count > 1)
                    _connection.Gateway_list[1] = connection.Gateway_list[1].IPGateway?.Split('.');
            }

        }

        public LocalConnection connection
        {
            get { return _connection; }
            set
            {
                if (value != _connection)
                {
                    _connection = value;
                    OnPropertyChanged("connection");
                }
            }
        }

        //public bool DHCP_Enabled
        //{
        //    get { return Convert.ToBoolean(_connection.DHCP_Enabled); }
        //    set
        //    {
        //        _connection.DHCP_Enabled = value.ToString();
        //        OnPropertyChanged("DHCP_Enabled");
        //    }
        //}

        //public string[] Ip_Address_v4
        //{
        //    get { return _connection.Ip_Address_v4?.Split('.'); }
        //    set
        //    {
        //        _connection.Ip_Address_v4 = string.Join(".", value);
        //        OnPropertyChanged("Ip_Address_v4");
        //    }
        //}

        //public string[] IPSubnetMask
        //{
        //    get { return _connection.IPSubnetMask?.Split('.'); }
        //    set
        //    {
        //        _connection.IPSubnetMask = string.Join(".", value);
        //        OnPropertyChanged("IPSubnetMask");
        //    }
        //}

        //public string[] DHCPServer
        //{
        //    get { return _connection.DHCPServer?.Split('.'); }
        //    set
        //    {
        //        _connection.DHCPServer = string.Join(".", value);
        //        OnPropertyChanged("DHCPServer");
        //    }
        //}

        //public string DNSDomain
        //{
        //    get { return _connection.DNSDomain; }
        //    set
        //    {
        //        _connection.DNSDomain = value;
        //        OnPropertyChanged("DNSDomain");
        //    }
        //}

        //public string[] firstGateway
        //{
        //    get { return (_connection.Gateway_list?.Count > 0) ? _connection.Gateway_list[0].IPGateway?.Split('.'):null; }
        //    set
        //    {
        //        _connection.Gateway_list[0].IPGateway = string.Join(".", value);
        //        OnPropertyChanged("firstGateway");
        //    }
        //}

        //public string[] secondGateway
        //{
        //    get { return (_connection.Gateway_list?.Count > 1) ? _connection.Gateway_list[1].IPGateway?.Split('.'):null; }
        //    set
        //    {
        //        _connection.Gateway_list[1].IPGateway = string.Join(".", value);
        //        OnPropertyChanged("secondGateway");
        //    }
        //}

        //public string[] firstDNSServer
        //{
        //    get { return (_connection.DNS_list?.Count > 0) ? _connection.DNS_list[0].DNSServer?.Split('.'):null; }
        //    set
        //    {
        //        _connection.DNS_list[0].DNSServer = string.Join(".", value);
        //        OnPropertyChanged("firstDNSServer");
        //    }
        //}

        //public string[] secondDNSServer
        //{
        //    get { return (_connection.DNS_list?.Count > 1) ? _connection.DNS_list[1].DNSServer?.Split('.'):null; }
        //    set
        //    {
        //        _connection.DNS_list[1].DNSServer = string.Join(".", value);
        //        OnPropertyChanged("secondDNSServer");
        //    }
        //}
        //Gateway_list[0].IPGateway
        //Gateway_list[1].IPGateway

        //DNS_list[0].DNSServer
        //DNS_list[1].DNSServer

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }    
}
