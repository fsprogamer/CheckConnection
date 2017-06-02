using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class GatewayManager : IGatewayManager
    {
        private readonly IGatewayRepo _repository;

        public GatewayManager(SQLiteConnection conn)
        {
            _repository = new GatewayRepo(conn);
        }
        public IList<Gateway> GetGatewaysByConnectionId(int ConnectionId)
        {
            return new List<Gateway>(_repository.GetGatewaysByConnectionId(ConnectionId));
        }
        public int SaveGateways(IEnumerable<Gateway> gateways)
        {
            return _repository.SaveItems(gateways);
        }
    }
   
}
