using System;
using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IWMIMediumTypeManager
    {
        MediumType GetItem(Func<MediumType, bool> predicate);

        bool IsWireless(string name);
    }
}
