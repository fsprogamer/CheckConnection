using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    interface IDNSRepo:IGenericRepo<DNS>
    {
        IEnumerable<DNS> GetDNSsByConnectionId(int ConnectionId);
    }
}
