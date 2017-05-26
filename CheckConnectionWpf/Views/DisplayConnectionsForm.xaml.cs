using CheckConnection.Model;
using CheckConnectionWpf.Models;
using CheckConnectionWpf.Views;
using CheckConnectionWpf.Methods;
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
        //public event Action TableCompareButtonClicked;
        public event Action TracertButtonClicked;

        public event EventHandler<CompareConnectionsEventArgs> TableCompareButtonClicked;

        public DisplayConnectionsForm()
        {
            InitializeComponent();
            BindComponent();
        }

        private void BindComponent()
        {
            WinObjMethods.AddColumn(ref ConnectionsdataGridView);
            WinObjMethods.AddColumn(ref HistorydataGridView);
            Closed += (object sender, EventArgs e) => { Application.Current.Shutdown(); };
        }

        public Connection SelectedActiveConnection
        {
            get
            {
                return (Connection)ConnectionsdataGridView.SelectedItem;                
            }
            set
            {
                ConnectionsdataGridView.SelectedItem = value;
            }
        }

        public Connection SelectedHistoryConnection
        {
            get
            {
                return (Connection)HistorydataGridView.SelectedItem;
            }
            set
            {
                HistorydataGridView.SelectedItem = value;
            }
        }

        public int ActiveConnectionSelectedIndex
        {
            set
            {
                ConnectionsdataGridView.SelectedIndex = value;
            }
        }

        public int HistoryConnectionSelectedIndex
        {
            set
            {
                HistorydataGridView.SelectedIndex = value;
            }
        }

        public void LoadActiveConnections(IList<Connection> connections)
        {            
            if (connections?.Count > 0)
            {
                ConnectionsdataGridView.ItemsSource = connections;
            }
        }

        public void LoadHistoryConnections(IList<Connection> connections)
        {
            if (connections?.Count > 0)
            {
                HistorydataGridView.ItemsSource = connections;
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

        private void buttonTableCompare_Click(object sender, RoutedEventArgs e)
        {            
            TableCompareButtonClicked(this, 
                                      new CompareConnectionsEventArgs(SelectedActiveConnection,
                                                                     SelectedHistoryConnection)
                                     );
        }
    }

}
