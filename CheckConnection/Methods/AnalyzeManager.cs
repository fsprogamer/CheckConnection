﻿using System.Collections.Generic;
using CheckConnection.Model;
using System;

using Common;

namespace CheckConnection.Methods
{
    class AnalyzeManager:ClassWithLog
    {
        public string _ProviderDefaultAddress = Properties.Settings.Default.ProviderDefaultAddress;
        private string _IpAddress;
        private string _DHCPEnabled;
        private int _ValidatedDNS = -1;
        private int _subnet = -1;
        private List<Gateway> _IPGateway;
        private List<DNS> _DNS;
        private PingResultManager _pingmgr;
        private List<PingResult> _pngresult = new List<PingResult>(6);

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

        public void CompareWithStandartParam()
        {
            _ValidatedDNS = CheckDNS();
            _subnet = CheckIP();
        }

        public List<string[]> GetPingResults()
        {
            List<string[]> strlist= new List<string[]>(3);
            foreach (PingResult png in _pngresult)
            {
                string[] row = new string[] { png.Ip_Address, png.Name, png.StatusCode, png.ResponseTime };
                strlist.Add(row);
            }
            return strlist;
        }

        public string[] GetPingResult(string ipaddress)
        {
            string[] row = null;
            PingResult png = _pingmgr.GetPingResult(ipaddress);
            if ( png != null)
            {
                _pngresult.Add(png);
                row = new string[] { png.Ip_Address, png.Name, png.StatusCode, png.ResponseTime };                
            }
            return row;
        }

        public int CheckDNS() {
            int ValidatedDNS = 0;            
            System.Collections.Specialized.StringCollection AkadoDNS = Properties.Settings.Default.AkadoDNS;

            log.InfoFormat("before CheckDNS, AkadoDNS.Count = {0}", AkadoDNS.Count);

            string[] strArray = new string[AkadoDNS.Count];
            AkadoDNS.CopyTo(strArray, 0);
            log.Info("before foreach");

            if (_DNS != null)
            {                
                foreach (DNS dns in _DNS)
                {
                    foreach (string akadodns in AkadoDNS)
                    {
                        if (dns.DNSServer == akadodns)
                        {
                            log.InfoFormat("break in CheckDNS, {0} == {1}", dns.DNSServer, akadodns);
                            ValidatedDNS = 1;
                            break;
                        }
                    }
                }
            }
            else
                ValidatedDNS = 0;

            log.Info("after CheckDNS");
            return ValidatedDNS;
        }

        public int CheckIP()
        {
            int subnet = -1;
            log.Info("before CheckIP");
            try
            {
                System.Collections.Specialized.StringCollection TestNetwork = Properties.Settings.Default.TestNetwork;
                System.Collections.Specialized.StringCollection WorkingNetwork = Properties.Settings.Default.WorkingNetwork;

                string substr = _IpAddress.Substring(0, _IpAddress.LastIndexOf('.') );
                substr = substr.Substring(0, substr.LastIndexOf('.') );

                log.InfoFormat("substr = {0}",substr);
                if (substr.Substring(0, substr.LastIndexOf('.')) == WorkingNetwork[0].Substring(0, WorkingNetwork[0].LastIndexOf('.')))
                {
                    string strpart = substr.Substring(substr.IndexOf('.') + 1 , substr.Length - (substr.IndexOf('.') + 1));
                    int part = Convert.ToInt32(strpart);
                    int lowerbound = Convert.ToInt32(WorkingNetwork[0].Substring(WorkingNetwork[0].IndexOf('.') + 1, WorkingNetwork[0].Length - (WorkingNetwork[0].IndexOf('.') + 1)), 10);
                    int upperbound = Convert.ToInt32(WorkingNetwork[1].Substring(WorkingNetwork[1].IndexOf('.') + 1, WorkingNetwork[1].Length - (WorkingNetwork[1].IndexOf('.') + 1)), 10);

                    log.InfoFormat("part = {0}, lowerbound = {1}, upperbound = {2}", part, lowerbound, upperbound);
                    if ((part >= lowerbound) && (part <= upperbound))
                    {
                        subnet = 1;
                    }
                }

                if (substr.Substring(0, substr.LastIndexOf('.')) == TestNetwork[0].Substring(0, TestNetwork[0].LastIndexOf('.')))
                {
                    string strpart = substr.Substring(substr.IndexOf('.')+1, substr.Length - (substr.IndexOf('.') + 1));
                    int part = Convert.ToInt32(strpart);
                    int lowerbound = Convert.ToInt32(TestNetwork[0].Substring(TestNetwork[0].IndexOf('.') + 1, TestNetwork[0].Length - (TestNetwork[0].IndexOf('.') + 1)), 10);
                    int upperbound = Convert.ToInt32(TestNetwork[1].Substring(TestNetwork[1].IndexOf('.') + 1, TestNetwork[1].Length - (TestNetwork[0].IndexOf('.') + 1)), 10);

                    log.InfoFormat("part = {0}, lowerbound = {1}, upperbound = {2}", part, lowerbound, upperbound);
                    if ((part >= lowerbound) && (part <= upperbound))
                    {
                        subnet = 0;
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error("Ошибка при разборе ip-адреса", ex);
            }
            log.Info("after CheckIP");
            return subnet; 
        }

        public List<string> MakeConclusion()
        {            
            List<string> lst = new List<string>();

            foreach (PingResult png in _pngresult)
            {
                if(png.StatusCode != "Успешно")
                {
                    lst.Add("При анализе сетевого подключения обнаружены ошибки.");
                    lst.Add("Они могут влиять на работу компьютера в сети.");
                    //"Обнаружены ошибки при анализе сетевого подключения";
                    if ((_IpAddress == "0.0.0.0")&&(_DHCPEnabled == "True"))
                        lst.Add("Проверьте питание и работоспособность модема.");
                    //if ((_IPGateway == null)&&(_DHCPEnabled == "True"))
                    //    Conclusion += "\n\rПроверьте питание и работоспособность модема";
                    break;
                }
            }

            if (_subnet == -1)
            {
                lst.Add("Ip-адрес не относиться к сети АКАДО или выдан роутером.");
            }
            if (_subnet == 0)
            {
                lst.Add("Ip-адрес заблокирован в сети АКАДО.");
            }
            if (_ValidatedDNS==0)
            {
                lst.Add("Вы не используете стандартные DNS-сервера АКАДО.");
            }

            //if (_ValidatedDNS == 0)
            //{
            //    lst.Add("У вас нет доступа к нескольким серверам DNS.");
            //}

            return lst;
        }
    }
}