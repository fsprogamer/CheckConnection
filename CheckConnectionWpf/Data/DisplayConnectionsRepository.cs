using CheckConnection.Methods;
using CheckConnection.Model;
using CheckConnectionWpf.Methods;
using CheckConnectionWpf.Models;
using Common;
using Ninject.Parameters;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace CheckConnectionWpf.Data
{
    class DisplayConnectionsRepository : ClassWithLog
    {
        private SQLiteConnection sqlconn;
        private IConnectionManager connmgr;
        private IDNSManager dnsmgr;
        private IGatewayManager gatewaymgr;
        private IWMINetworkAdapterManager namgr;
        public Mode appMode { get; set; }

        public string UserAccount
        {
            get
            {
                WMIAccountManager wmiacc = new WMIAccountManager();
                if ((wmiacc.IsAdminAccount()) == true)
                    return " (Администратор)";
                else
                    return string.Empty;
            }
        }

        public DisplayConnectionsRepository()
        {            
            log.Info("DisplayConnectionsRepository, before");
            string conn_string = WinObjMethods.GetDBConnectionString(log);
            try
            {
                sqlconn = new SQLiteConnection(conn_string, true);
            }
            catch (Exception e)
            {
                string CantMakeDir = "Невозможно открыть файл " + Environment.NewLine + conn_string;
                log.Error(CantMakeDir, e);
            }            

                log.Info("DisplayConnections, before Ninject");

                log.Info("DisplayConnections, before IWMINetworkAdapterManager");
                Func<IWMINetworkAdapterManager> dwmi = ReadWMIInfo;
                //Invoke our method in another thread
                IAsyncResult asyncWMI = dwmi.BeginInvoke(new AsyncCallback(WMICallBack), null);
                log.Info("DisplayConnections, after IWMINetworkAdapterManager");

                if (sqlconn != null)
                {
                    //Set the parameter for Ninject
                    IParameter parameter = new ConstructorArgument("conn", sqlconn);

                    Func<IParameter, IConnectionManager> dconnection = new Func<IParameter, IConnectionManager>(ReadConnectionInfo);
                    //Invoke our method in another thread
                    IAsyncResult asyncConnection = dconnection.BeginInvoke(parameter, new AsyncCallback(ConnectionCallBack), null);
                    log.Info("DisplayConnections, after Ninject IConnectionManager");

                    Func<IParameter, IDNSManager> ddns = new Func<IParameter, IDNSManager>(ReadDNSInfo);
                    //Invoke our method in another thread
                    IAsyncResult asyncDNS = ddns.BeginInvoke(parameter, new AsyncCallback(DNSCallBack), null);
                    log.Info("DisplayConnections, after Ninject IDNSManager");

                    Func<IParameter, IGatewayManager> dgateway = new Func<IParameter, IGatewayManager>(ReadGatewayInfo);
                    //Invoke our method in another thread
                    IAsyncResult asyncGateway = dgateway.BeginInvoke(parameter, new AsyncCallback(GatewayCallBack), null);
                    log.Info("DisplayConnections, after Ninject IGatewayManager");

                    asyncConnection.AsyncWaitHandle.WaitOne();
                    asyncGateway.AsyncWaitHandle.WaitOne();
                    asyncDNS.AsyncWaitHandle.WaitOne();
                }
                asyncWMI.AsyncWaitHandle.WaitOne();
            
        }

        void WMICallBack(IAsyncResult async)
        {
            AsyncResult ar = (AsyncResult)async;
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

        public bool ProgramExpired
        {
            get {
                const uint ExpiryPeriod = 30;
                if (connmgr?.GetDiffInDays() > ExpiryPeriod)
                {
                    return true;
                }
                else return false;
            }
        }

        public IList<Connection> ActiveConnections()
        {
            return namgr?.GetItems(p => true);
        }
        public IList<Connection> HistoryConnections(int Offset = 0, int HistorypageSize = 10)
        {
            IList<Connection> historyconnections = connmgr?.GetConnections(Offset, HistorypageSize);
            if (historyconnections != null)
            {
                foreach (Connection conn in historyconnections)
                {
                    conn.DNS_list = dnsmgr?.GetDNSsByConnectionId(conn.Id).ToList();
                    conn.Gateway_list = gatewaymgr?.GetGatewaysByConnectionId(conn.Id).ToList();
                }
            }
            return historyconnections;
        }
    }
}
