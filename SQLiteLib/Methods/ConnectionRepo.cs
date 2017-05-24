using System.Collections.Generic;
using System.Linq;
using SQLite;
using System;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class ConnectionRepo : GenericNameRepo<SQLiteConnection, Connection >, IConnectionRepo
    {
        //static readonly object Locker = new object();

        public ConnectionRepo(SQLiteConnection conn) : base(conn)
        {
            log.Info("ConnectionRepo costructor");
        }
        public IEnumerable<Connection> GetItemsPage(int Offset = 0, int Pagesize = 0)
        {
            lock (Locker)
            {
                try
                {
                    return Context.Query<Connection>(@"select Id,Date,NetConnectionID,Name,MAC,Ip_Address_v4,Ip_Address_v6,DHCP_Enabled,DHCPServer,DNSDomain,IPSubnetMask,NetConnectionStatus,NetEnabled 
                        from (SELECT *,
                        (SELECT COUNT(*)
                        FROM Connection AS t2
                        WHERE t2.Date >= t1.Date                        
		                ) AS row_Num
                        FROM Connection AS t1                        
                        ORDER BY Date desc) t3
                        where row_Num between ? and ?", Offset + 1, Offset + Pagesize)/*.ToList()*/;
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }
        public IEnumerable<Connection> GetItemsPageByName(string Name, int Offset = 0, int Pagesize = 0)
        {
            lock (Locker)
            {
                try
                {
                    return Context.Query<Connection>(@"select Id,Date,NetConnectionID,Name,MAC,Ip_Address_v4,Ip_Address_v6,DHCP_Enabled,DHCPServer,DNSDomain,IPSubnetMask,NetConnectionStatus,NetEnabled  
                        from (SELECT *,
                        (SELECT COUNT(*)
                        FROM Connection AS t2
                        WHERE t2.Date >= t1.Date
                        and Name = ?
		                ) AS row_Num
                        FROM Connection AS t1
                        where Name = ?
                        ORDER BY Date desc) t3
                        where row_Num between ? and ?", Name, Name, Offset + 1, Offset + Pagesize)/*.ToList()*/;
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return null;
                }
            }
        }
        public int GetItemsAmount()
        {
            lock (Locker)
            {
                try
                {                    
                    return Context.ExecuteScalar<int>("SELECT count(*) FROM Connection");
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }
        public int GetItemsAmountByName(string Name)
        {
            lock (Locker)
            {
                try
                {
                    return Context.ExecuteScalar<int>("SELECT count(*) FROM Connection where Name=?", Name);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }
        public int GetLastInsertRowId()
        {
            lock (Locker)
            {
                try
                {
                    return Context.ExecuteScalar<int>("SELECT last_insert_rowid()");
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                    return 0;
                }
            }
        }

        public int GetDiffInDays()
        {
            lock (Locker)
            {
                try
                {
                    return Context.ExecuteScalar<int>(@"select (max(Date) / 10000000 - min(Date) / 10000000) / (24 * 3600) from Connection");
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
