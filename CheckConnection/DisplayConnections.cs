using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using SQLite;
using System.Linq;
using System;
using CheckConnection.Methods;
using CheckConnection.Model;
using log4net;

namespace CheckConnection
{
    public partial class DisplayConnections : Form
    {
        //delegate void SetComboBoxCellType(int iRowIndex);        
        private WMIInterface wmi;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool FormLoadComplete = false;

        private int HistorypageSize = Properties.Settings.Default.HistoryPageSize;//10;

        private SQLiteConnection sqlconn;
        private ConnectionManager connmgr;
        private DNSManager dnsmgr;
        private GatewayManager gatewaymgr;

        public DisplayConnections( WMIInterface wmiparam)
        {
            wmi = wmiparam;
            InitializeComponent();

            HistorybindingNavigator.BindingSource = HistorybindingSource;

            string conn_string = Properties.Settings.Default.DBConnectionString;
            sqlconn = new SQLiteConnection(conn_string, true);

            connmgr = new ConnectionManager(sqlconn);
            dnsmgr = new DNSManager(sqlconn);
            gatewaymgr = new GatewayManager(sqlconn);
        }

        private void DisplayConnections_Load(object sender, System.EventArgs e)
        {
            ConnectionsdataGridView.Name = WinObjMethods.ConnGridName;
            WinObjMethods.AddColumn(ref ConnectionsdataGridView);

            BindConnectionGrid();

            WinObjMethods.AddColumn(ref HistorydataGridView);
            
            int rowcnt = 0;
            string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");

            if (!String.IsNullOrEmpty(name))
            {
                //Get count
                rowcnt = connmgr.GetConnectionsAmountByName(name);
            }
            if (rowcnt > 0)
            {
                HistorybindingSource.DataSource = new PageOffsetList(rowcnt);
                HistorybindingSource.MoveFirst();
                BindHistoryGrid(sqlconn);
            }

            FormLoadComplete = true;
        }       
   
        private void BindConnectionGrid()
        {
            WMIConnectionManager wconnmgr = new WMIConnectionManager(wmi);
            List<Connection> connlist = wconnmgr.GetItems();

            if (connlist.Count > 0)
            {
                var bindsList = new BindingList<Connection>(connlist);
                //Bind BindingList directly to the DataGrid
                var source = new BindingSource(bindsList, null);
                ConnectionsdataGridView.DataSource = source;
            }
        }              

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            ConnectionParamManager cpmgr = new ConnectionParamManager(wmi);
            List<ConnectionParam> connparam = cpmgr.GetItems().Where(p => p.Connection.Ip_Address_v4 != null).ToList();

            if (connparam.Count > 0)
            {
                foreach (ConnectionParam conn in connparam)
                {
                    sqlconn.RunInTransaction(() =>
                    {

                        int ret = connmgr.SaveConnection(conn.Connection);
                        conn.Connection.Id = connmgr.GetLastInsertRowId();

                        if (conn.DNS_list != null)
                        {
                            foreach (DNS dns in conn.DNS_list)
                                dns.Connection_Id = conn.Connection.Id;
                            dnsmgr.SaveDNSs(conn.DNS_list);
                        }

                        if (conn.Gateway_list != null)
                        {
                            foreach (Gateway gtw in conn.Gateway_list)
                                gtw.Connection_Id = conn.Connection.Id;
                            gatewaymgr.SaveGateways(conn.Gateway_list);
                        }
                    });
                }

            }

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
        }
        public void CorrectWindowSize()
        {
            int width = WinObjMethods.CountGridWidth(ConnectionsdataGridView);
            ClientSize = new Size(width, ClientSize.Height);
        }
      
        private void PingtoolStripButton_Click(object sender, System.EventArgs e)
        {
            var PingForm = new PingForm.MainPingForm();
            PingForm.StartPosition=FormStartPosition.CenterScreen;
            PingForm.Show();
        }

        private void TracerttoolStripButton_Click(object sender, System.EventArgs e)
        {            
            List<Tracert> Tracert_list = new List<Tracert>();            

            var TracertForm = new TracertForm.MainForm();
            TracertForm.StartPosition=FormStartPosition.CenterScreen;
            TracertForm.Show();
        }

        private void ComparetoolStripButton_Click(object sender, System.EventArgs e)
        {
            if (HistorydataGridView.DataSource == null)
            {
                var message = MessageBox.Show("Нет данных для сравнения", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else
            {
                int rowIndex = -1;
                int count = 0;
                if (CheckSelectRow(ref rowIndex))
                {
                    for (int i = 1; i < ConnectionsdataGridView.ColumnCount; i++)
                    {
                        if ((HistorydataGridView.Rows[rowIndex].Cells[i].Value != null)&&(ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Value != null))
                        {
                            if (
                                !ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Value.ToString()
                                    .Equals(HistorydataGridView.Rows[rowIndex].Cells[i].Value.ToString()))
                            {
                                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = Color.LightGray;
                                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = Color.Red;
                            }
                            else
                            {
                                count++;
                            }
                        }
                        else
                        {
                            if (ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Value == null)
                            {
                                count++;
                            }
                            else
                            {
                                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = Color.LightGray;
                                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = Color.Red;
                            }
                        }
                    }
                }
                if (count == (ConnectionsdataGridView.ColumnCount-1))
                {
                    for (int i = 1; i < ConnectionsdataGridView.ColumnCount; i++)
                    {
                        ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = ConnectionsdataGridView.DefaultCellStyle.SelectionBackColor;
                        ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = ConnectionsdataGridView.DefaultCellStyle.SelectionForeColor;
                    }

                    var message = MessageBox.Show("Параметры подключений совпадают", "Проверка совпадений", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ViewComparetoolStripButton_Click(object sender, System.EventArgs e)
        {
            if (HistorydataGridView.DataSource == null)
            {
                var message = MessageBox.Show("Нет данных для сравнения", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                
                int rowIndex = -1;
                if (CheckSelectRow(ref rowIndex))
                {
                    CompareConnections compareForm = new CompareConnections();
                    compareForm.StartPosition=FormStartPosition.CenterScreen;
                    compareForm.NewDataTable(ConnectionsdataGridView, HistorydataGridView.Rows[rowIndex]);
                    compareForm.Show();
                }
            }
        }

        private bool CheckSelectRow(ref int rowSelectIndex)
        {
            log.Info("HistorydataGridView.SelectedRows.Count="+ HistorydataGridView.SelectedRows.Count.ToString());

            if (HistorydataGridView.SelectedRows.Count == 0)
            {
                log.Info("HistorydataGridView.SelectedCells[0].RowIndex=" + HistorydataGridView.SelectedCells[0].RowIndex.ToString());
                int rowIndex = HistorydataGridView.SelectedCells[0].RowIndex;
                int count = 0;
                foreach (DataGridViewCell cell in HistorydataGridView.SelectedCells)
                {
                    if (cell.RowIndex == rowIndex)
                    {
                        count++;
                    }
                }
                if ((count == HistorydataGridView.SelectedCells.Count) && (count == HistorydataGridView.ColumnCount))
                {
                    rowSelectIndex = rowIndex;
                    return true;
                }
                else
                {
                    var message = MessageBox.Show("Выберите одну строку для сравнения", "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                log.Info("HistorydataGridView.SelectedRows.Count=" + HistorydataGridView.SelectedRows.Count.ToString());

                if (HistorydataGridView.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Выберите только одну строку для сравнения", "Ошибка подключения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    rowSelectIndex = HistorydataGridView.SelectedRows[0].Index;
                    return true;
                }
            }
        }

        private void toolStripButtonChangeConnection_Click(object sender, System.EventArgs e)
        {
            int selectedrow = WinObjMethods.GetSelectedRow(ConnectionsdataGridView);           

            if ( ConnectionsdataGridView.Rows[selectedrow].Cells["Name"].Value != null) {
                string Name = ConnectionsdataGridView.Rows[selectedrow].Cells["Name"].Value.ToString();            
                var ChangeConnectionForm = new ChangeConnectionForm(wmi, Name);

                ChangeConnectionForm.StartPosition = FormStartPosition.CenterScreen;
                ChangeConnectionForm.ShowDialog();
            }
            else
            {
                log.Info("Соединение с таким наименованием отсутствуют");
                MessageBox.Show("Соединение с таким наименованием отсутствуют", "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonRefresh_Click(object sender, System.EventArgs e)
        {
            wmi.GetNetworkDevicesConfig();
            BindConnectionGrid();
        }

        string GetSelectedConnectionParam(DataGridView dgv, string paramname)
        {
            int selectedrow = WinObjMethods.GetSelectedRow(dgv);
            string Name = string.Empty;
            if (ConnectionsdataGridView.Rows[selectedrow].Cells[paramname].Value != null)
                Name = ConnectionsdataGridView.Rows[selectedrow].Cells[paramname].Value.ToString();
            return Name;
        }

        private void ConnectionsdataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //int rowcnt=0;
            //if (FormLoadComplete)
            //{
            //    string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");
            //    if (!String.IsNullOrEmpty(name))
            //    //Get count
            //    rowcnt = db.ReadConnectionHistoryCount(name);
            //    if (rowcnt > 0)
            //    {
            //        HistorybindingSource.DataSource = new PageOffsetList(rowcnt);
            //        HistorybindingSource.MoveFirst();
            //        BindHistoryGrid();
            //    }
            //    for (int i = 1; i < ConnectionsdataGridView.ColumnCount; i++)
            //        {
            //            if (ConnectionsdataGridView.SelectedRows.Count > 0)
            //            {
            //                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = ConnectionsdataGridView.DefaultCellStyle.SelectionBackColor;
            //                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = ConnectionsdataGridView.DefaultCellStyle.SelectionForeColor;
            //            }
            //        }
            //}
        }

        private void toolStripButtonRestore_Click(object sender, EventArgs e)
        {
            int selectedRow = WinObjMethods.GetSelectedRow(ConnectionsdataGridView);
            string Name = ConnectionsdataGridView.Rows[selectedRow].Cells["Name"].Value.ToString();

            ConnectionParamManager connparammgr = new ConnectionParamManager();
            ConnectionParam connparam = connparammgr.GetItem(HistorydataGridView);
            if (connparam != null)
            {
                //Прописываем название подключения, для которого изменяются параметы
                connparam.Connection.Name = Name;              
                var ChangeConnectionForm = new ChangeConnectionForm(wmi, connparam);

                ChangeConnectionForm.StartPosition = FormStartPosition.CenterScreen;
                ChangeConnectionForm.Show();
            }
            else
            {
                MessageBox.Show("Отсутствует информация для восстановления параметров соединения", "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void HistorybindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (FormLoadComplete)
            {
                if ((HistorybindingSource.Current != null)&&((int)HistorybindingSource.Current>0))
                    BindHistoryGrid(sqlconn);
            }
        }

        private void BindHistoryGrid(SQLiteConnection sqlconn)
        {
            //string SelectedConnectionName = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");
            //if (!String.IsNullOrEmpty(SelectedConnectionName))
            //{
                // The desired page has changed, so fetch the page of records using the "Current" offset 
                if (HistorybindingSource.Current != null)
                {
                    int offset = (int)HistorybindingSource.Current;
                    IList<Connection> connlist = connmgr.GetConnections(offset, HistorypageSize);

                    foreach (Connection conn in connlist)
                    {
                        IList<DNS> dnslist = dnsmgr.GetDNSsByConnectionId(conn.Id);
                        IList<Gateway> gtwlist = gatewaymgr.GetGatewaysByConnectionId(conn.Id);

                        if (dnslist.Count > 0)
                        {
                            foreach (DNS dns in dnslist)
                            {
                                conn.DNSServer += dns.DNSServer + "; ";
                            }
                            if (conn.DNSServer.Length > 2)
                                conn.DNSServer = conn.DNSServer.Substring(0, conn.DNSServer.Length - 2);
                        }
                        if (gtwlist.Count > 0)
                        {
                            foreach (Gateway gtw in gtwlist)
                            {
                                conn.IPGateway += gtw.IPGateway + "; ";
                            }
                            if (conn.IPGateway.Length > 2)
                                conn.IPGateway = conn.IPGateway.Substring(0, conn.IPGateway.Length - 2);
                        }
                    }
                    HistorydataGridView.DataSource = connlist;

                    WinObjMethods.ResizeGrid(ref ConnectionsdataGridView);
                    CorrectWindowSize();
                }
            //}
        }

        private void ConnectionsdataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //int rowcnt = 0;
            //if (FormLoadComplete)
            //{
            //    string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");
            //    if (!String.IsNullOrEmpty(name))
            //    {
            //        //Get count
            //        rowcnt = connmgr.GetConnectionsAmountByName(name);
            //    }
            //    if (rowcnt > 0)
            //    {
            //        HistorybindingSource.DataSource = new PageOffsetList(rowcnt);
            //        HistorybindingSource.MoveFirst();
            //        BindHistoryGrid(sqlconn);
            //    }
            //    for (int i = 1; i < ConnectionsdataGridView.ColumnCount; i++)
            //    {
            //        if (ConnectionsdataGridView.SelectedRows.Count > 0)
            //        {
            //            ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = ConnectionsdataGridView.DefaultCellStyle.SelectionBackColor;
            //            ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = ConnectionsdataGridView.DefaultCellStyle.SelectionForeColor;
            //        }
            //    }
            //}
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            if (FormLoadComplete)
            {
                if (HistorybindingSource.Current != null) 
                    BindHistoryGrid(sqlconn);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Before copyToolStripMenuItem_Click");
            string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");
            MObject objMO = new MObject(wmi.GetManagementObject(name));
            objMO.Clone();
            objMO["Description"] = "Clone 1";
            objMO.Put();

            log.Info("After copyToolStripMenuItem_Click");
        }


        private void toolStripButtonRenewDHCP_Click(object sender, EventArgs e)
        {
            log.Info("Before toolStripButtonRenewDHCP_Click");
            string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");
            MObject objMO = new MObject(wmi.GetManagementObject(name));
            objMO.RenewDHCPLease();
            log.Info("Before toolStripButtonRenewDHCP_Click");
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            if (FormLoadComplete)
            {
                if (HistorybindingSource.Current != null)
                    BindHistoryGrid(sqlconn);
            }
        }

        private void toolStripButtonAnalyze_Click(object sender, EventArgs e)
        {
            //string RouterDeafultIpAddress = Properties.Settings.Default.RouterDeafultIpAddress;
            //string ProviderDefaultAddress = Properties.Settings.Default.ProviderDefaultAddress;
            //string IPGateway = GetSelectedConnectionParam(ConnectionsdataGridView, "IPGateway");

            ConnectionParamManager connparammgr = new ConnectionParamManager();
            ConnectionParam connparam = connparammgr.GetItem(ConnectionsdataGridView);
            if (connparam.Connection.Ip_Address_v4 != null)
            {
                AnalyzeForm analyze = new AnalyzeForm(connparam);
                analyze.StartPosition = FormStartPosition.CenterScreen;
                analyze.ShowDialog();
            }
            else
            {
                MessageBox.Show("Допускается анализ только подключений с ip-адресом", "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

    }
}
