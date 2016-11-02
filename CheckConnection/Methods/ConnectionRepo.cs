using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class ConnectionRepo
    {
        readonly DBMethods _db = null;
        protected static string DbLocation;

        public ConnectionRepo(SQLiteConnection conn, string dbLocation)
        {
            _db = new DBMethods(conn, dbLocation);
        }

        public Connection GetConnection(int id)
        {
            return _db.GetItem<Connection>(id);
        }

        public Connection GetConnectionByName(string Name)
        {
            return _db.GetItemByName<Connection>(Name);
        }

        public IEnumerable<Connection> GetConnections()
        {
            return _db.GetItems<Connection>();
        }

        public IEnumerable<Connection> GetConnectionsByName(string Name)
        {
            return _db.GetItemsByName<Connection>(Name);
        }

        public int SaveConnection(Connection connection)
        {
            return _db.SaveItem<Connection>(connection);
        }

        public int DeleteConnection(int id)
        {
            return _db.DeleteItem<Connection>(id);
        }

        public IEnumerable<Connection> GetConnectionsByName(string Name, int Offset = 0, int Pagesize = 0)
        {
            return _db.GetConnectionPageByName(Name, Offset, Pagesize);
        }

        public int GetConnectionsAmountByName(string Name)
        {
            return _db.GetConnectionAmountByName(Name);
        }

        public int GetLastInsertRowId()
        {
            return _db.GetLastInsertRowId();
        }
    }
}
