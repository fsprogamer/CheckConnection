using System;
using System.Net.NetworkInformation;
using System.Text;

namespace PingForm.Methods
{
    public class PingMethods:PingInterface
    {
        private Ping pingSender;
        private PingOptions options;

        public PingMethods()
        {
            pingSender = new Ping();
            options = new PingOptions();
        }

        public PingReply GetPing(string destination)
        {
            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(destination, timeout, buffer, options);
            return reply;
        }
    }
}
