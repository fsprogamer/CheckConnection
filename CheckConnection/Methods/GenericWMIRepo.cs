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

        public List<T> Context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        public GenericWMIRepo(string query)
        {
            _query = query;
           // Query(_query);
        }
        //public int Query(string query)
        //{
        //    int ret = 0;
        //    ManagementObjectSearcher moSearch = new ManagementObjectSearcher(query);
        //    Context = moSearch.Get().Cast<T>().ToList();

        //    //Context = moSearch.Get();
        //    if (Context != null)
        //        ret = Context.Count;

        //    return ret;
        //}
        public T GetItem(Func<T, bool> predicate)
        {
            return Context.Where<T>(predicate).First();
        }
        public IEnumerable<T> GetItems(Func<T, bool> predicate)
        {
            return Context.Where<T>(predicate);
        }
        public int SaveItem(T item)
        {
            throw new NotImplementedException();
        }
        public int SaveItems(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }
    }
}
