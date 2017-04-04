using System;
using System.Linq;
using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    class GatewayRepo : GenericRepo<SQLiteConnection, Gateway>, IGatewayRepo
    {
        //static readonly object Locker = new object();

        public GatewayRepo(SQLiteConnection conn) : base(conn)
        {
            log.Info("GatewayRepo costructor");
        }

        public IEnumerable<Gateway> GetGatewaysByConnectionId(int ConnectionId)
        {
            lock (Locker)
            {
                try
                {
                    return Context.Query<Gateway>("SELECT * FROM Gateway where connection_id = ? order by Id asc", ConnectionId)/*.ToList()*/;
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
