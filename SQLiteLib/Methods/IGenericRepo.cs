using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IGenericRepo<T> where T : class, IEntity, new()
    {
        //Get all items in the database method
        IEnumerable<T> GetItems();        
        //Get specific item in the database method with Id
        T GetItem(int id);
        int SaveItem(T item);
        int SaveItems(IEnumerable<T> items);
        int DeleteItem(int id);
    }
}
