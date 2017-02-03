using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using PingLib.Methods;

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

        private void startPing_Click(object sender, EventArgs e)
        {
            string strHostName = destination.Text;
            if (String.IsNullOrEmpty(strHostName))
            {
                strHostName = "localhost";
            }
            try
            {
                PingResultRepo pm = new PingResultRepo();
                PingReply reply = pm.GetPing(strHostName);
                if (reply.Status == IPStatus.Success)
                {
                    ListViewItem item = pingList.Items.Add(reply.Address.ToString());
                    item.SubItems.Add((item.Index + 1).ToString());
                    ListViewItem.ListViewSubItem hostNameItem = item.SubItems.Add(strHostName);
                    item.SubItems.Add(reply.Status == IPStatus.Success ? reply.RoundtripTime.ToString() : "*");
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сетевого соединениния");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Ошибка сетевого соединениния");
            }
        }

        //private void startPing_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        tracert.HostNameOrAddress = destination.Text;
        //        //routeList.Items.Clear();
        //        tracert.Trace();
        //        startPing.Enabled = false;
        //    }
        //    catch (SocketException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка сетевого соединениния");
        //    }
        //}             

        private void tracert_Done(object sender, EventArgs e)
        {
            int cnt = tracert.Nodes.Length-1;

            ListViewItem item = pingList.Items.Add(tracert.Nodes[cnt].Address.ToString());

            item.SubItems.Add((item.Index + 1).ToString());
            ListViewItem.ListViewSubItem hostNameItem = item.SubItems.Add(tracert.HostNameOrAddress);
            item.SubItems.Add(tracert.Nodes[cnt].Status == IPStatus.Success ? tracert.Nodes[cnt].RoundTripTime.ToString() : "*");

            startPing.Enabled = true;
        }
    }
}
