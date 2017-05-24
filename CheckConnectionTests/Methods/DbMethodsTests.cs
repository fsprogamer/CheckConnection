using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SQLite;
using CheckConnection.Methods;
using CheckConnection.Model;

namespace CheckConnection.Methods.Tests
{
    [TestClass()]
    public class DbMethodsTests//: DBMethods
    {
        private string conn_string = "Connections.db";
        private ConnectionManager connmgr;
        private DNSManager dnsmgr;
        private GatewayManager gatewaymgr;

        DbMethodsTests()
        {
            SQLiteConnection sqlconn = new SQLiteConnection(conn_string, true);
            connmgr = new ConnectionManager(sqlconn);
            dnsmgr = new DNSManager(sqlconn);
            gatewaymgr = new GatewayManager(sqlconn);
        }
        [TestMethod()]
        public void ReadConnectionHistoryTest()
        {            
            IList<Connection> connlist = connmgr.GetConnections(); 
            Assert.AreNotEqual(0, connlist.Count);
        }

        [TestMethod()]
        public void ReadDNSHistoryTest()
        {
            //DBMethods DB = new DBMethods();
            IList<Connection> connlist = connmgr.GetConnections();
            Assert.AreNotEqual(0, connlist.Count);
            foreach (Connection conn in connlist)
            {
                IList<DNS> dnslist = dnsmgr.GetDNSsByConnectionId(conn.Id); //DB.ReadDNSHistory(conn.Id);
                Assert.AreNotEqual(0, dnslist.Count);
            }
        }
        [TestMethod()]
        public void ReadGatewayHistoryTest()
        {
            //DBMethods DB = new DBMethods();
            IList<Connection> connlist = connmgr.GetConnections();
            Assert.AreNotEqual(0, connlist.Count);
            foreach (Connection conn in connlist)
            {
                IList<Gateway> gtwlist = gatewaymgr.GetGatewaysByConnectionId(conn.Id); //DB.ReadGatewayHistory(conn.Id);
                Assert.AreNotEqual(0, gtwlist.Count);
            }
        } 
        //[TestMethod()]
        //public void isTableExistsTest()
        //{
        //    const string table_name = "Connection";
        //    DBMethods db = new DBMethods();
        //    using (var conn = new SQLiteConnection(conn_string, true))
        //    {
        //        Assert.AreEqual(true, db.isTableExists(table_name, conn));
        //    }
        //}

        [TestMethod()]
        public void isConnectionStringExistsTest()
        {
            Assert.AreNotEqual(null, conn_string);
            Assert.AreEqual("Connections.db", conn_string);
        }
    }
}