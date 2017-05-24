using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IGatewayManager
    {
        IList<Gateway> GetGatewaysByConnectionId(int ConnectionId);
        int SaveGateways(IEnumerable<Gateway> gateways);
    }
}
