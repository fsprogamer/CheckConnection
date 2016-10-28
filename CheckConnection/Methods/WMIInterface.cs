using System.Collections.Generic;
using CheckConnection.Model;
using System.Management;

namespace CheckConnection.Methods
{
    public interface WMIInterface
    {
        ManagementObject GetManagementObject(string connname);
        int GetNetworkDevicesConfig();

        List<ConnectionParam> GetNetworkDevices();

        List<Connection> GetNetworkDevicesList();

        //Connection GetNetworkDeviceByName(string conndesc);

        List<DNS> GetDNSArray(/*string conndesc, */int Connection_Id = 0);

        List<Gateway> GetGatewayArray(/*string conndesc, */int Connection_Id = 0);

        List<PingResult> GetPingResult(string PingAddress);
        int SaveConnectionParam(ConnectionParam param);
        //int setStaticIP(string ip_address, string subnet_mask);
        //int setDinamicIP();
        //int setDNS(string NIC, string DNS);
        //int setGateway(string gateway);

    }
}
