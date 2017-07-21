using CheckConnection.Model;

namespace CheckConnectionWpf.Views
{
    interface IChangeConnectionView
    {
        //event EventHandler<PingEventArgs> PingStarted;
        void LoadConnection(Connection connection);
        //Connection connection { get; set; }
    }
}
