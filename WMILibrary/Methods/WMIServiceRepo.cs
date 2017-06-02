using System;
using System.Management;
using System.Collections.Generic;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class WMISeviceRepo : GenericWMIRepo<Service>, IWMIServiceRepo
    {
        private WMIManagementObjectRepo _mo_repo;

        public WMIManagementObjectRepo mo_repo
        {
            get { return _mo_repo; }
            set { _mo_repo = value; }
        }
        public WMISeviceRepo() : base("root\\CIMV2", "Select ProcessId,Name,Status,State from Win32_Service") 
        {
            //int Conn_id = 0;
            mo_repo = new WMIManagementObjectRepo(this._scope, this._query);
            Context = new List<Service>(mo_repo.Context.Count);

            foreach (ManagementObject mo in mo_repo.GetItems())
            {
                try
                {
                    Service item = new Service();

                    if (mo["ProcessId"] != null)
                        item.Id = Int32.Parse(mo["ProcessId"].ToString());
  
                    item.Name = mo["Name"]?.ToString();
                    item.Status = mo["Status"]?.ToString();
                    item.State = mo["State"]?.ToString();
                   
                    log.InfoFormat("ProcessId={0}, Name={1}, Status={2}, State={3}", item.Id, item.Name, item.Status, item.State );
                    
                    Context.Add(item);
                }
                catch (Exception e)
                {
                    log.Error("Ошибка чтения значений Service:" + e.Message);
                }

            }
        }

    }
}
