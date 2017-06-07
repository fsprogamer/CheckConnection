using CheckConnectionWpf.Models;
using PingLib.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace CheckConnectionWpf.Views
{
    /// <summary>
    /// Interaction logic for TraceForm.xaml
    /// </summary>
    public partial class TracertForm : PingForm, ITracertView
    {
        public TracertForm()
        {
            InitializeComponent();
        }

       
        public event EventHandler<PingEventArgs> TracertStarted;

        public override void BindControl()
        {
            GroupBoxHeader = "Трассировка маршрута";
            FormTitle = "Маршрут";
            startTrace.Click += startPing_Click;
        }

        private void startPing_Click(object sender, RoutedEventArgs e)
        {
            TracertStarted(this, new PingEventArgs(destination.Text));
        }

        public void ClearList()
        {
            pingList.ItemsSource = null;
            pingList.Items.Clear();
        }
        public bool TracertButtonEnable
        {
            get
            {
                return startTrace.IsEnabled;
            }

            set
            {
                startTrace.IsEnabled = value;
            }
        }
        public ObservableCollection<PingResult> ItemsSourceForPingList
        {
            get
            {
                return pingList.ItemsSource as ObservableCollection<PingResult>;
            }
            set
            {
                pingList.ItemsSource = value;
            }
        }
    }
}
