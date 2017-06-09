using CheckConnectionWpf.Data;
using System.Collections.Generic;
using System.Windows;

namespace CheckConnectionWpf.Views
{
    /// <summary>
    /// Interaction logic for CompareConnections.xaml
    /// </summary>
    public partial class CompareConnectionsForm : Window, ICompareConnectionsView
    {
        public CompareConnectionsForm()
        {
            InitializeComponent();
        }
        public void LoadConnections(List<CompareConnection> connections)
        {
            listBoxCompareConnection.ItemsSource = connections;
        }
    }
}
