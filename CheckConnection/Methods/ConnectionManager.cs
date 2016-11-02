using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class ConnectionManager
    {
        private readonly ConnectionRepo _repository;

        public ConnectionManager(SQLiteConnection conn)
        {
            _repository = new ConnectionRepo(conn, "");
        }

        public Connection GetConnection(int id)
        {
            return _repository.GetConnection(id);
        }

        public IList<Connection> GetConnections()
        {
            return new List<Connection>(_repository.GetConnections());
        }

        public IList<Connection> GetConnectionsByName(string Name, int Offset, int Pagesize)
        {
            return new List<Connection>(_repository.GetConnectionsByName(Name, Offset, Pagesize));
        }

        public int GetConnectionsAmountByName(string Name)
        {
            return _repository.GetConnectionsAmountByName(Name);
        }

        public int SaveConnection(Connection conn)
        {
            return _repository.SaveConnection(conn);
        }

        public int DeleteConnection(int id)
        {
            return _repository.DeleteConnection(id);
        }

        public int GetLastInsertRowId()
        {
            return _repository.GetLastInsertRowId();
        }

    }
}
