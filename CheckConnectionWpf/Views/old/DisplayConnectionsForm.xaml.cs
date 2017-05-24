using CheckConnection.Model;
using CheckConnectionWpf.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CheckConnectionWpf
{
    public partial class DisplayConnectionsForm : Window, IDisplayConnectionsView
    {
        public event Action ActiveConnectionSelected;
        public event Action AnalyzeButtonClicked;
        public event Action ChangeButtonClicked;
        public event Action CompareButtonClicked;
        public event Action HistoryConnectionSelected;
        public event Action PingButtonClicked;
        public event Action RefreshButtonClicked;
        public event Action RefreshDHCPButtonClicked;
        public event Action RepairButtonClicked;
        public event Action RestoreButtonClicked;
        public event Action TableCompareButtonClicked;
        public event Action TracertButtonClicked;

        public Connection SelectedActiveConnection
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Connection SelectedHistoryConnection
        {
            get
            {
                throw new NotImplementedException();
            }
        }              

        public void LoadActiveConnections(IList<Connection> connections)
        {
            if (connections.Count > 0)
            {
                //ConnectionsdataGridView.ItemsSource = connections;
            }
        }

        public void LoadHistoryConnections(IList<Connection> connections)
        {
            if (connections.Count > 0)
            {
               // HistorydataGridView.ItemsSource = connections;
            }
        }

        public void ShowMessage(string text, string caption, Icons icon)
        {            
            if (icon == Icons.Error)
            {
                 MessageBox.Show(this, text, caption,
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
            }
            else if (icon == Icons.Stop)
            {
                MessageBox.Show(this, text, caption,
                           MessageBoxButton.OK,
                           MessageBoxImage.Stop);
                Application.Current.Shutdown();
            }
        }
    }

    //public partial class DisplayConnectionsFormOld : Window
    //{
    //    private bool FormLoadComplete = false;
    //    private bool IsAdminAccount = false;
    //    const string NotAdmin = @"Изменения в настройки сетевых подключений могут вносить только пользователи из группы 'Администраторы'.";
    //    private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //    private int HistorypageSize = CheckConnectionWpf.Properties.Settings.Default.HistoryPageSize;//10;

    //    private SQLiteConnection sqlconn;

    //    private IConnectionManager connmgr;
    //    private IDNSManager dnsmgr;
    //    private IGatewayManager gatewaymgr;
    //    private IWMINetworkAdapterManager namgr;
    //    private readonly uint ExpiryPeriod = 30;
    //    public int Mode { get; set; }

    //    public DisplayConnectionsFormOld()
    //    {
    //        InitializeComponent();

    //        log.Info("DisplayConnections, before");
    //        //HistorybindingNavigator.BindingSource = HistorybindingSource;

    //        WMIAccountManager wmiacc = new WMIAccountManager();
    //        if ((IsAdminAccount = wmiacc.IsAdminAccount()) == true)
    //            this.Title += " (Администратор)";

    //        string conn_string = WinObjMethods.GetDBConnectionString(log);

    //        try
    //        {
    //            sqlconn = new SQLiteConnection(conn_string, true);
    //        }
    //        catch (Exception e)
    //        {
    //            string CantMakeDir = "Невозможно открыть файл " + Environment.NewLine + conn_string;
    //            log.Error(CantMakeDir, e);
    //            MessageBox.Show(this, CantMakeDir, "Ошибка",
    //                            MessageBoxButton.OK,
    //                            MessageBoxImage.Error);
    //        }

    //        //Set the parameter for Ninject
    //        IParameter parameter = new ConstructorArgument("conn", sqlconn);

    //        log.Info("DisplayConnections, before Ninject");

    //        log.Info("DisplayConnections, before IWMINetworkAdapterManager");
    //        Func<IWMINetworkAdapterManager> dwmi = ReadWMIInfo;
    //        //Invoke our method in another thread
    //        IAsyncResult asyncWMI = dwmi.BeginInvoke(new AsyncCallback(WMICallBack), null);
    //        log.Info("DisplayConnections, after IWMINetworkAdapterManager");

    //        Func<IParameter, IConnectionManager> dconnection = new Func<IParameter, IConnectionManager>(ReadConnectionInfo);
    //        //Invoke our method in another thread
    //        IAsyncResult asyncConnection = dconnection.BeginInvoke(parameter, new AsyncCallback(ConnectionCallBack), null);
    //        log.Info("DisplayConnections, after Ninject IConnectionManager");

    //        Func<IParameter, IDNSManager> ddns = new Func<IParameter, IDNSManager>(ReadDNSInfo);
    //        //Invoke our method in another thread
    //        IAsyncResult asyncDNS = ddns.BeginInvoke(parameter, new AsyncCallback(DNSCallBack), null);
    //        log.Info("DisplayConnections, after Ninject IDNSManager");

    //        Func<IParameter, IGatewayManager> dgateway = new Func<IParameter, IGatewayManager>(ReadGatewayInfo);
    //        //Invoke our method in another thread
    //        IAsyncResult asyncGateway = dgateway.BeginInvoke(parameter, new AsyncCallback(GatewayCallBack), null);
    //        log.Info("DisplayConnections, after Ninject IGatewayManager");

    //        asyncConnection.AsyncWaitHandle.WaitOne();
    //        asyncGateway.AsyncWaitHandle.WaitOne();
    //        asyncDNS.AsyncWaitHandle.WaitOne();

    //        asyncWMI.AsyncWaitHandle.WaitOne();

    //        if (connmgr?.GetDiffInDays() > ExpiryPeriod)
    //        {
    //            string mess = "Превышено максимальное число запусков.Приложение будет закрыто.";
    //            log.Info(mess);
    //            MessageBox.Show(this, mess, "", MessageBoxButton.OK, MessageBoxImage.Stop);
    //            // Wpf app
    //            Application.Current.Shutdown(); ;
    //        }
    //    }

    //    void WMICallBack(IAsyncResult async)
    //    {
    //        AsyncResult ar = (AsyncResult)async;
    //        Func<IWMINetworkAdapterManager> dwmi = (Func<IWMINetworkAdapterManager>)ar.AsyncDelegate;
    //        namgr = dwmi.EndInvoke(async);
    //        log.Info("DisplayConnections, complete IWMINetworkAdapterManager");
    //    }
    //    //A method to be invoke by the delegate
    //    IWMINetworkAdapterManager ReadWMIInfo()
    //    {
    //        IWMINetworkAdapterManager namgr = Common.IocKernel.Get<IWMINetworkAdapterManager>();
    //        return namgr;
    //    }

    //    void ConnectionCallBack(IAsyncResult async)
    //    {
    //        AsyncResult ar = (AsyncResult)async;
    //        Func<IParameter, IConnectionManager> dconnection = (Func<IParameter, IConnectionManager>)ar.AsyncDelegate;
    //        connmgr = dconnection.EndInvoke(async);
    //        log.Info("DisplayConnections, complete IConnectionManager");
    //    }
    //    //A method to be invoke by the delegate
    //    IConnectionManager ReadConnectionInfo(IParameter parameter)
    //    {
    //        IConnectionManager connmgr = Common.IocKernel.Get<IConnectionManager>(parameter);
    //        return connmgr;
    //    }

    //    void DNSCallBack(IAsyncResult async)
    //    {
    //        AsyncResult ar = (AsyncResult)async;
    //        Func<IParameter, IDNSManager> ddns = (Func<IParameter, IDNSManager>)ar.AsyncDelegate;
    //        dnsmgr = ddns.EndInvoke(async);
    //        log.Info("DisplayConnections, complete IDNSManager");
    //    }
    //    //A method to be invoke by the delegate
    //    IDNSManager ReadDNSInfo(IParameter parameter)
    //    {
    //        IDNSManager dnsmgr = Common.IocKernel.Get<IDNSManager>(parameter);
    //        return dnsmgr;
    //    }

    //    void GatewayCallBack(IAsyncResult async)
    //    {
    //        AsyncResult ar = (AsyncResult)async;
    //        Func<IParameter, IGatewayManager> dgateway = (Func<IParameter, IGatewayManager>)ar.AsyncDelegate;
    //        gatewaymgr = dgateway.EndInvoke(async);
    //        log.Info("DisplayConnections, complete IGatewayManager");
    //    }
    //    //A method to be invoke by the delegate
    //    IGatewayManager ReadGatewayInfo(IParameter parameter)
    //    {
    //        IGatewayManager gatewaymgr = Common.IocKernel.Get<IGatewayManager>(parameter);
    //        return gatewaymgr;
    //    }

    //    private void Window_Loaded(object sender, RoutedEventArgs e)
    //    {
    //        log.Info("DisplayConnections_Load, before");
    //        //ConnectionsdataGridView.Name = WinObjMethods.ConnGridName;
    //        //WinObjMethods.AddColumn(ref ConnectionsdataGridView);

    //        BindConnectionGrid();

    //        //WinObjMethods.AddColumn(ref HistorydataGridView);

    //        //if (Mode == 0)
    //        //{
    //        //    foreach (var item in toolStrip.Items)
    //        //        (item as ToolStripButton).Visible = false;

    //        //    toolStripButtonRepair.IsVisible = true;
    //        //}

    //        int rowcnt = connmgr.GetConnectionsAmount();
    //        if (rowcnt > 0)
    //        {
    //            //HistorybindingSource.DataSource = new PageOffsetList(rowcnt);
    //            //HistorybindingSource.MoveFirst();
    //            BindHistoryGrid();
    //        }

    //        //FormLoadComplete = true;
    //        //log.Info("DisplayConnections_Load, after");
    //    }

    //    private void BindConnectionGrid()
    //    {
    //        List<Connection> connlist = namgr.GetItems(p => true);

    //        if (connlist.Count > 0)
    //        {                
    //            ConnectionsdataGridView.ItemsSource = connlist;
    //        }
    //    }

    //    private void BindHistoryGrid()
    //    {

    //        // The desired page has changed, so fetch the page of records using the "Current" offset 
    //        //if (HistorybindingSource.Current != null)
    //        //{
    //            //int offset = (int)HistorybindingSource.Current;
    //            IList<Connection> connlist = connmgr.GetConnections(0/*offset*/, HistorypageSize);

    //            foreach (Connection conn in connlist)
    //            {
    //                conn.DNS_list = dnsmgr.GetDNSsByConnectionId(conn.Id).ToList();
    //                conn.Gateway_list = gatewaymgr.GetGatewaysByConnectionId(conn.Id).ToList();
    //            }

    //            //var bindsList = new BindingList<Connection>(connlist);
    //            //Bind BindingList directly to the DataGrid
    //            //HistoryGridbindingSource.DataSource = connlist;
    //            //HistorydataGridView.DataSource = HistoryGridbindingSource;
    //            HistorydataGridView.ItemsSource = connlist;

    //            //WinObjMethods.ResizeGrid(ref ConnectionsdataGridView);
    //            //CorrectWindowSize();
    //            //}

    //    }

    //}
}
