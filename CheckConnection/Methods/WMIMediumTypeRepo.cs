using System;
using System.Management;
using CheckConnection.Model;
using System.Collections.Generic;

namespace CheckConnection.Methods
{
    class WMIMediumTypeRepo : GenericWMIRepo<MediumType>, IWMIMediumTypeRepo
    {        
        public WMIMediumTypeRepo() : base("SELECT * FROM MSNdis_PhysicalMediumType")
        {
            WMIManagementObjectRepo mo_repo = new WMIManagementObjectRepo(this._query);
            Context = new List<MediumType>(mo_repo.Context.Count);
            /*
             * realise
             */
        }
    }
}
