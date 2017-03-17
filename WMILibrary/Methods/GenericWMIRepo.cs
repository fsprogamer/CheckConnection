using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using Common;

namespace CheckConnection.Methods
{
    public abstract class GenericWMIRepo<T> : ClassWithLog, IGenericWMIRepo<T> where T : class, new()
                                                                                                                                                          
    {
        private List<T> _entities;
        protected string _query = String.Empty;
        protected string _scope = String.Empty;

        public List<T> Context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        public GenericWMIRepo(string scope, string query)
        {
            _query = query;
            _scope = scope;
           // Query(_query);
        }
        public T GetItem(Func<T, bool> predicate)
        {
            return Context.Where<T>(predicate).First();
        }
        public List<T> GetItems(Func<T, bool> predicate)
        {
            return Context.Where<T>(predicate).ToList();
        }
        public List<T> GetItems()
        {
            return Context;
        }
        //abstract public int SaveItem(T item);
        //abstract public int SaveItems(IEnumerable<T> items);
    }
}
