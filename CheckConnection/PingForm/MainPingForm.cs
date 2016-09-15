using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using PingForm.Methods;

namespace PingForm
{
    public partial class MainPingForm : Form
    {
        public MainPingForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void startTrace_Click(object sender, EventArgs e)
        {
            string strHostName = destination.Text;
            if (String.IsNullOrEmpty(strHostName))
            {
                strHostName = "localhost";
            }

            PingMethods pm = new PingMethods();
            PingReply reply = pm.GetPing(strHostName);

            if (reply.Status == IPStatus.Success)
            {
                //Console.WriteLine("Address: {0}", reply.Address.ToString());
                //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);

                ListViewItem item = pingList.Items.Add(reply.Address.ToString());
                item.SubItems.Add((item.Index + 1).ToString());
                ListViewItem.ListViewSubItem hostNameItem = item.SubItems.Add(strHostName);
                item.SubItems.Add(reply.Status == IPStatus.Success ? reply.RoundtripTime.ToString() : "*");

            }
        }
    }
}
