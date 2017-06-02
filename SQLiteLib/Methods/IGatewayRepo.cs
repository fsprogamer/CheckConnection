using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    interface IGatewayRepo : IGenericRepo<Gateway>
    {
        IEnumerable<Gateway> GetGatewaysByConnectionId(int ConnectionId);
    }
}
