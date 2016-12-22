using System;
using System.Management;

using Common;

namespace CheckConnection.Methods
{
    public class MObjectManager: ClassWithLog, IMObjectManager
    {
        private ManagementObject _objMO;        
        public MObjectManager(ManagementObject pobjMO)
        {
            _objMO = pobjMO;
        }
        public bool IpEnabled { get  { return (bool)_objMO["IPEnabled"]; }                         
                              }

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
                ret = 1;
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
                throw;
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
                throw;
            }
            return ret;
        }

        /// <summary> 
        /// Set's the DNS Server of the local machine 
        /// </summary> 
        /// <param name="NIC">NIC address</param> 
        /// <param name="DNS">DNS server address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        public int setDNS(string NIC, string DNS)
        {
            int ret = 0;

            if (_objMO["Caption"].Equals(NIC))
            {
                try
                {
                    ManagementBaseObject newDNS =
                        _objMO.GetMethodParameters("SetDNSServerSearchOrder");
                    newDNS["DNSServerSearchOrder"] = DNS.Split(',');
                    ManagementBaseObject setDNS =
                        _objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                    ret = 1;
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
                    throw;
                }
            }
            return ret;
        }

        /// <summary> 
        /// Set's WINS of the local machine 
        /// </summary> 
        /// <param name="NIC">NIC Address</param> 
        /// <param name="priWINS">Primary WINS server address</param> 
        /// <param name="secWINS">Secondary WINS server address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        public int setWINS(string NIC, string priWINS, string secWINS)
        {
            int ret = 0;

            if (_objMO["Caption"].Equals(NIC))
            {
                try
                {
                    ManagementBaseObject setWINS;
                    ManagementBaseObject wins =
                    _objMO.GetMethodParameters("SetWINSServer");
                    wins.SetPropertyValue("WINSPrimaryServer", priWINS);
                    wins.SetPropertyValue("WINSSecondaryServer", secWINS);

                    setWINS = _objMO.InvokeMethod("SetWINSServer", wins, null);
                    ret = 1;
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
                    throw;
                }
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
                throw;
            }
            return ret;
        }

        public int setDNSServerSearchOrder(string[] name)
        {
            int ret = 0;
            try
            {
                ManagementBaseObject newDNS = _objMO.GetMethodParameters("SetDNSServerSearchOrder");
                log.InfoFormat("name.Length == {0}", name.Length);
                if (name.Length == 0)
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
                throw;
            }
            return ret;
        }

        public int RenewDHCPLease()
        {
            int ret = 0;
            try
            {
                _objMO.InvokeMethod("ReleaseDHCPLease", null);
                _objMO.InvokeMethod("RenewDHCPLease", null);
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
                throw;
            }
            return ret;
        }

    }
}
