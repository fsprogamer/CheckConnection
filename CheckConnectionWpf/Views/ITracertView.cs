using CheckConnectionWpf.Models;
using PingLib.Model;
using System;
using System.Collections.ObjectModel;

namespace CheckConnectionWpf.Views
{
    interface ITracertView:IPingView
    {
        event EventHandler<PingEventArgs> TracertStarted;        
        void ClearList();
        ObservableCollection<PingResult> ItemsSourceForPingList { get; set; }
    }
}
