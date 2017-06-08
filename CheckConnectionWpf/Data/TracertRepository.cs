using Common;
using PingLib.Model;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace CheckConnectionWpf.Data
{
    class TracertRepository: ClassWithLog
    {
        //ObservableCollection<PingResult> tracertResults;
        //public TracertRepository()
        //{
        //    tracertResults = new ObservableCollection<PingResult>();

        //    _model.Done += tracert_Done;
        //    _model.RouteNodeFound += tracert_RouteNodeFound;
        //}

        //private void OnGetHostEntry(IAsyncResult ar)
        //{
        //    try
        //    {
        //        PingResult host = ar.AsyncState as PingResult;
        //        host.Name = Dns.EndGetHostEntry(ar).HostName;
        //    }
        //    catch (SocketException ex)
        //    {
        //        log.Error(ex);
        //    }
        //}

        //private void tracert_RouteNodeFound(object sender, VRK.Net.RouteNodeFoundEventArgs e)
        //{
        //    var pingresult = new PingResult(_model.HostNameOrAddress);
        //    pingresult.Ip_Address = e.Node.Address.ToString();
        //    pingresult.Name = String.Empty;
        //    pingresult.ResponseTime = (e.Node.Status == IPStatus.Success) ? e.Node.RoundTripTime.ToString() : "*";
        //    tracertResults.Add(pingresult);
        //    //var host = tracertResults[tracertResults.Count - 1];

        //    if (e.Node.Status == IPStatus.Success)
        //    {
        //        Dns.BeginGetHostEntry(pingresult.Ip_Address, new AsyncCallback(this.OnGetHostEntry), pingresult);
        //    }

        //}

        //private void tracert_Done(object sender, EventArgs e)
        //{
        //    _view.TracertButtonEnable = true;
        //}
    }
}
