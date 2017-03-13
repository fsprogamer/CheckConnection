using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    interface IConnectionRepo : IGenericNameRepo<Connection>
    {      
        IEnumerable<Connection> GetItemsPage(int Offset = 0, int Pagesize = 0);
        IEnumerable<Connection> GetItemsPageByName(string Name, int Offset = 0, int Pagesize = 0);
        int GetItemsAmount();
        int GetItemsAmountByName(string Name);
        int GetLastInsertRowId();
        int GetDiffInDays();
    }
}
