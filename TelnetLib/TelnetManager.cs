using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;
using System.Text.RegularExpressions;

namespace TelnetLib
{
    public class TelnetManager : ClassWithLog
    {
        private TelnetConnection _tc;
        private string _s = null;

        public TelnetManager(string hostname, int port) {
            try
            {
                _tc = new TelnetConnection(hostname, port);

            }
            catch(Exception ex)
            {
                _s = ex.Message;
                log.Error("Ошибка подключения к хосту", ex);
            }
        }

        public string GetResult()
        {   
            if(_tc!= null) {
                if (!_tc.IsConnected)
                    return _s;

                _s = _tc.Read();
                _s = Regex.Replace(_s, @"\x1b\[([0-9,A-Z]{1,2}(;[0-9]{1,2})?(;[0-9]{3})?)?[m|K]?", "");
            }
            return _s;
        }
    }
}
