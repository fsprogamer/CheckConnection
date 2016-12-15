using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    interface IDNSManager
    {
        IList<DNS> GetDNSsByConnectionId(int ConnectionId);
        int SaveDNSs(IEnumerable<DNS> dnss);
    }
}
