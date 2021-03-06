﻿using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using PingLib.Model;
using Common;

namespace PingLib.Methods
{
    
    public class PingResultManager: ClassWithLogger<PingResultManager>
    {
        IPingResultRepo pm;
        Model.PingResult png;

        public event EventHandler<PingResultEventArgs> PingResultCompleted;
        public PingResultManager()
        {
            pm = new PingResultRepo();
            pm.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);            
        }

        public PingResult GetPingResult(string strHostName)
        {
            if (String.IsNullOrEmpty(strHostName))
            {
                strHostName = "localhost";
            }
            png = new Model.PingResult(strHostName);
            try
            {                
                PingReply reply = pm.GetPing(strHostName);

                png.StatusCode = /*GetStatusCode((int)reply.Status); */(reply.Status.ToString() == "Success" ? "Успешно" : reply.Status.ToString());
                log.InfoFormat("Status: {0}, {1}", (int)reply.Status, png.StatusCode );

                if (reply.Status == IPStatus.Success) { 
                    png.Ip_Address = reply.Address.ToString();
                    log.InfoFormat("Address: {0}", png.Ip_Address);

                    png.ResponseTime = reply.RoundtripTime.ToString();
                    log.InfoFormat("ResponseTime: {0}", png.ResponseTime);
                }
                else
                {
                    png.ResponseTime = "*";
                    log.InfoFormat("ResponseTime: {0}", png.ResponseTime);
                }
            }
            catch (SocketException ex)
            {
                png.ResponseTime = "*";
                png.ErrMessage = ex.Message;
                png.StatusCode = ex.Message;
                png.SocketErrorCode = ex.ErrorCode;
                log.InfoFormat("SocketException ErrMessage: {0}", png.ErrMessage);
            }
            catch (Exception ex)
            {
                png.ResponseTime = "*";
                png.ErrMessage = (ex.InnerException!=null)? ex.InnerException.Message : ex.Message;
                png.StatusCode = (ex.InnerException!=null)? ex.InnerException.Message : ex.Message;                            
                log.InfoFormat("Exception ErrMessage: {0}", png.ErrMessage);
            }
            return png;
        }

        public void GetPingResultAsync(string strHostName)
        {
            if (String.IsNullOrEmpty(strHostName))
            {
                strHostName = "localhost";
            }
            png = new Model.PingResult(strHostName);

            try
            {
                pm.GetPingAsync(strHostName);
            }
            catch (SocketException ex)
            {
                png.ResponseTime = "*";
                png.ErrMessage = ex.Message;
                png.StatusCode = ex.Message;
                png.SocketErrorCode = ex.ErrorCode;
                log.InfoFormat("SocketException ErrMessage: {0}", png.ErrMessage);
            }
            catch (Exception ex)
            {
                png.ResponseTime = "*";
                png.ErrMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                png.StatusCode = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                log.InfoFormat("Exception ErrMessage: {0}", png.ErrMessage);
            }            
        }

        void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {            
            png.StatusCode = (e.Reply.Status.ToString() == "Success" ? "Успешно" : e.Reply.Status.ToString());
            log.InfoFormat("Status: {0}, {1}", (int)e.Reply.Status, png.StatusCode);

            if (e.Reply.Status == IPStatus.Success)
            {
                png.Ip_Address = e.Reply.Address.ToString();
                log.InfoFormat("Address: {0}", png.Ip_Address);

                png.ResponseTime = e.Reply.RoundtripTime.ToString();
                log.InfoFormat("ResponseTime: {0}", png.ResponseTime);
            }
            else
            {
                png.ResponseTime = "*";
                log.InfoFormat("ResponseTime: {0}", png.ResponseTime);
            }

            PingResultCompleted(this, new PingResultEventArgs(png));
        }

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
