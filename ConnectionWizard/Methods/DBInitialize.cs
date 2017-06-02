using System;
using SQLite;
using ConnectionWizard.Model;
using System.Collections.Generic;
using Common;

namespace ConnectionWizard.Methods
{
    partial class DBMethods : DBConnection
    {
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

    }
}
