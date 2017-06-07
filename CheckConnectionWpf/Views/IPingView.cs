using CheckConnectionWpf.Models;
using PingLib.Model;
using System;
using System.Net.NetworkInformation;

namespace CheckConnectionWpf.Views
{
    interface IPingView
    {
        event EventHandler<PingEventArgs> PingStarted;
        void AddItemAtPingList(PingResult pingResult);
        void ShowMessage(string text, string caption, Icons icon);
    }
}
