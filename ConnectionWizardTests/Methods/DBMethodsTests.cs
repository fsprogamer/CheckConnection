using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;
using ConnectionWizard.Methods;
using Common;
using ConnectionWizard.Model;
using System.Collections.Generic;
using System;


namespace ConnectionWizard.Methods.Tests
{
    [TestClass()]
    public class DBMethodsTests:DBMethods
    {

        [TestMethod()]
        public void GetFormsByIdTest()
        {
            DBMethods DB = new DBMethods();            
            Assert.AreEqual( 271, DB.GetFormsById(271).Id_Form );            
        }

        [TestMethod()]
        public void GetQueryTest()
        {
            DBMethods DB = new DBMethods();            
            Assert.AreNotEqual ( 0, DB.GetQuery( DB.GetFormsById(271).Id_Query_First ) );            
        }

        [TestMethod()]
        public void GetQueryTableTest()
        {
            DBMethods DB = new DBMethods();
            List<Form_Query> querylist = DB.GetQueryTable();            
            Assert.AreNotEqual(0, querylist.Count);            
        }

        [TestMethod()]
        public void GetQueryAnswerTest()
        {
            DBMethods DB = new DBMethods();
            List<Form_Ans> form_ans_list = DB.GetQueryAnswer( DB.GetFormsById(271).Id_Query_First );
        }

        [TestMethod()]
        public void GetQueryByAnswerIdTest()
        {            
            DBMethods DB = new DBMethods();
            int queryid = DB.GetQueryByAnswerId(10082);
            Assert.AreNotEqual(0, queryid);
        }

        [TestMethod()]
        public void GetQueryAnswerByTextTest()
        {
            DBMethods DB = new DBMethods();
            Form_Ans form_answer = DB.GetQueryAnswerByText("Ethernet (с роутером)");
            Assert.AreNotEqual(0, form_answer.Id_Ans);
        }
  
        [TestMethod()]
        public void isTableExistsTest()
        {
            const string table_name = "Forms";
            DBMethods db = new DBMethods();            
            using (var conn = new SQLiteConnection(conn_string, true))
            {
                Assert.AreEqual(true, db.isTableExists(table_name, conn));
            }
        }
        [TestMethod()]
        public void isConnectionStringExistsTest()
        {
            Assert.AreNotEqual(null, conn_string);
            Assert.AreEqual("WizardSteps.db", conn_string);
        }

        #region Not implemented
        //[TestMethod()]
        //public void GetNextQueryIdTest()
        //{

        //}

        //[TestMethod()]
        //public void GetNextQueryTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void GetNextFormAnsAboTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void Form_Next_QueryTest()
        //{
        //    Assert.Fail();
        //}
        #endregion
    }
}