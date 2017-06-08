using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace PingLib.Methods
{
    public class PingResultRepo:IPingResultRepo
    {
        private Ping pingSender;
        private PingOptions options;
        public event PingCompletedEventHandler PingCompleted;

        public PingResultRepo()
        {
            pingSender = new Ping();
            options = new PingOptions();
            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);            
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
        public void GetPingAsync(string destination)
        {
            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            
            pingSender.SendAsync(destination, timeout, buffer, options);
        }
        void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            var res = e.Reply;
            PingCompleted(this, e);
        }
    }
}
