using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Management;
using PingForm.Methods;
using log4net;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    class PingResultManager
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private ManagementObjectSearcher moSearch;
        //string _PingAddress;
        //public PingResultManager(string PingAddress)
        //{
        //    _PingAddress = PingAddress;
        //    string query = "SELECT * FROM Win32_PingStatus WHERE Address = '{0}'";
        //    query = String.Format(query, _PingAddress);

        //    moSearch = new ManagementObjectSearcher(query);
        //}

        public PingResult GetPingResult(string strHostName)
        {
            PingResult png = new PingResult(strHostName);

            if (String.IsNullOrEmpty(strHostName))
            {
                strHostName = "localhost";
            }
            try
            {
                PingMethods pm = new PingMethods();
                PingReply reply = pm.GetPing(strHostName);

                png.StatusCode = reply.Status.ToString();
                log.InfoFormat("Status : {0}", png.StatusCode);

                if (reply.Status == IPStatus.Success) { 
                    png.Ip_Address = reply.Address.ToString();
                    log.InfoFormat("Address : {0}", png.Ip_Address);

                    png.ResponseTime = reply.RoundtripTime.ToString();
                    log.InfoFormat("ResponseTime : {0}", png.ResponseTime);
                }
                else
                {
                    png.ResponseTime = "*";
                    log.InfoFormat("ResponseTime : {0}", png.ResponseTime);
                }
            }
            catch (SocketException ex)
            {
                png.ErrMessage = ex.Message;
                log.InfoFormat("ErrMessage : {0}", png.ErrMessage);
            }
            catch (Exception ex)
            {
                png.ErrMessage = ex.Message;
                log.InfoFormat("ErrMessage : {0}", png.ErrMessage);
            }
            return png;
        }

        //public List<PingResult> GetItems()
        //{
        //    List<PingResult> Ping_list = new List<PingResult>();

        //    ManagementObjectCollection moCollection = moSearch.Get();

        //    //enumerate the collection.
        //    foreach (ManagementObject mo in moCollection)
        //    {
        //        PingResult item = new PingResult();

        //        // access properties of the WMI object
        //        log.ErrorFormat("StatusCode : {0}", mo["StatusCode"]);

        //        log.ErrorFormat("strStatusCode : {0}", GetStatusCode(Convert.ToInt32(mo["StatusCode"])));

        //        log.ErrorFormat("ResponseTime : {0} ms", mo["ResponseTime"]);
        //        log.ErrorFormat("TimeToLive : {0} ms", mo["TimeToLive"]);
        //        log.ErrorFormat("Timeout : {0} ms", mo["Timeout"]);

        //        item.StatusCode = mo["StatusCode"].ToString();
        //        item.Connection_Id = 1;
        //        item.Date = DateTime.Now;
        //        item.Ip_Address = _PingAddress;
        //        item.Name = _PingAddress;
        //        item.ResponseTime = mo["ResponseTime"].ToString();

        //        Ping_list.Add(item);
        //    }
        //    return Ping_list;
        //}

        private string GetStatusCode(int intCode)
        {

            string strStatus;

            switch (intCode)
            {
                case 0:
                    strStatus = "Success";
                    break;
                case 11001:
                    strStatus = "Buffer Too Small";
                    break;
                case 11002:
                    strStatus = "Destination Net Unreachable";
                    break;
                case 11003:
                    strStatus = "Destination Host Unreachable";
                    break;
                case 11004:
                    strStatus = "Destination Protocol Unreachable";
                    break;
                case 11005:
                    strStatus = "Destination Port Unreachable";
                    break;
                case 11006:
                    strStatus = "No Resources";
                    break;
                case 11007:
                    strStatus = "Bad Option";
                    break;
                case 11008:
                    strStatus = "Hardware Error";
                    break;
                case 11009:
                    strStatus = "Packet Too Big";
                    break;
                case 11010:
                    strStatus = "Request Timed Out";
                    break;
                case 11011:
                    strStatus = "Bad Request";
                    break;
                case 11012:
                    strStatus = "Bad Route";
                    break;
                case 11013:
                    strStatus = "TimeToLive Expired Transit";
                    break;
                case 11014:
                    strStatus = "TimeToLive Expired Reassembly";
                    break;
                case 11015:
                    strStatus = "Parameter Problem";
                    break;
                case 11016:
                    strStatus = "Source Quench";
                    break;
                case 11017:
                    strStatus = "Option Too Big";
                    break;
                case 11018:
                    strStatus = "Bad Destination";
                    break;
                case 11032:
                    strStatus = "Negotiating IPSEC";
                    break;
                case 11050:
                    strStatus = "General Failure";
                    break;
                default:
                    strStatus = intCode + " - Unknown";
                    break;
            }

            return strStatus;

        }
    }
}
