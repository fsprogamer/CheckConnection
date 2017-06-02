using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace CheckConnection.Methods
{
    class WMIManagementObjectManager: IWMIManagementObjectManager
    {
        private readonly IWMIManagementObjectRepo _repository;
        public WMIManagementObjectManager(string scope, string query)
        {
            _repository = new WMIManagementObjectRepo(scope, query);
        }
        public IEnumerable<ManagementObject> GetItems()
        {
            return _repository.GetItems(p => true)/*.ToList()*/;
        }

        public ManagementObject GetItem(Func<ManagementObject, bool> predicate)
        {
            return _repository.GetItem(predicate);
        }

        public int Count
        {
            get
            {
                return _repository.GetItems(p => true).Count();
            }
        }

    }
}
