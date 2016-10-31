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
        }
        public void SaveConnectionTable( List<ConnectionParam> connparam )
        {
            const string table_name = "Connection";

            try
            {
                using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.Create,*/ true))
                {
                    if (!isTableExists(table_name, db))
                        db.CreateTable<Connection>();

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
            string table_name = "DNS";
            if (DNS_list != null)
            {
                try
                {
                    if (!isTableExists(table_name, db))
                        db.CreateTable<DNS>();

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
            string table_name = "Gateway";
            if (Gateway_list != null)
            {
                try
                {
                    if (!isTableExists(table_name, db))
                        db.CreateTable<Gateway>();

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
            const string table_name = "Connection";
            List<Connection> Connection_list = new List<Connection>();
            try { 
                using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.ReadOnly,*/ true))
                {
                    if (isTableExists(table_name, db))
                    {
                        var connections = db.Query<Connection>(String.Format("SELECT * FROM {0} order by Date desc", table_name));
                        Connection_list.Capacity = connections.Count;
                        Connection_list.AddRange(connections);
                    }
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
            const string table_name = "Connection";
            List<Connection> Connection_list = new List<Connection>(pagesize);
            try
            {
                using (var db = new SQLiteConnection(conn_string, true))
                {
                    if (isTableExists(table_name, db))
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
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
            return Connection_list;
        }

        public int ReadConnectionHistoryCount(string name)
        {
            const string table_name = "Connection";
            int reccount = 0;
            List<Connection> Connection_list = new List<Connection>();
            try
            {
                using (var db = new SQLiteConnection(conn_string, true))
                {
                    if (!isTableExists(table_name, db))
                        db.CreateTable<Connection>();
                    reccount = db.ExecuteScalar<int>("SELECT count(*) FROM Connection where Name=?", name);                                        
                }
            }
            catch (Exception e)
            {
                log.Error("Ошибка: '{0}'", e);
            }
            return reccount;
        }
        //public List<Connection> ReadFullConnectionHistory()
        //{
        //    string[] table_name = new string[] { "Connection", "DNS", "Gateway" };
        //    List<Connection> Connection_list = new List<Connection>();

        //    using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.ReadOnly,*/ true))
        //    {
        //        if (isTableExists(table_name, db))
        //        {
        //            var connections = db.Query<Connection>(String.Format("SELECT * FROM {0} order by Date desc", table_name));
        //            foreach (var conn in connections)
        //            {
        //                Connection_list.Add(conn);
        //            }
        //        }
        //    }
        //    return Connection_list;
        //}
        public List<DNS> ReadDNSHistory(int Connection_Id)
        {
            const string table_name = "DNS";
            List<DNS> DNS_list = new List<DNS>();
            try
            {
                using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.ReadOnly,*/ true))
                {
                    if (isTableExists(table_name, db))
                    {
                        var dns_array = db.Query<DNS>("SELECT * FROM DNS where connection_id = ? order by Order_Id asc", Connection_Id);
                        DNS_list.Capacity = dns_array.Count;
                        DNS_list.AddRange(dns_array);
                    }
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
            const string table_name = "Gateway";
            List<Gateway> Gateway_list = new List<Gateway>();
            try
            {
                using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.ReadOnly,*/ true))
                {
                    if (isTableExists(table_name, db))
                    {
                        var gateway_array = db.Query<Gateway>("SELECT * FROM Gateway where connection_id = ? order by Id asc",  Connection_Id);
                        Gateway_list.Capacity = gateway_array.Count;
                        Gateway_list.AddRange(gateway_array);
                    }
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
