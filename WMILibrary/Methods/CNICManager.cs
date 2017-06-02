using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Common;

namespace CheckConnection.Methods
{
    public class CNICManager: ExtProgrammManager
    {
        public CNICManager():base("cmd", "/c "+".\\OS\\WinXP\\CNic.exe ")
        {
        }
        public int EnableConnection(string NetConnectionId)
        {
            log.Info(Start("\"" + NetConnectionId + "\"" + " -c"));
            return 1;
        }
        public int DisableConnection(string NetConnectionId)
        {
            log.Info(Start("\"" + NetConnectionId + "\"" + " -d"));
            return 1;
        }       

    }
}
