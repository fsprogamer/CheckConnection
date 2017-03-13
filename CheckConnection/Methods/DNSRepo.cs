using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    class DNSRepo : GenericRepo<SQLiteConnection, DNS>, IDNSRepo
    {
        //static readonly object Locker = new object();

        public DNSRepo(SQLiteConnection conn) : base(conn)
        {
            log.Info("DNSRepo costructor");
        }

        public IEnumerable<DNS> GetDNSsByConnectionId(int ConnectionId)
        {
            lock (Locker)
            {
                try
                {
                    return Context.Query<DNS>("SELECT * FROM DNS where connection_id = ? order by Order_Id asc", ConnectionId).ToList();
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }

    }
    
}
