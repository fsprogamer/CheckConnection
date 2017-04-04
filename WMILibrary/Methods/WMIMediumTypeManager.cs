using System;
using System.Collections.Generic;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{

    public class WMIMediumTypeManager : ClassWithLogger<WMIMediumTypeManager>, IWMIMediumTypeManager
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

        public bool IsWireless(string name)
        {
            try
            {
                log.InfoFormat("p => p.Name == {0}",name);
                uint PhysicalMediumType = GetItem(p => p.Name.StartsWith(name)).NdisPhysicalMediumType;
                if ((PhysicalMediumType == 1) || (PhysicalMediumType == 9))
                    return true;
            }
            catch(Exception e)
            {
                log.Error("Ошибка чтения PhysicalMediumType: ", e);
            } 
            return false;
        }

    }
}
