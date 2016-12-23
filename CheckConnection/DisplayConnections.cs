﻿using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using SQLite;
using System.Linq;
using System;

using CheckConnection.Methods;
using CheckConnection.Model;

using Ninject;
using Ninject.Parameters;
using log4net;
using System.Activities;
using System.Threading;


namespace CheckConnection
{
    public partial class DisplayConnections : Form//WithLog
    {
        //delegate void SetComboBoxCellType(int iRowIndex);                     
        private bool FormLoadComplete = false;
        private bool IsAdminAccount = false;
        const string NotAdmin = @"Изменения в настройки сетевых подключений могут вносить только пользователи из группы 'Администраторы'.";
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int HistorypageSize = Properties.Settings.Default.HistoryPageSize;//10;

        private SQLiteConnection sqlconn;

        private IConnectionManager connmgr;
        private IDNSManager dnsmgr;
        private IGatewayManager gatewaymgr;
        private IWMIConnectionManager cpmgr;

        public DisplayConnections( )
        {
            InitializeComponent();
                                    
            HistorybindingNavigator.BindingSource = HistorybindingSource;
        
            WMIAccountManager wmiacc = new WMIAccountManager();
            if ((IsAdminAccount = wmiacc.IsAdminAccount()) == true)
                this.Text += " (Администратор)";

            SetToolStripTitles();

            string conn_string;
            if (Properties.Settings.Default.DBConnectionString == "Connections.db")
            {
                StringBuilder sb = new StringBuilder(System.IO.Path.GetTempPath());
                sb.Append( System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().ProcessName) );
                conn_string = sb.ToString();

                // Determine whether the directory exists.
                if (!Directory.Exists(conn_string))
                {
                    try
                    {
                        DirectoryInfo di = Directory.CreateDirectory(conn_string);
                    }
                    catch (Exception e)
                    {
                        string CantMakeDir = e.Message + Environment.NewLine + conn_string;
                        log.Info(CantMakeDir);
                        MessageBox.Show(CantMakeDir, "Ошибка",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
                sb.Append("\\");
                sb.Append(Properties.Settings.Default.DBConnectionString);

                conn_string = sb.ToString();
            }
            else
            {
                conn_string = Properties.Settings.Default.DBConnectionString;
            }

            try
            {
                sqlconn = new SQLiteConnection(conn_string, true);
            }
            catch(Exception e)
            {
                string CantMakeDir = "Невозможно открыть файл " + Environment.NewLine + conn_string;
                log.Info(CantMakeDir);
                MessageBox.Show(CantMakeDir, "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            //Set the parameter for Ninject
            IParameter parameter = new ConstructorArgument("conn", sqlconn);

            connmgr = Common.NinjectProgram.Kernel.Get<IConnectionManager>(parameter);
            dnsmgr = Common.NinjectProgram.Kernel.Get<IDNSManager>(parameter);           
            gatewaymgr = Common.NinjectProgram.Kernel.Get<IGatewayManager>(parameter);

            cpmgr = Common.NinjectProgram.Kernel.Get<IWMIConnectionManager>();
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
            List<Connection> connlist = cpmgr.GetItems();

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

            if ( (sqlconn!= null)&&
                 (File.Exists(sqlconn.DatabasePath))//&&
                 //(WinObjMethods.HasWritePermission( sqlconn.DatabasePath.Substring(0, sqlconn.DatabasePath.LastIndexOf("\\")) )) 
               )
            {
                List<Connection> connparam = cpmgr.GetItems().Where(p => p.Ip_Address_v4 != null).ToList();

                if (connparam.Count > 0)
                {
                    foreach (Connection conn in connparam)
                    {
                        sqlconn.RunInTransaction(() =>
                        {
                            int ret = connmgr.SaveConnection(conn);
                            if (ret > 0)
                            {
                                conn.Id = connmgr.GetLastInsertRowId();

                                if (conn.DNS_list != null)
                                {
                                    foreach (DNS dns in conn.DNS_list)
                                        dns.Connection_Id = conn.Id;
                                    dnsmgr.SaveDNSs(conn.DNS_list);
                                }

                                if (conn.Gateway_list != null)
                                {
                                    foreach (Gateway gtw in conn.Gateway_list)
                                        gtw.Connection_Id = conn.Id;
                                    gatewaymgr.SaveGateways(conn.Gateway_list);
                                }
                            }
                            else
                            {
                                log.Info("Ошибка сохранения");
                            }
                        });
                    }

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
            const string NoConn = @"Соединение с таким наименованием отсутствует.";

            if (!IsAdminAccount)
            {
                log.Info(NotAdmin);
                MessageBox.Show(NotAdmin, "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                int selectedrow = WinObjMethods.GetSelectedRow(ConnectionsdataGridView);

                if (ConnectionsdataGridView.Rows[selectedrow].Cells["Name"].Value != null)
                {
                    string Name = ConnectionsdataGridView.Rows[selectedrow].Cells["Name"].Value.ToString();
                    var ChangeConnectionForm = new ChangeConnectionForm(/*wmi, */Name);

                    ChangeConnectionForm.StartPosition = FormStartPosition.CenterScreen;
                    ChangeConnectionForm.ShowDialog();
                }
                else
                {
                    log.Info(NoConn);
                    MessageBox.Show(NoConn, "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButtonRefresh_Click(object sender, System.EventArgs e)
        {
            cpmgr = Common.NinjectProgram.Kernel.Get<IWMIConnectionManager>();
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
            const string NoInfo = @"Отсутствует информация для восстановления параметров соединения.";
            if (!IsAdminAccount)
            {
                log.Info(NotAdmin);
                MessageBox.Show(NotAdmin, "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {

                int selectedRow = WinObjMethods.GetSelectedRow(ConnectionsdataGridView);
                string Name = ConnectionsdataGridView.Rows[selectedRow].Cells["Name"].Value.ToString();

                Connection conn = cpmgr.GetItem(HistorydataGridView);
                if (conn != null)
                {
                    //Прописываем название подключения, для которого изменяются параметы
                    conn.Name = Name;
                    var ChangeConnectionForm = new ChangeConnectionForm(/*wmi,*/ conn);

                    ChangeConnectionForm.StartPosition = FormStartPosition.CenterScreen;
                    ChangeConnectionForm.Show();
                }
                else
                {
                    log.Info(NotAdmin);
                    MessageBox.Show(NoInfo, "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
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

        private void toolStripButtonRenewDHCP_Click(object sender, EventArgs e)
        {
            if (!IsAdminAccount)
            {
                log.Info(NotAdmin);
                MessageBox.Show(NotAdmin, "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                log.Info("Before toolStripButtonRenewDHCP_Click");
                string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");

                IMObjectManager objMO = new MObjectManager(new WMIConnectionManager().mo_repo.GetItem(p => p.Properties["Description"].Value.ToString() == name));
                objMO.RenewDHCPLease();
                log.Info("Before toolStripButtonRenewDHCP_Click");
            }
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
            Connection conn = cpmgr.GetItem(ConnectionsdataGridView);
            if (conn.Ip_Address_v4 != null)
            {
                AnalyzeForm analyze = new AnalyzeForm(conn);
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

        private void SetToolStripTitles()
        {
            PingtoolStripButton.Text = "Ping";
            TracerttoolStripButton.Text = "Tracert";
            ComparetoolStripButton.Text = "Сравнить" + Environment.NewLine + "параметры" + Environment.NewLine + "подключения";
            ViewComparetoolStripButton.Text = "Табличное" + Environment.NewLine + "сравнение";
            toolStripButtonChangeConnection.Text = "Изменить"+ Environment.NewLine + "параметры"+ Environment.NewLine + "подключения";
            toolStripButtonRestore.Text = "Восстановить" + Environment.NewLine + "параметры" + Environment.NewLine + "подключения"; ;
            toolStripButtonRenewDHCP.Text = "Обновить" + Environment.NewLine + "ip-адрес";
            toolStripButtonRefresh.Text = "Обновить";
            toolStripButtonAnalyze.Text = "Анализ" + Environment.NewLine + "подключения";
            if(!IsAdminAccount)
            {
                toolStripButtonChangeConnection.Enabled = false;
                toolStripButtonRestore.Enabled = false;
                toolStripButtonRenewDHCP.Enabled = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing && (sqlconn != null))
            {
                sqlconn.Dispose();
            }
            base.Dispose(disposing);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Activity wf = new Workflow.Flowchart.CheckConnection();
            //IDictionary<string, object> outputs =  WorkflowInvoker.Invoke(wf);
            //WorkflowLib.WorkFlowApp.Run();

            string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");

            IWMINetworkAdapterManager namgr = new WMINetworkAdapterManager();

            IMObjectManager objMOConnection = new MObjectManager(new WMIConnectionManager().mo_repo.GetItem(p => p.Properties["Description"].Value.ToString() == name));

            IMObjectManager objMONetAdapter = new MObjectManager(new WMINetworkAdapterManager().mo_repo.GetItem(p => p.Properties["Name"].Value.ToString() == name));

            if(!objMOConnection.IpEnabled)
                objMONetAdapter.EnableAdapter();

        }
    }
}
