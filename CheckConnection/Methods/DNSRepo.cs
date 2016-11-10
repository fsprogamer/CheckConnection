using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    class DNSRepo
    {
        readonly DBMethods _db = null;
        //protected static string DbLocation;

        public DNSRepo(SQLiteConnection conn, string dbLocation)
        {
            _db = new DBMethods(conn, dbLocation);
        }

        public DNS GetDNS(int id)
        {
            return _db.GetItem<DNS>(id);
        }

        public IEnumerable<DNS> GetDNSs()
        {
            return _db.GetItems<DNS>();
        }

        public IEnumerable<DNS> GetDNSsByConnectionId(int ConnectionId)
        {
            return _db.GetDNSByConnectionId(ConnectionId);
        }

        public int SaveDNS(DNS DNS)
        {
            return _db.SaveItem<DNS>(DNS);
        }

        public int SaveDNSs(IEnumerable<DNS> DNSs)
        {
            return _db.SaveItems<DNS>(DNSs);
        }

        public int DeleteDNS(int id)
        {
            return _db.DeleteItem<DNS>(id);
        }
    }
}
