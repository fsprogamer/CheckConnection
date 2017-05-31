using System;
using System.Collections.Generic;
using System.Windows;
using CheckConnection.Model;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CheckConnectionWpf.Methods;
using CheckConnectionWpf.Data;

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
