using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace CheckConnection.Methods
{
    public interface IWMIManagementObjectRepo: IGenericWMIRepo<ManagementObject>
    {
    }
}
