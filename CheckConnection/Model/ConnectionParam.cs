using System.Collections.Generic;

namespace CheckConnection.Model
{
    public class ConnectionParam
    {
        public Connection Connection { get; set; }
        public List<DNS> DNS_list { get; set; }
        public List<Gateway> Gateway_list { get; set; }
        public ConnectionParam()
        {
        }
    }
}
