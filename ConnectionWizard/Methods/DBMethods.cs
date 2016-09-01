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

        public Forms GetFormsByName(int form_id)
        {
            string table_name = "Forms";
            List<Forms> forms_list = new List<Forms>();

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (isTableExists(table_name, db))
                {
                    var forms_array = db.Query<Forms>(String.Format("SELECT * FROM {0} where id_form = '{1}'", table_name, form_id));
                    foreach (var forms in forms_array)
                     {
                        forms_list.Add(forms);
                     }
                }
            }
            return forms_list[0];
        }

        public List<Form_Query> GetNextQuery(int idform, int idquery)
        {
            string table_name = "Form_Query";
            List<Form_Query> form_query_list = new List<Form_Query>();

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (isTableExists(table_name, db))
                {
                        var form_query_array = db.Query<Form_Query>(String.Format("SELECT * FROM {0} where Id_Query = {1} and Id_Form={2}", table_name, idquery, idform));
                        foreach (var fq in form_query_array)
                        {
                            form_query_list.Add(fq);
                        }
                }
            }
            return form_query_list;
        }

        public List<Form_Ans> GetNextQueryAnswer( int idquery)
        {
            string table_name = "Form_Ans";
            List<Form_Ans> form_ans_list = new List<Form_Ans>();

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (isTableExists(table_name, db))
                {

                        var form_ans_array = db.Query<Form_Ans>(String.Format("SELECT * FROM {0} where Id_Query = {1}", table_name, idquery));
                        foreach (var fa in form_ans_array)
                        {
                            form_ans_list.Add(fa);
                        }

                }
            }
            return form_ans_list;
        }

        public int SetFormVisit(int pId_Form)
        {
            string table_name = "Form_Visit";
            Form_Visit form_visit = new Form_Visit {
                 Id_Form = pId_Form, Date_Beg = DateTime.Today
            };

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Form_Visit>();
                }

                db.RunInTransaction(() =>
                 {
                    db.Insert(form_visit);
                    form_visit.Id_Visit = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
                 });

            }

            return form_visit.Id_Visit;
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

        public void InitWizardDB()
        {
            InitForms();
            InitFormQuery();
            InitFormAns();
            InitFormAnsAbo();
            InitFormCursTemp();
            InitFormQueryCurs();
        }

        public void InitForms()
        {
            string table_name = "Forms";
            //List<Forms> form_list = new List<Forms>  {
            //new Forms(){ Id_Form = 271, Name = "Диагностика", Id_Query_First = 2360, Status = 1}
            //};

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Forms>();
                }

                //foreach (Form form in form_list)
                //{

                //    db.RunInTransaction(() =>
                //    {
                //        db.Insert(form);
                //        form.Id_Form = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
                //    });

                //}
            }
        }

        public void InitFormQuery()
        {
            string table_name = "Form_Query";
            //List<Form_Query> fquery_list = new List<Form_Query>  {
            //    new Form_Query(){ Id_Form = 271, Id_Query = 2360, Query = "Выберите конфигурацию", Num_Query = 2361},
            //    new Form_Query(){ Id_Form = 271, Id_Query = 2361, Query = "Проверить состояние сетевого подключения ?", Num_Query = 2362},
            //    new Form_Query(){ Id_Form = 271, Id_Query = 2362, Query = "Есть IP-адрес ?", Num_Query = 2363},
            //    new Form_Query(){ Id_Form = 271, Id_Query = 2363, Query = "Ip-адресов больше одного ?", Num_Query = 0},
            //    new Form_Query(){ Id_Form = 271, Id_Query = 2364, Query = "Есть включенные адаптеры ?", Num_Query = 0}
            //};

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Form_Query>();
                }

                //foreach (Form_Query fquery in fquery_list)
                //{

                //    db.RunInTransaction(() =>
                //    {
                //        db.Insert(fquery);                        
                //    });

                //}
            }
        }

        public void InitFormAns()
        {
            string table_name = "Form_Ans";

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Form_Ans>();
                }

                //foreach (Form_Query fquery in fquery_list)
                //{

                //    db.RunInTransaction(() =>
                //    {
                //        db.Insert(fquery);
                //    });

                //}
            }
        }

        public void InitFormAnsAbo()
        {
            string table_name = "Form_Ans_Abo";

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Form_Ans_Abo>();
                }
            }
        }

        public void InitFormCursTemp()
        {
            string table_name = "Form_Curs_Temp";

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Form_Curs_Temp>();
                }
            }
        }

        public void InitFormQueryCurs()
        {
            string table_name = "Form_Query_Curs";

            using (var db = new SQLiteConnection(conn_string, true))
            {
                if (!isTableExists(table_name, db))
                {
                    db.CreateTable<Form_Query_Curs>();
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
