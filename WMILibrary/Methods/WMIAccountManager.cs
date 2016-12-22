using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Principal;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{
    public class WMIAccountManager:ClassWithLog
    {                
        public WMIAccountManager()
        {            
        }
        public bool IsAdminAccount()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

            return isAdmin;
        }
    }
}
