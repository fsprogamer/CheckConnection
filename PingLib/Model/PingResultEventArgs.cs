using PingLib.Model;
using System;

namespace PingLib.Model
{
    public class PingResultEventArgs: EventArgs
    {
        public PingResult PingResult { get; set; }
        public PingResultEventArgs(PingResult pingResult)
        {
            PingResult = pingResult;
        }
    }
}
