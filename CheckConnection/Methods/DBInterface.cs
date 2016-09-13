using System;
using System.Collections.Generic;
using CheckConnection.Model;
using SQLite;

namespace CheckConnection.Methods
{
    public interface DBInterface
    {
        void SaveConnectionTable(List<Connection> Connection_list,
                                 List<DNS> DNS_list,
                                 List<Gateway> Gateway_list
                                    );
        void SaveDNSTable(List<DNS> DNS_list, SQLiteConnection db, int connId);

        void SaveGatewayTable(List<Gateway> Gateway_list, SQLiteConnection db, int connId);

        List<Connection> ReadConnectionHistory();

        List<DNS> ReadDNSHistory(int Connection_Id);

        List<Gateway> ReadGatewayHistory(int Connection_Id);

        bool isTableExists(String tableName, SQLiteConnection db);

    }
}
