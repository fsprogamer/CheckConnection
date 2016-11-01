using System;
using System.Collections.Generic;
using SQLite;
using CheckConnection.Model;
using Common;
using log4net;

namespace CheckConnection.Methods
{
    public partial class DBMethods : DBConnection,DBInterface
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DBMethods()
        {
            conn_string = Properties.Settings.Default.DBConnectionString;
            using (var db = new SQLiteConnection(conn_string, true))
            {
                //Create the tables
                db.CreateTable<Connection>();
                db.CreateTable<DNS>();
                db.CreateTable<Gateway>();                
            }
        }
        public void SaveConnectionTable( List<ConnectionParam> connparam )
        {
            try
            {
                using (var db = new SQLiteConnection(conn_string, true))
                {
                    foreach (ConnectionParam conn in connparam)
                    {
                        db.RunInTransaction(() =>
                        {
                            db.Insert(conn.Connection);
                            conn.Connection.Id = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
                            SaveDNSTable(conn.DNS_list, db, conn.Connection.Id);
                            SaveGatewayTable(conn.Gateway_list, db, conn.Connection.Id);
                        });
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
        }

        public void SaveDNSTable(List<DNS> DNS_list, SQLiteConnection db, int connId)
        {
            if (DNS_list != null)
            {
                try
                {
                    foreach (DNS dns in DNS_list)
                        dns.Connection_Id = connId;

                    db.InsertAll(DNS_list);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                }
            }
        }

        public void SaveGatewayTable(List<Gateway> Gateway_list, SQLiteConnection db, int connId)
        {
            if (Gateway_list != null)
            {
                try
                {
                    foreach (Gateway gtw in Gateway_list)
                        gtw.Connection_Id = connId;

                    db.InsertAll(Gateway_list);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка: '{0}'", e);
                }
            }
        }

        public List<Connection> ReadConnectionHistory()
        {
            List<Connection> Connection_list = new List<Connection>();
            try { 
                using (var db = new SQLiteConnection(conn_string, true))
                {
                        var connections = db.Query<Connection>("SELECT * FROM Connection order by Date desc");
                        Connection_list.Capacity = connections.Count;
                        Connection_list.AddRange(connections);
                }
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
            return Connection_list;
        }

        public List<Connection> ReadConnectionHistory(string name, int offset = 0, int pagesize = 0)
        {
            List<Connection> Connection_list = new List<Connection>(pagesize);
            try
            {
                using (var db = new SQLiteConnection(conn_string, true))
                {
                        //var connections = db.Query<Connection>("SELECT * FROM Connection where Name=?", name);
                        var connections = db.Query<Connection>(@"select Id,Date,Name,MAC,Ip_Address_v4,Ip_Address_v6,DHCP_Enabled,DHCPServer,DNSDomain,IPSubnetMask 
                        from (SELECT *,
                        (SELECT COUNT(*)
                        FROM Connection AS t2
                        WHERE t2.Date >= t1.Date
                        and Name = ?
		                ) AS row_Num
                        FROM Connection AS t1
                        where Name = ?
                        ORDER BY Date desc) t3
                        where row_Num between ? and ?", name, name, offset+1, offset+pagesize);

                        Connection_list.Capacity = connections.Count;
                        Connection_list.AddRange(connections);
                }
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
            return Connection_list;
        }

        public int ReadConnectionHistoryCount(string name)
        {
            int reccount = 0;
            List<Connection> Connection_list = new List<Connection>();
            try
            {
                using (var db = new SQLiteConnection(conn_string, true))
                {
                    reccount = db.ExecuteScalar<int>("SELECT count(*) FROM Connection where Name=?", name);                                        
                }
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
            return reccount;
        }

        public List<DNS> ReadDNSHistory(int Connection_Id)
        {
            List<DNS> DNS_list = new List<DNS>();
            try
            {
                using (var db = new SQLiteConnection(conn_string, true))
                {
                    var dns_array = db.Query<DNS>("SELECT * FROM DNS where connection_id = ? order by Order_Id asc", Connection_Id);
                    DNS_list.Capacity = dns_array.Count;
                    DNS_list.AddRange(dns_array);
                }
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
            return DNS_list;
        }

        public List<Gateway> ReadGatewayHistory(int Connection_Id)
        {
            List<Gateway> Gateway_list = new List<Gateway>();
            try
            {
                using (var db = new SQLiteConnection(conn_string, true))
                {
                    var gateway_array = db.Query<Gateway>("SELECT * FROM Gateway where connection_id = ? order by Id asc",  Connection_Id);
                    Gateway_list.Capacity = gateway_array.Count;
                    Gateway_list.AddRange(gateway_array);
                }
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
            return Gateway_list;
        }

        public bool isTableExists(String tableName, SQLiteConnection db)
        {
            if (db != null && !String.IsNullOrWhiteSpace(tableName))
            {
                try
                {
                    int count = db.ExecuteScalar<int>("SELECT count(tbl_name) from sqlite_master where tbl_name = ?", tableName);
                    if (count > 0)
                    {
                        return true;
                    }
            }
                 catch (Exception e)
            {
                    log.Error("Ошибка: '{0}'", e);
            }
        }
            return false;
        }
    }
}
