using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class DNSManager : IDNSManager
    {
        private readonly IDNSRepo _repository;

        public DNSManager(SQLiteConnection conn)
        {
            _repository = new DNSRepo(conn);
        }
        public IList<DNS> GetDNSsByConnectionId(int ConnectionId)
        {
            return new List<DNS>(_repository.GetDNSsByConnectionId(ConnectionId));
        }
        public int SaveDNSs(IEnumerable<DNS> dnss)
        {
            return _repository.SaveItems(dnss);
        }
    }
    
}
