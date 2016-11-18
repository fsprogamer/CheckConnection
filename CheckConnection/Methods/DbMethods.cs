using System.Collections.Generic;
using SQLite;
using CheckConnection.Model;
using System.Linq;
using System;
using Common;
using log4net;

namespace CheckConnection.Methods
{
    public partial class DBMethods : DBConnection
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        SQLiteConnection db;
        static readonly object Locker = new object();

        public DBMethods(SQLiteConnection conn, string path)
        {
            db = conn;
            //Create the tables
            try
            {
                db.CreateTable<Connection>();
                db.CreateTable<DNS>();
                db.CreateTable<Gateway>();
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
        }

        //Get all items in the database method
        public IEnumerable<T> GetItems<T>() where T : IEntity, new()
        {
            lock (Locker)
            {
                try { 
                    return (from i in db.Table<T>() select i).ToList();
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }

        //Get all items in the database method with Name
        public IEnumerable<T> GetItemsByName<T>(string Name) where T : INameEntity, new()
        {
            lock (Locker)
            {
                try
                {
                    return (from i in db.Table<T>() select i).Where(i => i.Name == Name).ToList();
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }      

        //Get specific item in the database method with Id
        public T GetItem<T>(int id) where T : IEntity, new()
        {
            lock (Locker)
            {
                try
                {
                    return db.Table<T>().FirstOrDefault(x => x.Id == id);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return default(T);
                }
            }
        }

        //Get specific item in the database method with Name
        public T GetItemByName<T>(string Name) where T : INameEntity, new()
        {
            lock (Locker)
            {
                try
                {
                    return db.Table<T>().FirstOrDefault(x => x.Name == Name);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return default(T);
                }
            }
        }
        
        public int SaveItem<T>(T item) where T : IEntity
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
                    return db.Insert(item);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }

        public int SaveItems<T>(IEnumerable<T> items) where T : IEntity
        {
            lock (Locker)
            {
                try
                {
                    return db.InsertAll(items);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }        

        public int DeleteItem<T>(int id) where T : IEntity, new()
        {
            lock (Locker)
            {
                try
                {
                    return db.Delete<T>(id);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }

        //------------Special--------------

        //Get last insert rowid
        public int GetLastInsertRowId()
        {
            lock (Locker)
            {
                try
                {
                    return db.ExecuteScalar<int>("SELECT last_insert_rowid()");
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }

        public int GetConnectionAmount()
        {
            lock (Locker)
            {
                try
                {
                    return db.ExecuteScalar<int>("SELECT count(*) FROM Connection");
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }

        //Get amount of item in the database method with Name
        public int GetConnectionAmountByName(string Name)
        {
            lock (Locker)
            {
                try
                {
                    return db.ExecuteScalar<int>("SELECT count(*) FROM Connection where Name=?", Name);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }

        //Get page of Connection items in the database method with Name
        public IEnumerable<Connection> GetConnectionPage(int Offset = 0, int Pagesize = 0)
        {
            lock (Locker)
            {
                try
                {
                    return db.Query<Connection>(@"select Id,Date,Name,MAC,Ip_Address_v4,Ip_Address_v6,DHCP_Enabled,DHCPServer,DNSDomain,IPSubnetMask 
                        from (SELECT *,
                        (SELECT COUNT(*)
                        FROM Connection AS t2
                        WHERE t2.Date >= t1.Date                        
		                ) AS row_Num
                        FROM Connection AS t1                        
                        ORDER BY Date desc) t3
                        where row_Num between ? and ?", Offset + 1, Offset + Pagesize).ToList();
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }

        //Get page of Connection items in the database method with Name
        public IEnumerable<Connection> GetConnectionPageByName(string Name, int Offset = 0, int Pagesize = 0)
        {
            lock (Locker)
            {
                try
                {
                    return db.Query<Connection>(@"select Id,Date,Name,MAC,Ip_Address_v4,Ip_Address_v6,DHCP_Enabled,DHCPServer,DNSDomain,IPSubnetMask 
                        from (SELECT *,
                        (SELECT COUNT(*)
                        FROM Connection AS t2
                        WHERE t2.Date >= t1.Date
                        and Name = ?
		                ) AS row_Num
                        FROM Connection AS t1
                        where Name = ?
                        ORDER BY Date desc) t3
                        where row_Num between ? and ?", Name, Name, Offset + 1, Offset + Pagesize).ToList();
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }

        public IEnumerable<DNS> GetDNSByConnectionId(int Connection_Id)
        {
            lock (Locker)
            {
                try
                {
                    return db.Query<DNS>("SELECT * FROM DNS where connection_id = ? order by Order_Id asc", Connection_Id).ToList();
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }

        public IEnumerable<Gateway> GetGatewayByConnectionId(int Connection_Id)
        {
            lock (Locker)
            {
                try
                {
                    return db.Query<Gateway>("SELECT * FROM Gateway where connection_id = ? order by Id asc", Connection_Id).ToList();
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }
    }
}
