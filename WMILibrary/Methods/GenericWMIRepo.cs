using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using Common;

namespace CheckConnection.Methods
{
    public abstract class GenericWMIRepo<T> : ClassWithLogger<T>, IGenericWMIRepo<T> where T : class, new()
                                                                                                                                                          
    {
        protected string _query = String.Empty;
        protected string _scope = String.Empty;

        public IList<T> Context
        {
            get; 
            set; 
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
        public IEnumerable<T> GetItems(Func<T, bool> predicate)
        {
            return Context.Where<T>(predicate)/*.ToList()*/;
        }
        public IEnumerable<T> GetItems()
        {
            return Context;
        }
        //abstract public int SaveItem(T item);
        //abstract public int SaveItems(IEnumerable<T> items);
    }
}
