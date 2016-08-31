using System;
using SQLite;
using ConnectionWizard.Model;
using System.Collections.Generic;
using Common;

namespace ConnectionWizard.Methods
{
    class DBMethods : DBConnection
    {
        public DBMethods()
        {
            conn_string = Properties.Settings.Default.DBConnectionString;
        }

        public List<Step> ReadWizardSteps()
        {
            string table_name = "Step";
            List<Step> Step_list = new List<Step>();

            using (var db = new SQLite.SQLiteConnection(conn_string, SQLiteOpenFlags.ReadOnly, true))
            {
                if (isTableExists(table_name, db))
                {
                    var connections = db.Query<Step>(String.Format("SELECT * FROM {0} order by OrderId asc", table_name));
                    foreach (var conn in connections)
                    {
                        Step_list.Add(conn);
                    }
                }
            }
            return Step_list;
        }

        public void InitSteps()
        {
            string table_name = "Step";
            List<Step> Step_list = new List<Step>  {
               new Step(){ OrderId = 1, Name = "Шаг 1", Text = "Делай раз", Type = 1},
               new Step(){ OrderId = 2, Name = "Шаг 2", Text = "Делай два", Type = 1},
               new Step(){ OrderId = 3, Name = "Шаг 3", Text = "Делай три", Type = 1},
               new Step(){ OrderId = 4, Name = "Шаг 4", Text = "Делай четыре", Type = 1},
               new Step(){ OrderId = 5, Name = "Шаг 5", Text = "Делай пять", Type = 1}
            };

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Step>();
                }

                foreach (Step step in Step_list)
                {

                    db.RunInTransaction(() =>
                    {
                        db.Insert(step);
                        step.Id = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
                    });

                }
            }
        }

        public bool isTableExists(String tableName, SQLiteConnection db)
        {
            if (db != null && !String.IsNullOrWhiteSpace(tableName))
            {
                int count = db.ExecuteScalar<int>("SELECT count(tbl_name) from sqlite_master where tbl_name = '" + tableName + "'");
                if (count > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
