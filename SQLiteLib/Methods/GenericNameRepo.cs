using SQLite;
using System.Collections.Generic;
using System.Linq;
using System;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public abstract class GenericNameRepo<C, T> : GenericRepo<C,T>, IGenericNameRepo<T> where T : class, INameEntity, IEntity, new()
                                                                               where C : SQLiteConnection
    {
        public GenericNameRepo(C conn): base(conn)
        {            
        }

        //Get all items in the database method with Name
        public IEnumerable<T> GetItemsByName(string Name)
        {
            lock (Locker)
            {
                try
                {
                    return (from i in Context.Table<T>() select i).Where(i => i.Name == Name)/*.ToList()*/;
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }
       
        //Get specific item in the database method with Name
        public T GetItemByName(string Name)
        {
            lock (Locker)
            {
                try
                {
                    return Context.Table<T>().FirstOrDefault(x => x.Name == Name);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return default(T);
                }
            }
        }

    }
}
