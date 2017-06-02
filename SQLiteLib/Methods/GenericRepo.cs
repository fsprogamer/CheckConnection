using System.Collections.Generic;
using SQLite;
using CheckConnection.Model;
using System.Linq;
using System;
using Common;

namespace CheckConnection.Methods
{     
    public abstract class GenericRepo<C,T> : ClassWithLogger<T>, IGenericRepo<T> where T : class, IEntity, new()
                                                                           where C : SQLiteConnection
    {
        private C _entities;
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        static protected readonly object Locker = new object();

        public GenericRepo(C conn)
        {
            Context = conn;
            //Create the tables
            try
            {
                Context.CreateTable<T>();
                //Context.CreateTable<DNS>();
                //Context.CreateTable<Gateway>();
                log.Info("DB Created.");
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
        }
        //Get all items in the database method
        public IEnumerable<T> GetItems()
        {
            lock (Locker)
            {
                try
                {
                    return (from i in Context.Table<T>() select i)/*.ToList()*/;
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }
        //Get specific item in the database method with Id
        public T GetItem(int id) 
        {
            lock (Locker)
            {
                try
                {
                    return Context.Table<T>().FirstOrDefault(x => x.Id == id);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return default(T);
                }
            }
        }
        public int SaveItem(T item) 
        {
            lock (Locker)
            {
                try
                {
                    //if (item.Id != 0)
                    //{
                    //    db.Update(item);
                    //    return item.Id;
                    //}
                    return Context.Insert(item);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }
        public int SaveItems(IEnumerable<T> items) 
        {
            lock (Locker)
            {
                try
                {
                    return Context.InsertAll(items);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }
        public int DeleteItem(int id) 
        {
            lock (Locker)
            {
                try
                {
                    return Context.Delete<T>(id);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }
    }
}
