using CheckConnectionWpf.Models;
using CheckConnectionWpf.Views;
using Common;
using PingLib.Model;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Threading;

namespace CheckConnectionWpf.Presenters
{
    class TracertPresenter : ClassWithLog
    {
        private readonly ITracertView _view;
        private readonly VRK.Net.Tracert _model;

        ObservableCollection<PingResult> tracertResults;

        public TracertPresenter(ITracertView view, VRK.Net.Tracert model)
        {
            this._view = view;
            this._model = model;

            BindControl();

            tracertResults = new ObservableCollection<PingResult>();
        }

        void BindControl()
        {
            _view.TracertStarted += OnTracertButtonClicked;
            _model.Done += tracert_Done;
            _model.RouteNodeFound += tracert_RouteNodeFound;
        }

        private void OnTracertButtonClicked(object sender, PingEventArgs e)
        {            
            _model.HostNameOrAddress = e.Destination;
            _view.TracertButtonEnable = false;
            _view.ClearList();
            tracertResults.Clear();
            _view.ItemsSourceForPingList = tracertResults;
            _model.Trace();
        }

        private void OnGetHostEntry(IAsyncResult ar)
        {

            try
            {
                PingResult host = ar.AsyncState as PingResult;
                host.Name = Dns.EndGetHostEntry(ar).HostName;
            }
            catch (SocketException ex)
            {
                log.Error(ex);
            }

            //try
            //{
            //    Application.Current.Dispatcher.BeginInvoke( DispatcherPriority.Background,
            //    new Action(() => { PingResult host = ar.AsyncState as PingResult; host.Name = Dns.EndGetHostEntry(ar).HostName; }));
            //}
            //catch (SocketException ex)
            //{
            //    log.Error(ex);
            //}
        }

        private void tracert_RouteNodeFound(object sender, VRK.Net.RouteNodeFoundEventArgs e)
        {
            var pingresult = new PingResult(_model.HostNameOrAddress);
            pingresult.Ip_Address = e.Node.Address.ToString();
            pingresult.Name = String.Empty;
            pingresult.ResponseTime = (e.Node.Status == IPStatus.Success) ? e.Node.RoundTripTime.ToString() : "*";
            tracertResults.Add(pingresult);            

            if (e.Node.Status == IPStatus.Success)
            {                
                Dns.BeginGetHostEntry(pingresult.Ip_Address, new AsyncCallback(this.OnGetHostEntry), pingresult);
            }
            
        }

        private void tracert_Done(object sender, EventArgs e)
        {
            _view.TracertButtonEnable = true;
        }
    }
}
