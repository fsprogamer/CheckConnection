using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface WMIInterface
    {
        List<Connection> GetNetworkDevices();

        List<DNS> GetDNSArray(int Connection_Id);

        List<Gateway> GetGatewayArray(int Connection_Id);

        List<Ping> GetPingResult(string PingAddress);
    }
}
