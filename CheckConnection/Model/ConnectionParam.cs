using System.Collections.Generic;

using Common;

namespace CheckConnection.Model
{
    public class ConnectionParam:ClassWithLog
    {
        public Connection Connection { get; set; }
        public List<DNS> DNS_list { get; set; }
        public List<Gateway> Gateway_list { get; set; }        

        public ConnectionParam()
        {            
        }      
        public void setDNSServerSearchOrder(string strdns)
        {
            int i = 0;
            log.Info("before setDNSServerSearchOrder");
            if (!string.IsNullOrEmpty(strdns))
            {
                DNS_list = new List<DNS>(2);
                string[] dns_array = strdns.Split(';');
                foreach (string dns in dns_array)
                {
                    DNS_list.Add(new DNS { DNSServer = dns, Order_Id = i });
                    i++;
                }
            }
            log.Info("after setDNSServerSearchOrder");
        }
        public void setGateway(string strgateway)
        {
            log.Info("before setGateway");
            if (!string.IsNullOrEmpty(strgateway))
            {
                Gateway_list = new List<Gateway>(2);
                string[] gateway_array = strgateway.Split(';');
                foreach (string gateway in gateway_array)
                {
                    Gateway_list.Add(new Gateway { IPGateway = gateway });
                }
            }
            log.Info("after setGateway");
        }
    }
}
