using System.Collections.Generic;
using System;

namespace CheckConnection.Methods
{
   public interface IGenericWMIRepo<T> where T : class, new()
    {
        T GetItem(Func<T, bool> predicate);
        //int Query(string wmiquery);
        IEnumerable<T> GetItems(Func<T, bool> predicate);
        int SaveItem(T item);
        int SaveItems(IEnumerable<T> items);
    }
}
