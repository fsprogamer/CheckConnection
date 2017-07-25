using CheckConnection.Model;
using CheckConnectionWpf.Data;

namespace CheckConnectionWpf.Views
{
    interface IChangeConnectionView
    {
        //event EventHandler<PingEventArgs> PingStarted;
        void LoadConnection(ChangeConnectionRepository repoconnection);
        //Connection connection { get; set; }
    }
}
