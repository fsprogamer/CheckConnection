using System;
using System.Management;
using CheckConnection.Model;
using System.Collections.Generic;

namespace CheckConnection.Methods
{
    class WMIMediumTypeRepo : GenericWMIRepo<MediumType>, IWMIMediumTypeRepo
    {        
        public WMIMediumTypeRepo() : base("root\\wmi", "SELECT * FROM MSNdis_PhysicalMediumType")
        {
            log.Info("before WMIManagementObjectRepo");
            WMIManagementObjectRepo mo_repo = new WMIManagementObjectRepo(this._scope, this._query);
            log.Info("after WMIManagementObjectRepo");

            if(mo_repo.Context != null) { 
            Context = new List<MediumType>(mo_repo.Context.Count);

                foreach (ManagementObject mo in mo_repo.GetItems(m => true))
                {
                    try
                    {
                        MediumType item = new MediumType();

                        if (mo["Active"] != null)
                        {
                            item.Active = mo["Active"].ToString();
                            log.InfoFormat("Active={0}", mo["Active"].ToString());
                        }

                        if (mo["InstanceName"] != null)
                        {
                            item.Name = mo["InstanceName"].ToString();
                            log.InfoFormat("Name={0}", mo["InstanceName"].ToString());
                        }

                        if (mo["NdisPhysicalMediumType"] != null)
                        {
                            item.NdisPhysicalMediumType = Convert.ToUInt32(mo["NdisPhysicalMediumType"]);
                            log.InfoFormat("NdisPhysicalMediumType={0}", mo["NdisPhysicalMediumType"].ToString());
                        }

                        Context.Add(item);
                    }
                    catch (Exception)
                    {
                        log.Error("Ошибка чтения значений MSNdis_PhysicalMediumType");
                    }
                }

            }
        }
    }
}
