﻿using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class ConnectionManager:IConnectionManager
    {
        private readonly IConnectionRepo _repository;

        public ConnectionManager(SQLiteConnection conn)
        {
            _repository = new ConnectionRepo(conn);
        }

        public Connection GetConnection(int id)
        {
            return _repository.GetItem(id);
        }

        public IList<Connection> GetConnections()
        {
            return new List<Connection>(_repository.GetItems());
        }

        public IList<Connection> GetConnections(int Offset, int Pagesize)
        {
            return new List<Connection>(_repository.GetItemsPage(Offset, Pagesize));
        }
        public IList<Connection> GetConnectionsByName(string Name, int Offset, int Pagesize)
        {
            return new List<Connection>(_repository.GetItemsPageByName(Name, Offset, Pagesize));
        }

        public int GetConnectionsAmount()
        {
            return _repository.GetItemsAmount();
        }
        public int GetConnectionsAmountByName(string Name)
        {
            return _repository.GetItemsAmountByName(Name);
        }

        public int SaveConnection(Connection conn)
        {
            return _repository.SaveItem(conn);
        }

        public int DeleteConnection(int id)
        {
            return _repository.DeleteItem(id);
        }

        public int GetLastInsertRowId()
        {
            return _repository.GetLastInsertRowId();
        }
        public int GetDiffInDays()
        {
            return _repository.GetDiffInDays();
        }
    }    
}
