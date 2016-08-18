using System;
using System.Collections.Generic;
using SQLite;

namespace ReestrUser
{
    class DbMethods
    {
        public void SaveConnectionTable(ref List<Connection> Connection_list)
        {
            string table_name = "Connection";
            using (var db = new SQLiteConnection("Connections.db", /*SQLiteOpenFlags.Create,*/ true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Connection>();
                }
                foreach( Connection conn in Connection_list )
                {
                    db.Insert(conn);
                }
            }
        }

        public int ReadConnectionHistory(ref List<Connection> Connection_list)
        {
            string table_name = "Connection";

            using (var db = new SQLiteConnection("Connections.db", /*SQLiteOpenFlags.ReadOnly,*/ true))
            {
                if (isTableExists(table_name, db))
                {
                    var connections = db.Query<Connection>(String.Format("SELECT * FROM {0} order by Date desc", table_name));
                    foreach (var conn in connections)
                    {
                        Connection_list.Add(conn);
                    }
                }
            }
            return Connection_list.Count;
        }

        public void SavePingTable(ref List<Ping> Ping_list)
        {

            using (var db = new SQLiteConnection("Connections.db", /*SQLiteOpenFlags.Create,*/ true))
            {
                db.CreateTable<Ping>();

                foreach (Ping png in Ping_list)
                {
                    db.Insert(png);
                }

            }
        }

        public void SaveTracertTable(ref List<Tracert> Tracert_list)
        {

            using (var db = new SQLiteConnection("Connections.db", /*SQLiteOpenFlags.Create,*/ true))
            {
                db.CreateTable<Tracert>();

                foreach (Tracert trc in Tracert_list)
                {
                    db.Insert(trc);
                }

            }
        }

        public void SaveHopTable(ref List<Hop> Hop_list)
        {

            using (var db = new SQLiteConnection("Connections.db", /*SQLiteOpenFlags.Create,*/ true))
            {
                db.CreateTable<Hop>();

                foreach (Hop hop in Hop_list)
                {
                    db.Insert(hop);
                }

            }
        }

        public bool isTableExists(String tableName, SQLiteConnection db)
        {
            if ( db !=null && !String.IsNullOrWhiteSpace(tableName) )
            {
                int count = db.ExecuteScalar<int>("SELECT count(tbl_name) from sqlite_master where tbl_name = '" + tableName + "'");
                if (count > 0) {
                    return true;
                }
            }
           
            return false;
        }
    }
}
