using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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
            try
            {                
                PingMethods pm = new PingMethods();
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
    }
}
