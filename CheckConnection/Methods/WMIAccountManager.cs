using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Principal;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{
    class WMIAccountManager:ClassWithLog
    {        
        private WMIInterface _wmi;

        public WMIAccountManager(WMIInterface pwmi)
        {
            _wmi = pwmi;
        }

        public int GetCurrentAccounts()
        {
            const string CurrentAccount_query = "SELECT * FROM Win32_Account"; //where Name='svfrolov'
            //const string LogonSession_query = "Select* from Win32_LogonSession Where LogonType = 2 OR LogonType = 10"
            //const string LogonUser_query = "Select * from Win32_LoggedOnUser";
            //const string ComputerSystem_query = "Select * from Win32_ComputerSystem";
            return _wmi.QueryWMI(CurrentAccount_query);
        }

        public bool IsAdminAccount()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

            return isAdmin;
        }
        public List<Account> GetItems()
        {
            int Account_id = 0;
            List<Account> Account_list = new List<Account>(10);

            foreach (ManagementObject mo in _wmi.GetManagementObjectCollection())
            {
                try
                {
                    Account item = new Account();

                    if (mo["Caption"] != null)
                    {
                        item.Caption = mo["Caption"].ToString();
                        log.InfoFormat("Caption={0}", mo["Caption"].ToString());
                    }

                    if (mo["Description"] != null)
                    {
                        item.Description = mo["Description"].ToString();
                        log.InfoFormat("Description={0}", mo["Description"].ToString());
                    }

                    if (mo["Domain"] != null)
                    {
                        item.Domain = mo["Domain"].ToString();
                        log.InfoFormat("Domain={0}", mo["Domain"].ToString());
                    }

                    if (mo["LocalAccount"] != null)
                    {
                        item.LocalAccount = Convert.ToBoolean(mo["LocalAccount"]);
                        log.InfoFormat("LocalAccount={0}", mo["LocalAccount"].ToString());
                    }

                    if (mo["Name"] != null)
                    {
                        item.Name = mo["Name"].ToString();
                        log.InfoFormat("Name={0}", mo["Name"].ToString());
                    }

                    if (mo["SID"] != null)
                    {
                        item.SID = mo["SID"].ToString();
                        log.InfoFormat("SID={0}", mo["SID"].ToString());
                    }

                    if (mo["Status"] != null)
                    {
                        item.Status = mo["Status"].ToString();
                        log.InfoFormat("Status={0}", mo["Status"].ToString());
                    }

                    if (mo["SIDType"] != null)
                    {
                        item.Name = mo["SIDType"].ToString();
                        log.InfoFormat("{0}, SIDType={1}", item.Name, mo["SIDType"].ToString());
                    }

                    Account_list.Add(item);
                    Account_id++;
                }
                catch (Exception)
                {
                    Account item = new Account();
                    item.Name = "empty";
                    item.Id = Account_id;
                    Account_list.Add(item);
                }
            }

            return Account_list.ToList(); ;
        }
    }
}
