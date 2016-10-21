using System;
using System.Management;
using log4net;

namespace CheckConnection.Model
{
    class MObject: ManagementObject
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MObject()
        {            
        }

        public int setStaticIP(string ip_address, string subnet_mask)
        {
            int ret = 0;
            try
            {
                // Set IPAddress and Subnet Mask
                ManagementBaseObject newIP = this.GetMethodParameters("EnableStatic");
                newIP["IPAddress"] = new string[] { ip_address };
                newIP["SubnetMask"] = new string[] { subnet_mask };

                this.InvokeMethod("EnableStatic", newIP, null);
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
                this.InvokeMethod("EnableDHCP", null);                
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
        public int setGateway(string gateway)
        {
            int ret = 0;
            try
            {
                ManagementBaseObject setGateway;
                ManagementBaseObject newGateway =
                    this.GetMethodParameters("SetGateways");

                newGateway["DefaultIPGateway"] = new string[] { gateway };
                newGateway["GatewayCostMetric"] = new int[] { 1 };

                setGateway = this.InvokeMethod("SetGateways", newGateway, null);
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

            if (this["Caption"].Equals(NIC))
            {
                try
                {
                    ManagementBaseObject newDNS =
                        this.GetMethodParameters("SetDNSServerSearchOrder");
                    newDNS["DNSServerSearchOrder"] = DNS.Split(',');
                    ManagementBaseObject setDNS =
                        this.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
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

            if (this["Caption"].Equals(NIC))
            {
                try
                {
                    ManagementBaseObject setWINS;
                    ManagementBaseObject wins =
                    this.GetMethodParameters("SetWINSServer");
                    wins.SetPropertyValue("WINSPrimaryServer", priWINS);
                    wins.SetPropertyValue("WINSSecondaryServer", secWINS);

                    setWINS = this.InvokeMethod("SetWINSServer", wins, null);
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
                ManagementBaseObject DNSDomain = this.GetMethodParameters("DNSDomain");

                DNSDomain["DNSDomain"] = name;
                setdnsDomain = this.InvokeMethod("SetDNSDomain", DNSDomain, null);
                ret = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Ошибка при изменении DNSDomain", ex);
                throw;
            }
            return ret;
        }

    }
}
