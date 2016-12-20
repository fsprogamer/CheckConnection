using System;
using System.Collections.Generic;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{

    public class WMIMediumTypeManager : ClassWithLog, IWMIMediumTypeManager
    {
        private readonly IWMIMediumTypeRepo _repository;

        public WMIMediumTypeManager()
        {
            _repository = new WMIMediumTypeRepo();
        }

        public MediumType GetItem(Func<MediumType, bool> predicate)
        {
            return _repository.GetItem(predicate);
        }

    }
}
