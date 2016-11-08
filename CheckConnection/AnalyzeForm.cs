using System;
using System.Windows.Forms;
using System.Collections.Generic;

using CheckConnection.Methods;
using CheckConnection.Model;

using log4net;

namespace CheckConnection
{
    public partial class AnalyzeForm : Form
    {
        private ConnectionParam _connparam;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public AnalyzeForm(ConnectionParam connparam)
        {
            InitializeComponent();
            _connparam = connparam;
        }

        private void AnalyzeForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            log.InfoFormat("_connparam.Connection.Ip_Address_v4 = {0}", _connparam.Connection.Ip_Address_v4);
            AnalyzeManager analyze = new AnalyzeManager(_connparam.Connection.Ip_Address_v4, _connparam.Connection.DHCP_Enabled);
            log.Info("SetGateway");
            analyze.SetGateway(_connparam.Gateway_list);
            log.Info("SetDNS");
            analyze.SetDNS(_connparam.DNS_list);
            log.Info("StartAnalyze");
            analyze.StartAnalyze();
            
            log.Info("before GetPingResult");
            List<string[]> rows = analyze.GetPingResult();
            foreach (string[] row in rows)
            {
              var listViewItem = new ListViewItem(row);
                if ((row.GetValue(2) != null) && (row.GetValue(2).ToString() == "Успешно"))
                {
                    listViewItem.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    listViewItem.BackColor = System.Drawing.Color.Crimson;
                    listViewItem.ForeColor = System.Drawing.Color.WhiteSmoke;
                }
              listViewResults.Items.Add(listViewItem);
            }
            log.Info("before MakeConclusion");
            listBoxConclusion.DataSource = analyze.MakeConclusion();
            this.Cursor = Cursors.Default;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
