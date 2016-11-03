namespace CheckConnection.Methods
{
    class AnalyzeManager
    {
        private string _RouterDeafultIpAddress = Properties.Settings.Default.RouterDeafultIpAddress;
        private string _ProviderDefaultAddress = Properties.Settings.Default.ProviderDefaultAddress;
        private string _IPGateway;
        private PingResultManager pingmgr;
        public AnalyzeManager()
        {
            pingmgr = new PingResultManager();        
        }
        public void SetGateway(string IPGateway)
        {
            _IPGateway = IPGateway;
        }
        public void StartAnalyze()
        {
            pingmgr.GetPingResult(_IPGateway);
            pingmgr.GetPingResult(_ProviderDefaultAddress);
        }
    }
}
