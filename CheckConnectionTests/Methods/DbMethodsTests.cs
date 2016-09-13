using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;
using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods.Tests
{
    [TestClass()]
    public class DbMethodsTests: DbMethods
    {
    
        [TestMethod()]
        public void ReadConnectionHistoryTest()
        {
            DbMethods DB = new DbMethods();
            List<Connection> connlist = DB.ReadConnectionHistory();
            Assert.AreNotEqual(0, connlist.Count);
        }

        [TestMethod()]
        public void ReadDNSHistoryTest()
        {
            DbMethods DB = new DbMethods();
            List<Connection> connlist = DB.ReadConnectionHistory();
            Assert.AreNotEqual(0, connlist.Count);
            foreach (Connection conn in connlist)
            {
                List<DNS> dnslist = DB.ReadDNSHistory(conn.Id);
                Assert.AreNotEqual(0, dnslist.Count);
            }
        }
        [TestMethod()]
        public void ReadGatewayHistoryTest()
        {
            DbMethods DB = new DbMethods();
            List<Connection> connlist = DB.ReadConnectionHistory();
            Assert.AreNotEqual(0, connlist.Count);
            foreach (Connection conn in connlist)
            {
                List<Gateway> gtwlist = DB.ReadGatewayHistory(conn.Id);
                Assert.AreNotEqual(0, gtwlist.Count);
            }
        } 
        [TestMethod()]
        public void isTableExistsTest()
        {
            const string table_name = "Connection";
            DbMethods db = new DbMethods();
            using (var conn = new SQLiteConnection(conn_string, true))
            {
                Assert.AreEqual(true, db.isTableExists(table_name, conn));
            }
        }

        [TestMethod()]
        public void isConnectionStringExistsTest()
        {
            Assert.AreNotEqual(null, conn_string);
            Assert.AreEqual("Connections.db", conn_string);
        }
    }
}