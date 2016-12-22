namespace CheckConnection.Methods
{
    public interface IMObjectManager
    {
        bool IpEnabled
        {
            get;
        }
        int setStaticIP(string ip_address, string subnet_mask);
        int setDinamicIP();
        /// <summary> 
        /// Set's a new Gateway address of the local machine 
        /// </summary> 
        /// <param name="gateway">The Gateway IP Address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        int setGateway(string[] gateway);
        /// <summary> 
        /// Set's the DNS Server of the local machine 
        /// </summary> 
        /// <param name="NIC">NIC address</param> 
        /// <param name="DNS">DNS server address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        int setDNS(string NIC, string DNS);
        /// <summary> 
        /// Set's WINS of the local machine 
        /// </summary> 
        /// <param name="NIC">NIC Address</param> 
        /// <param name="priWINS">Primary WINS server address</param> 
        /// <param name="secWINS">Secondary WINS server address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        int setWINS(string NIC, string priWINS, string secWINS);
        int setDNSDomain(string name);
        int setDNSServerSearchOrder(string[] name);
        int RenewDHCPLease();
    }
}
