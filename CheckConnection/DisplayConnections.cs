﻿using CheckConnection.Methods;
using CheckConnection.Model;
using Common;
using Ninject.Parameters;
using SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace CheckConnection
{
    public partial class DisplayConnections : FormWithLogger<DisplayConnections>
    {
        //delegate void SetComboBoxCellType(int iRowIndex);

        private bool FormLoadComplete = false;
        private bool IsAdminAccount = false;
        const string NotAdmin = @"Изменения в настройки сетевых подключений могут вносить только пользователи из группы 'Администраторы'.";        

        private int HistorypageSize = Properties.Settings.Default.HistoryPageSize;//10;

        private SQLiteConnection sqlconn;

        private IConnectionManager connmgr;
        private IDNSManager dnsmgr;
        private IGatewayManager gatewaymgr;        
        private IWMINetworkAdapterManager namgr;        
        private readonly uint ExpiryPeriod = 30;
        public int Mode { get; set; }

        public DisplayConnections( )
        {
            InitializeComponent();

            log.Info("DisplayConnections, before");
            HistorybindingNavigator.BindingSource = HistorybindingSource;
        
            WMIAccountManager wmiacc = new WMIAccountManager();
            if ((IsAdminAccount = wmiacc.IsAdminAccount()) == true)
                this.Text += " (Администратор)";

            SetToolStripTitles();

            string conn_string = WinObjMethods.GetDBConnectionString(log);
            
            try
            {
                sqlconn = new SQLiteConnection(conn_string, true);
            }
            catch(Exception e)
            {
                string CantMakeDir = "Невозможно открыть файл " + Environment.NewLine + conn_string;
                log.Error(CantMakeDir,e);
                MessageBox.Show(CantMakeDir, "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            //Set the parameter for Ninject
            IParameter parameter = new ConstructorArgument("conn", sqlconn);

            log.Info("DisplayConnections, before Ninject");

            log.Info("DisplayConnections, before IWMINetworkAdapterManager");
            Func<IWMINetworkAdapterManager> dwmi = ReadWMIInfo;
            //Invoke our method in another thread
            IAsyncResult asyncWMI = dwmi.BeginInvoke(new AsyncCallback(WMICallBack), null);
            log.Info("DisplayConnections, after IWMINetworkAdapterManager");
            
            Func<IParameter, IConnectionManager> dconnection = new Func<IParameter, IConnectionManager>(ReadConnectionInfo);
            //Invoke our method in another thread
            IAsyncResult asyncConnection = dconnection.BeginInvoke(parameter, new AsyncCallback(ConnectionCallBack), null);
            log.Info("DisplayConnections, after Ninject IConnectionManager");

            Func<IParameter, IDNSManager> ddns = new Func<IParameter, IDNSManager>(ReadDNSInfo);
            //Invoke our method in another thread
            IAsyncResult asyncDNS = ddns.BeginInvoke(parameter, new AsyncCallback(DNSCallBack), null);
            log.Info("DisplayConnections, after Ninject IDNSManager");

            Func<IParameter, IGatewayManager> dgateway = new Func<IParameter,IGatewayManager>(ReadGatewayInfo);
            //Invoke our method in another thread
            IAsyncResult asyncGateway = dgateway.BeginInvoke(parameter, new AsyncCallback(GatewayCallBack), null);
            log.Info("DisplayConnections, after Ninject IGatewayManager");

            asyncConnection.AsyncWaitHandle.WaitOne();
            asyncGateway.AsyncWaitHandle.WaitOne();
            asyncDNS.AsyncWaitHandle.WaitOne();

            //usermgr = Common.NinjectProgram.Kernel.Get<IUserManager>(parameter);
            //connmgr = Common.NinjectProgram.Kernel.Get<IConnectionManager>(parameter);
            //log.Info("DisplayConnections, after Ninject IConnectionManager");
            //dnsmgr = Common.NinjectProgram.Kernel.Get<IDNSManager>(parameter);
            //log.Info("DisplayConnections, before Ninject IDNSManager");
            //gatewaymgr = Common.NinjectProgram.Kernel.Get<IGatewayManager>(parameter);
            //log.Info("DisplayConnections, after Ninject IGatewayManager");
            //log.Info("DisplayConnections, before IWMINetworkAdapterManager");

            asyncWMI.AsyncWaitHandle.WaitOne();

            if(connmgr?.GetDiffInDays() > ExpiryPeriod)
            {
                string mess = "Превышено максимальное число запусков.Приложение будет закрыто.";
                log.Info(mess);
                MessageBox.Show(mess, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(1);
                }
            }
        }

        void WMICallBack(IAsyncResult async)
        {
            AsyncResult ar  = (AsyncResult)async;
            Func<IWMINetworkAdapterManager> dwmi = (Func<IWMINetworkAdapterManager>)ar.AsyncDelegate;
            namgr = dwmi.EndInvoke(async);
            log.Info("DisplayConnections, complete IWMINetworkAdapterManager");
        }
        //A method to be invoke by the delegate
        IWMINetworkAdapterManager ReadWMIInfo()
        {
            IWMINetworkAdapterManager namgr = Common.IocKernel.Get<IWMINetworkAdapterManager>();
            return namgr;
        }

        #region not used
        void ConnectionCallBack(IAsyncResult async)
        {
            AsyncResult ar = (AsyncResult)async;
            Func<IParameter, IConnectionManager> dconnection = (Func<IParameter, IConnectionManager>)ar.AsyncDelegate;
            connmgr = dconnection.EndInvoke(async);
            log.Info("DisplayConnections, complete IConnectionManager");
        }
        //A method to be invoke by the delegate
        IConnectionManager ReadConnectionInfo(IParameter parameter)
        {
            IConnectionManager connmgr = Common.IocKernel.Get<IConnectionManager>(parameter);
            return connmgr;
        }

        void DNSCallBack(IAsyncResult async)
        {
            AsyncResult ar = (AsyncResult)async;
            Func<IParameter, IDNSManager> ddns = (Func<IParameter, IDNSManager>)ar.AsyncDelegate;
            dnsmgr = ddns.EndInvoke(async);
            log.Info("DisplayConnections, complete IDNSManager");
        }
        //A method to be invoke by the delegate
        IDNSManager ReadDNSInfo(IParameter parameter)
        {
            IDNSManager dnsmgr = Common.IocKernel.Get<IDNSManager>(parameter);
            return dnsmgr;
        }

        void GatewayCallBack(IAsyncResult async)
        {
            AsyncResult ar = (AsyncResult)async;
            Func<IParameter, IGatewayManager> dgateway = (Func<IParameter, IGatewayManager>)ar.AsyncDelegate;
            gatewaymgr = dgateway.EndInvoke(async);
            log.Info("DisplayConnections, complete IGatewayManager");
        }
        //A method to be invoke by the delegate
        IGatewayManager ReadGatewayInfo(IParameter parameter)
        {
            IGatewayManager gatewaymgr = Common.IocKernel.Get<IGatewayManager>(parameter);
            return gatewaymgr;
        }
        #endregion

        private void DisplayConnections_Load(object sender, System.EventArgs e)
        {
            log.Info("DisplayConnections_Load, before");
            ConnectionsdataGridView.Name = WinObjMethods.ConnGridName;
            WinObjMethods.AddColumn(ref ConnectionsdataGridView);

            BindConnectionGrid();

            WinObjMethods.AddColumn(ref HistorydataGridView);

            if (Mode == 0)
            {
                foreach (var item in toolStrip.Items)
                    (item as ToolStripButton).Visible = false;

                toolStripButtonRepair.Visible = true;
            }

            int rowcnt = connmgr.GetConnectionsAmount();            
            if (rowcnt > 0)
            {
                HistorybindingSource.DataSource = new PageOffsetList(rowcnt);
                HistorybindingSource.MoveFirst();
                BindHistoryGrid();
            }

            FormLoadComplete = true;
            log.Info("DisplayConnections_Load, after");
        }

        private void BindConnectionGrid()
        {            
            List<Connection> connlist = namgr.GetItems(p=>true);

            if (connlist.Count > 0)
            {
                //var bindsList = new BindingList<Connection>(connlist);
                //Bind BindingList directly to the DataGrid
                //var source = new BindingSource(bindsList, null);
                ConnectionGridbindingSource.DataSource = connlist;
                ConnectionsdataGridView.DataSource = ConnectionGridbindingSource;
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
                IEnumerable<Connection> connparam = namgr.GetItems(p=>true).Where(p => p.Ip_Address_v4 != null);

                if (connparam.Count() > 0)
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
            string _ProviderDefaultAddress = Properties.Settings.Default.ProviderDefaultAddress;
            var PingForm = new PingForm.MainPingForm(_ProviderDefaultAddress);
            PingForm.StartPosition=FormStartPosition.CenterScreen;
            PingForm.ShowDialog();
        }

        private void TracerttoolStripButton_Click(object sender, System.EventArgs e)
        {
            string _ProviderDefaultAddress = Properties.Settings.Default.ProviderDefaultAddress;
            var TracertForm = new TracertForm.MainForm(_ProviderDefaultAddress);
            TracertForm.StartPosition=FormStartPosition.CenterScreen;
            TracertForm.ShowDialog();
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
                    compareForm.ShowDialog();
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
                //int selectedrow = WinObjMethods.GetSelectedRow(ConnectionsdataGridView);
                uint Index = (ConnectionGridbindingSource.Current as Connection).Index;
                if (Index > 0)
                {                    
                    var ChangeConnectionForm = new ChangeConnectionForm(Index);
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
            this.Cursor = Cursors.WaitCursor;
            namgr = Common.IocKernel.Get<IWMINetworkAdapterManager>();
            BindConnectionGrid();
            this.Cursor = Cursors.Default;
        }

        //string GetSelectedConnectionParam(DataGridView dgv, string paramname)
        //{
        //    int selectedrow = WinObjMethods.GetSelectedRow(dgv);
        //    log.InfoFormat("WinObjMethods.GetSelectedRow {0},{1}", paramname, selectedrow.ToString());
        //    string Name = string.Empty;
        //    if ((dgv.RowCount>0) &&(dgv.Rows[selectedrow].Cells[paramname].Value != null))
        //        Name = dgv.Rows[selectedrow].Cells[paramname].Value.ToString();
        //    return Name;
        //}

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

                //int selectedRow = WinObjMethods.GetSelectedRow(ConnectionsdataGridView);
                //string Name = ConnectionsdataGridView.Rows[selectedRow].Cells["Name"].Value.ToString();

                Connection conn = HistoryGridbindingSource.Current as Connection;

                //Connection conn = namgr.GetItem(HistorydataGridView);

                if (conn != null)
                {
                    //Прописываем название подключения, для которого изменяются параметы
                    //conn.Name = Name;
                    var ChangeConnectionForm = new ChangeConnectionForm(/*wmi,*/ conn);

                    ChangeConnectionForm.StartPosition = FormStartPosition.CenterScreen;
                    ChangeConnectionForm.ShowDialog();
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
                    BindHistoryGrid();
            }
        }

        private void BindHistoryGrid()
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
                        conn.DNS_list = dnsmgr.GetDNSsByConnectionId(conn.Id).ToList();
                        conn.Gateway_list = gatewaymgr.GetGatewaysByConnectionId(conn.Id).ToList();
                    }

                    //var bindsList = new BindingList<Connection>(connlist);
                    //Bind BindingList directly to the DataGrid
                    HistoryGridbindingSource.DataSource = connlist;
                    HistorydataGridView.DataSource = HistoryGridbindingSource;

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
                    BindHistoryGrid();
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
                
                uint Index = (ConnectionGridbindingSource.Current as Connection).Index;
                if (Index > 0)
                {
                    IMObjectManager objMO = new MObjectManager(new WMIConnectionManager().mo_repo.GetItem(p => Convert.ToUInt32(p.Properties["Index"].Value) == Index));
                    // objMO.RenewDHCPLease();
                    Func<int> drenewdhcp = objMO.RenewDHCPLease;
                    //Invoke our method in another thread
                    IAsyncResult async_result = drenewdhcp.BeginInvoke(null, null);
                    int res = drenewdhcp.EndInvoke(async_result);
                }
                log.Info("Before toolStripButtonRenewDHCP_Click");
            }
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            if (FormLoadComplete)
            {
                if (HistorybindingSource.Current != null)
                    BindHistoryGrid();
            }
        }

        private void toolStripButtonAnalyze_Click(object sender, EventArgs e)
        {
            Connection conn = namgr.GetItem(ConnectionsdataGridView);
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
            ComparetoolStripButton.Text = "Сравнить" + Environment.NewLine + "параметры" /*+ Environment.NewLine + "подключения"*/;
            ViewComparetoolStripButton.Text = "Табличное" + Environment.NewLine + "сравнение";
            toolStripButtonChangeConnection.Text = "Изменить"+ Environment.NewLine + "параметры"/*+ Environment.NewLine + "подключения"*/;
            toolStripButtonRestore.Text = "Восстановить" + Environment.NewLine + "параметры" /*+ Environment.NewLine + "подключения"*/;
            toolStripButtonRenewDHCP.Text = "Обновить" + Environment.NewLine + "ip-адрес";
            toolStripButtonRefresh.Text = "Обновить";
            toolStripButtonAnalyze.Text = "Анализ" + Environment.NewLine + "подключения";
            toolStripButtonRepair.Text = "Восстановление" + Environment.NewLine + "подключения";
            if (!IsAdminAccount)
            {
                toolStripButtonChangeConnection.Enabled = false;
                toolStripButtonRestore.Enabled = false;
                toolStripButtonRenewDHCP.Enabled = false;
                toolStripButtonRepair.Enabled = false;
            }
        }

        protected override void Dispose(bool disposing)
        {            
            
            ConnectionsdataGridView.DataSource = null;
            HistorydataGridView.DataSource = null;

            ConnectionGridbindingSource.DataSource = null;
            HistoryGridbindingSource.DataSource = null;

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing && (sqlconn != null))
            {
                sqlconn?.Close();
                sqlconn.Dispose();
            }
            base.Dispose(disposing);
        }

        private void toolStripRepairButton_Click(object sender, EventArgs e)
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

                ///Activity wf = new WorkFlowApp. .Flowchart.CheckConnection();
                //IDictionary<string, object> outputs = WorkflowInvoker.Invoke(wf);

                //string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");

                string[] log = null;//{ "111", "222" };
                RepairForm repair = new RepairForm(log);
                repair.ShowDialog();
            }
        }

        #region
        //----------------------------------------------------------------------------------------
        //log.Info("start test");
        //SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
        //ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
        //foreach (ManagementObject item in searchProcedure.Get())
        //{
        //    if (((string)item["NetConnectionId"]).IndexOf("Подключение по локальной сети") != -1)
        //    {
        //        log.InfoFormat("find test {0}", (string)item["NetConnectionId"]);
        //        try
        //        {
        //            item.InvokeMethod("Disable", null);
        //        }
        //        catch (Exception ex)
        //        {
        //            log.Error("error test", ex);
        //        }
        //    }
        //}
        //log.Info("end test");
        //----------------------------------------------------------------------------------------
        //log.Info("start test");
        //SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
        //ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
        //foreach (ManagementObject item in searchProcedure.Get())
        //{
        //    if (((string)item["NetConnectionId"]).IndexOf("Подключение по локальной сети") != -1)
        //    {
        //        log.InfoFormat("find test {0}", (string)item["NetConnectionId"]);
        //        try
        //        {
        //            CNICManager cnic = new CNICManager();
        //            cnic.DisableConnection((string)item["NetConnectionId"]);
        //        }
        //        catch (Exception ex)
        //        {
        //            log.Error("error test", ex);
        //        }
        //    }
        //}
        //log.Info("end test");

        //----------------------------------------------------------------------------------------            
        //string interfaceName = GetSelectedConnectionParam(ConnectionsdataGridView, "NetConnectionID");
        //System.Diagnostics.ProcessStartInfo psi =
        //    new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
        //System.Diagnostics.Process p = new System.Diagnostics.Process();
        //p.StartInfo = psi;
        //p.Start();
        //----------------------------------------------------------------------------------------

        //void Enable(string interfaceName)
        //{
        //System.Diagnostics.ProcessStartInfo psi =
        //       new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" enable");
        //System.Diagnostics.Process p = new System.Diagnostics.Process();
        //p.StartInfo = psi;
        //p.Start();
        //        }

        //void Disable(string interfaceName)
        //{
        //            System.Diagnostics.ProcessStartInfo psi =
        //                new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
        //            System.Diagnostics.Process p = new System.Diagnostics.Process();
        //            p.StartInfo = psi;
        //            p.Start();
        //        }


        //IWMINetworkAdapterManager namgr = new WMINetworkAdapterManager();

        //IMObjectManager objMOConnection = new MObjectManager(new WMIConnectionManager().mo_repo.GetItem(p => p.Properties["Description"].Value.ToString() == name));

        //IMObjectManager objMONetAdapter = new MObjectManager(new WMINetworkAdapterManager().mo_repo.GetItem(p => p.Properties["Name"].Value.ToString() == name));

        //if(!objMOConnection.IpEnabled)
        //    objMONetAdapter.EnableAdapter();
        #endregion

        private void DisplayConnections_Shown(object sender, EventArgs e)
        {
            string mess;
            if (IsAdminAccount)            
              mess = "Вам доступно редактирование параметров подключения. Изменения параметров подключения будут сохранены в реестре Windows.";
            else
              mess = "Для редактирования параметров подключения требуются права Администратора. Вам доступен только просмотр параметров подключения.";

            log.Info(mess);
            MessageBox.Show(mess, "", MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
        }
    }
}
