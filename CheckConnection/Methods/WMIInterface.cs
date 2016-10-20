using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface WMIInterface
    {
        int GetNetworkDevicesConfig();

        List<ConnectionParam> GetNetworkDevices();

        List<Connection> GetNetworkDevicesList();

        //Connection GetNetworkDeviceByName(string conndesc);

        List<DNS> GetDNSArray(/*string conndesc, */int Connection_Id = 0);

        List<Gateway> GetGatewayArray(/*string conndesc, */int Connection_Id = 0);

        List<PingResult> GetPingResult(string PingAddress);
        int setStaticIP(string ip_address, string subnet_mask);

        int setDinamicIP();
    }
}
