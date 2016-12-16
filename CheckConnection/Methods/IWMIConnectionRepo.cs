using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IWMIConnectionRepo: IGenericWMIRepo<Connection>
    {       
        List<Connection> GetItems();
    }
}
