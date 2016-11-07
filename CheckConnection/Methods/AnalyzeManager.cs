using System.Collections.Generic;
using CheckConnection.Model;

using log4net;

namespace CheckConnection.Methods
{
    class AnalyzeManager
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private string _RouterDeafultIpAddress = Properties.Settings.Default.RouterDeafultIpAddress;
        private string _ProviderDefaultAddress = Properties.Settings.Default.ProviderDefaultAddress;
        private string _IpAddress;
        private string _DHCPEnabled;
        private List<Gateway> _IPGateway;
        private List<DNS> _DNS;
        private PingResultManager _pingmgr;
        private List<PingResult> _pngresult;

        public AnalyzeManager(string IpAddress, string DHCPEnabled)
        {
            _pingmgr = new PingResultManager();
            _IpAddress = IpAddress;
            _DHCPEnabled = DHCPEnabled;
        }

        public void SetGateway(List<Gateway> IPGateway)
        {
            _IPGateway = IPGateway;
        }
        public void SetDNS(List<DNS> DNS)
        {
            _DNS = DNS;
        }

        public void StartAnalyze()
        {
            _pngresult = new List<PingResult>(6);

            if (_IpAddress != null)
            {
                log.Info("_IpAddress != null");
                _pngresult.Add(_pingmgr.GetPingResult(_IpAddress));
            }
            if (_IPGateway != null) {
                log.Info("_IPGateway != null");
                foreach (Gateway gateway in _IPGateway)
                {
                    _pngresult.Add(_pingmgr.GetPingResult(gateway.IPGateway));
                }
            }
            if (_DNS != null)
            {
                log.Info("_DNS != null");
                foreach (DNS dns in _DNS)
                {
                    _pngresult.Add(_pingmgr.GetPingResult(dns.DNSServer));
                }
            }
            log.InfoFormat("_ProviderDefaultAddress = {0}", _ProviderDefaultAddress);
            _pngresult.Add(_pingmgr.GetPingResult(_ProviderDefaultAddress));
        }
        public List<string[]> GetPingResult()
        {
            List<string[]> strlist= new List<string[]>(3);
            foreach (PingResult png in _pngresult)
            {
                string[] row = new string[] { png.Ip_Address, png.Name, png.StatusCode, png.ResponseTime };
                strlist.Add(row);
            }
            return strlist;
        }
        public string MakeConclusion()
        {
            string Conclusion = "Интересный вы человек! Все у вас в порядке. Удивительно, с таким счастьем — и на свободе."; 
            foreach(PingResult png in _pngresult)
            {
                if(png.StatusCode != "Успешно")
                {
                    Conclusion = "Houston, we have a problem";
                    //"Обнаружены ошибки при анализе сетевого подключения";
                    if ((_IpAddress == "0.0.0.0")&&(_DHCPEnabled == "True"))
                        Conclusion += "\n\rПроверьте питание и работоспособность модема";
                    //if ((_IPGateway == null)&&(_DHCPEnabled == "True"))
                    //    Conclusion += "\n\rПроверьте питание и работоспособность модема";
                }
            }
            return Conclusion;
        }
    }
}
