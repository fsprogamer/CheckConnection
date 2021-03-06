﻿using System;
using System.Management;

using Common;
using System.Threading.Tasks;

namespace CheckConnection.Methods
{
    public class MObjectManager: ClassWithLogger<MObjectManager>, IMObjectManager
    {
        private ManagementObject _objMO;        
        public MObjectManager(ManagementObject pobjMO)
        {
            _objMO = pobjMO;
        }
        public bool IpEnabled { get  { return (bool)_objMO["IPEnabled"]; }                         
                              }

        //public bool Enabled(string conn)
        //{
        //    bool ret = false;
        //    foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        //    {
        //        if ((ni.Description == conn) && (ni.OperationalStatus.ToString() == "Up"))
        //            ret = true;
        //    }
        //    return ret;
        //}

        public int setStaticIP(string ip_address, string subnet_mask)
        {
            int ret = 0;
            try
            {
                // Set IPAddress and Subnet Mask
                ManagementBaseObject newIP = _objMO.GetMethodParameters("EnableStatic");
                newIP["IPAddress"] = new string[] { ip_address };
                newIP["SubnetMask"] = new string[] { subnet_mask };

                _objMO.InvokeMethod("EnableStatic", newIP, null);
                ret = 0;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
                throw;
            }
            return ret;
        }

        public int setDinamicIP()
        {
            int ret = 0;
            try
            {
                _objMO.InvokeMethod("EnableDHCP", null);
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
                ret = 0;
            }
            return ret;
        }

        /// <summary> 
        /// Set's a new Gateway address of the local machine 
        /// </summary> 
        /// <param name="gateway">The Gateway IP Address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        public int setGateway(string[] gateway)
        {
            int ret = 0;
            try
            {
                ManagementBaseObject setGateway;
                ManagementBaseObject newGateway =
                    _objMO.GetMethodParameters("SetGateways");

                newGateway["DefaultIPGateway"] = gateway;
                newGateway["GatewayCostMetric"] = new int[] { 1, 2 };

                setGateway = _objMO.InvokeMethod("SetGateways", newGateway, null);
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
                ret = 0;
            }
            return ret;
        }

        public int setDNSDomain(string name)
        {
            int ret = 0;
            try
            {
                ManagementBaseObject setdnsDomain;
                ManagementBaseObject DNSDomain = _objMO.GetMethodParameters("SetDNSDomain");

                DNSDomain["DNSDomain"] = name;
                setdnsDomain = _objMO.InvokeMethod("SetDNSDomain", DNSDomain, null);
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении DNSDomain", ex);
                ret = 0;
            }
            return ret;
        }

        public int setDNSServerSearchOrder(string[] name)
        {
            int ret = 0;
            try
            {
                ManagementBaseObject newDNS = _objMO.GetMethodParameters("SetDNSServerSearchOrder");                
                if ((name == null) || (name.Length == 0))
                    newDNS["DNSServerSearchOrder"] = null;
                else
                    newDNS["DNSServerSearchOrder"] = name;
                ManagementBaseObject setDNS =
                    _objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);

                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении SetDNSServerSearchOrder", ex);
                ret = 0;
            }
            return ret;
        }

        public int RenewDHCPLease()
        {
            int ret;
            try
            {
                _objMO.InvokeMethod("ReleaseDHCPLease", null);
                _objMO.InvokeMethod("RenewDHCPLease", null);
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
                ret = 0;
            }
            return ret;
        }

        public int StartService()
        {
            int ret = 0;
            try
            {
                _objMO.InvokeMethod("StartService", null);              
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при старте сервиса", ex);
                ret = 0;
            }
            return ret;
        }

        public int StopService()
        {
            int ret = 0;
            try
            {
                _objMO.InvokeMethod("StopService", null);
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при остановке сервиса", ex);
                ret = 0;
            }
            return ret;
        }

        public int DisableAdapter()
        {
            int ret = 0;
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            try
            {
                log.Info("before DisableAdapter");
                //Disable the Network Adapter 

                if ((osInfo.Version.Major <= 5) && (osInfo.Version.Minor <= 1))
                {
                    CNICManager cnic = new CNICManager();
                    cnic.DisableConnection(_objMO.Properties["NetConnectionId"].ToString());
                }
                else
                    _objMO.InvokeMethod("Disable", null);
                log.Info("after DisableAdapter");
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении отключении адаптера", ex);
                ret = 0;
            }
            return ret;
        }

        public int EnableAdapter()
        {
            int ret = 0;
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            try
            {
                log.Info("before EnableAdapter");
                //Enable the Network Adapter 
                if ((osInfo.Version.Major <= 5) && (osInfo.Version.Minor <= 1))
                {
                    CNICManager cnic = new CNICManager();
                    cnic.EnableConnection(_objMO.Properties["NetConnectionId"].Value.ToString());
                }
                else
                    _objMO.InvokeMethod("Enable", null);
                log.Info("after EnableAdapter");
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении включении адаптера", ex);
                ret = 0;
            }
            return ret;
        }
    }

    /// <summary> 
    /// Set's the DNS Server of the local machine 
    /// </summary> 
    /// <param name="NIC">NIC address</param> 
    /// <param name="DNS">DNS server address</param> 
    /// <remarks>Requires a reference to the System.Management namespace</remarks> 
    //public int setDNS(string NIC, string DNS)
    //{
    //    int ret = 0;

    //    if (_objMO["Caption"].Equals(NIC))
    //    {
    //        try
    //        {
    //            ManagementBaseObject newDNS =
    //                _objMO.GetMethodParameters("SetDNSServerSearchOrder");
    //            newDNS["DNSServerSearchOrder"] = DNS.Split(',');
    //            ManagementBaseObject setDNS =
    //                _objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
    //            ret = 1;
    //        }
    //        catch (Exception ex)
    //        {
    //            log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
    //            ret = 0;
    //        }
    //    }
    //    return ret;
    //}

    /// <summary> 
    /// Set's WINS of the local machine 
    /// </summary> 
    /// <param name="NIC">NIC Address</param> 
    /// <param name="priWINS">Primary WINS server address</param> 
    /// <param name="secWINS">Secondary WINS server address</param> 
    /// <remarks>Requires a reference to the System.Management namespace</remarks> 
    //public int setWINS(string NIC, string priWINS, string secWINS)
    //{
    //    int ret = 0;

    //    if (_objMO["Caption"].Equals(NIC))
    //    {
    //        try
    //        {
    //            ManagementBaseObject setWINS;
    //            ManagementBaseObject wins =
    //            _objMO.GetMethodParameters("SetWINSServer");
    //            wins.SetPropertyValue("WINSPrimaryServer", priWINS);
    //            wins.SetPropertyValue("WINSSecondaryServer", secWINS);

    //            setWINS = _objMO.InvokeMethod("SetWINSServer", wins, null);
    //            ret = 1;
    //        }
    //        catch (Exception ex)
    //        {
    //            log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
    //            ret = 0;
    //        }
    //    }
    //    return ret;
    //}

}
