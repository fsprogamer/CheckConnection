using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class GatewayManager
    {
        private readonly GatewayRepo _repository;

        public GatewayManager(SQLiteConnection conn)
        {
            _repository = new GatewayRepo(conn, "");
        }

        public Gateway GetGateway(int id)
        {
            return _repository.GetGateway(id);
        }

        public IList<Gateway> GetGateway()
        {
            return new List<Gateway>(_repository.GetGateways());
        }

        public IList<Gateway> GetGatewaysByConnectionId(int ConnectionId)
        {
            return new List<Gateway>(_repository.GetGatewaysByConnectionId(ConnectionId));
        }

        public int SaveGateway(Gateway gateway)
        {
            return _repository.SaveGateway(gateway);
        }

        public int SaveGateways(IEnumerable<Gateway> gateways)
        {
            return _repository.SaveGateways(gateways);
        }

        public int DeleteGateway(int id)
        {
            return _repository.DeleteGateway(id);
        }
    }
}
