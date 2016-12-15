using System;
using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    interface IGenericNameRepo<T> : IGenericRepo<T> where T : class, INameEntity, new()
    {
        //Get specific item in the database method with Name
        T GetItemByName(string Name);
        //Get all items in the database method with Name
        IEnumerable<T> GetItemsByName(string Name);
    }
}
