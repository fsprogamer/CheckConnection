﻿using System;
using System.Collections.Generic;
using SQLite;
using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{
    public partial class DbMethods : DBConnection
    {

        public DbMethods()
        {
            conn_string = Properties.Settings.Default.DBConnectionString;
        }
        public void SaveConnectionTable( List<Connection> Connection_list, 
                                         List<DNS> DNS_list,
                                         List<Gateway> Gateway_list
                                            )
        {
            const string table_name = "Connection";

            try
            {
                using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.Create,*/ true))
                {
                    if (!isTableExists(table_name, db))
                        db.CreateTable<Connection>();

                    foreach (Connection conn in Connection_list)
                    {
                        db.RunInTransaction(() =>
                        {
                            db.Insert(conn);
                            conn.Id = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
                            SaveDNSTable(DNS_list, db, conn.Id);
                            SaveGatewayTable(Gateway_list, db, conn.Id);
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: '{0}'", e);
            }
        }

        public void SaveDNSTable(List<DNS> DNS_list, SQLiteConnection db, int connId)
        {
            string table_name = "DNS";
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
                Console.WriteLine("Ошибка: '{0}'", e);
            }
        }

        public void SaveGatewayTable(List<Gateway> Gateway_list, SQLiteConnection db, int connId)
        {
            string table_name = "Gateway";
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
                Console.WriteLine("Ошибка: '{0}'", e);
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
                Console.WriteLine("Ошибка: '{0}'", e);
            }
            return Connection_list;
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
                        var dns_array = db.Query<DNS>(String.Format("SELECT * FROM {0} where connection_id = {1} order by Order_Id asc", table_name, Connection_Id));
                        DNS_list.Capacity = dns_array.Count;
                        DNS_list.AddRange(dns_array);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: '{0}'", e);
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
                        var gateway_array = db.Query<Gateway>(String.Format("SELECT * FROM {0} where connection_id = {1} order by Id asc", table_name, Connection_Id));
                        Gateway_list.Capacity = gateway_array.Count;
                        Gateway_list.AddRange(gateway_array);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: '{0}'", e);
            }
            return Gateway_list;
        }

        public bool isTableExists(String tableName, SQLiteConnection db)
        {
            if (db != null && !String.IsNullOrWhiteSpace(tableName))
            {
                try
                {
                    int count = db.ExecuteScalar<int>("SELECT count(tbl_name) from sqlite_master where tbl_name = '" + tableName + "'");
                    if (count > 0)
                    {
                        return true;
                    }
            }
                 catch (Exception e)
            {
                Console.WriteLine("Ошибка: '{0}'", e);
            }
        }
            return false;
        }
    }
}
