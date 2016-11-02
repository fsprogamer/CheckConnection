using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    class GatewayRepo
    {
        readonly DBMethods _db = null;
        protected static string DbLocation;

        public GatewayRepo(SQLiteConnection conn, string dbLocation)
        {
            _db = new DBMethods(conn, dbLocation);
        }

        public Gateway GetGateway(int id)
        {
            return _db.GetItem<Gateway>(id);
        }

        public IEnumerable<Gateway> GetGateways()
        {
            return _db.GetItems<Gateway>();
        }

        public IEnumerable<Gateway> GetGatewaysByConnectionId(int ConnectionId)
        {
            return _db.GetGatewayByConnectionId(ConnectionId);
        }

        public int SaveGateway(Gateway gateway)
        {
            return _db.SaveItem<Gateway>(gateway);
        }

        public int SaveGateways(IEnumerable<Gateway> gatewayss)
        {
            return _db.SaveItems<Gateway>(gatewayss);
        }

        public int DeleteGateway(int id)
        {
            return _db.DeleteItem<Gateway>(id);
        }
    }
}
