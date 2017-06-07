using CheckConnectionWpf.Models;
using PingLib.Model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CheckConnectionWpf.Views
{
    /// <summary>
    /// Interaction logic for PingForm.xaml
    /// </summary>
    public partial class PingForm : Window, IPingView
    {
        public PingForm()
        {
            InitializeComponent();
            BindControl();
        }
        public string GroupBoxHeader { set { groupBox.Header = value; } }
        public string FormTitle { set { this.Title = value; } }

        public event EventHandler<PingEventArgs> PingStarted;

        public virtual void BindControl()
        {
            startTrace.Click += startPing_Click;
        }
        public void AddItemAtPingList(PingResult pingresult)
        {
            if (pingresult != null)
            {
                if (pingList.ItemsSource == null)
                {
                    var replies = new ObservableCollection<PingResult>();
                    replies.Add(pingresult);
                    pingList.ItemsSource = replies;
                }
                else
                {
                    ((ObservableCollection<PingResult>)pingList.ItemsSource).Add(pingresult);               
                }
            }
        }

        public void close_Click(object sender, EventArgs e)
        {
            Close();
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

        private void startPing_Click(object sender, RoutedEventArgs e)
        {            
            PingStarted(this, new PingEventArgs(destination.Text));
        }
        
    }

    public class NodeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return "Hello";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            var item = (ListViewItem)value;
            var listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1;
            return index.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
