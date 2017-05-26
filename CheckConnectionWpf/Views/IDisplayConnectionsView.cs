using CheckConnection.Model;
using CheckConnectionWpf.Models;
using System;
using System.Collections.Generic;

namespace CheckConnectionWpf.Views
{
    public enum Icons { Error = 0, Stop };
    public interface IDisplayConnectionsView
    {
        //event Action Closed;

        event Action PingButtonClicked;
        event Action TracertButtonClicked;
        event Action CompareButtonClicked;
        event EventHandler<CompareConnectionsEventArgs> TableCompareButtonClicked;
        event Action ChangeButtonClicked;

        event Action RestoreButtonClicked;
        event Action RefreshDHCPButtonClicked;
        event Action RefreshButtonClicked;
        event Action AnalyzeButtonClicked;
        event Action RepairButtonClicked;

        event Action ActiveConnectionSelected;
        event Action HistoryConnectionSelected;

        Connection SelectedActiveConnection { get; set; }
        Connection SelectedHistoryConnection { get; set; }

        int ActiveConnectionSelectedIndex { set; }
        int HistoryConnectionSelectedIndex { set; }

        void LoadActiveConnections(IList<Connection> connections);
        void LoadHistoryConnections(IList<Connection> connections);
        void ShowMessage(string text, string caption, Icons icon);
        
    }
}
