using System;
using System.Net.NetworkInformation;
using System.Net;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{

    public class WMINetworkAdapterManager : ClassWithLog, IWMINetworkAdapterManager
    {
        private readonly IWMINetworkAdapterRepo _repository;

        public WMINetworkAdapterManager()
        {
            _repository = new WMINetworkAdapterRepo();
        }

        public IWMIManagementObjectRepo mo_repo
        {
            get { return _repository.mo_repo; }
            set { mo_repo = value; }
        }

        public NetworkAdapter GetItem(Func<NetworkAdapter, bool> predicate)
        {
            return _repository.GetItem(predicate);
        }

    }
}
