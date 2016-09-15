﻿using System.Collections.Generic;
using SQLite;
using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{
    partial class DBMethods : DBConnection
    {
        public void SavePingTable(ref List<PingResult> Ping_list)
        {
            using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.Create,*/ true))
            {
                db.CreateTable<PingResult>();
                foreach (PingResult png in Ping_list)
                {
                    db.Insert(png);
                }
            }
        }

        public void SaveTracertTable(ref List<Tracert> Tracert_list)
        {
            using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.Create,*/ true))
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
            using (var db = new SQLiteConnection(conn_string, /*SQLiteOpenFlags.Create,*/ true))
            {
                db.CreateTable<Hop>();

                foreach (Hop hop in Hop_list)
                {
                    db.Insert(hop);
                }
            }
        }
    }
}
