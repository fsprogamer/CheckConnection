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
    /*
    public class DNSManager
    {
        private readonly DNSRepo _repository;

        public DNSManager(SQLiteConnection conn)
        {
            _repository = new DNSRepo(conn, "");
        }

        public DNS GetDNS(int id)
        {
            return _repository.GetDNS(id);
        }

        public IList<DNS> GetDNS()
        {
            return new List<DNS>(_repository.GetDNSs());
        }

        public IList<DNS> GetDNSsByConnectionId(int ConnectionId)
        {
            return new List<DNS>(_repository.GetDNSsByConnectionId(ConnectionId));
        }

        public int SaveDNS(DNS dns)
        {
            return _repository.SaveDNS(dns);
        }

        public int SaveDNSs(IEnumerable<DNS> dnss)
        {
            return _repository.SaveDNSs(dnss);
        }

        public int DeleteDNS(int id)
        {
            return _repository.DeleteDNS(id);
        }
    }
    */
}
