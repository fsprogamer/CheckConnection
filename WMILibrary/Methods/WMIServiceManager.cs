using System;
using System.Collections.Generic;
using System.Windows.Forms;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{
    public class WMIServiceManager : ClassWithLogger<WMIServiceManager>, IWMIServiceManager
    {
        private readonly IWMIServiceRepo _repository;

        public WMIServiceManager()
        {
            _repository = new WMISeviceRepo();
        }

        public IWMIManagementObjectRepo mo_repo
        {
            get { return _repository.mo_repo; }
            set { mo_repo = value; }
        }

        public Service GetItem(Func<Service, bool> predicate)
        {
            return _repository.GetItem(predicate);
        }

        public List<Service> GetItems()
        {
            return new List<Service>(_repository.GetItems());
        }

        public List<Service> GetItems(Func<Service, bool> predicate)
        {
            return new List<Service>(_repository.GetItems(predicate));
        }             


    }
}
