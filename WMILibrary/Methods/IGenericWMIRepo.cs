using System.Collections.Generic;
using System;

namespace CheckConnection.Methods
{
   public interface IGenericWMIRepo<T> where T : class, new()
    {
        T GetItem(Func<T, bool> predicate);
        //int Query(string wmiquery);
        List<T> GetItems(Func<T, bool> predicate);
        List<T> GetItems();
        //int SaveItem(T item);
        //int SaveItems(IEnumerable<T> items);
    }
}
