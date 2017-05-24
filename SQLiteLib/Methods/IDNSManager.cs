using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IDNSManager
    {
        IList<DNS> GetDNSsByConnectionId(int ConnectionId);
        int SaveDNSs(IEnumerable<DNS> dnss);
    }
}
