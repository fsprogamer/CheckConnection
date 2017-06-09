using System;
using System.Windows;

namespace CheckConnectionWpf.Views
{
    /// <summary>
    /// Interaction logic for ChangeConnectionForm.xaml
    /// </summary>
    public partial class ChangeConnectionForm : Window, IChangeConnectionView
    {
        public ChangeConnectionForm()
        {
            InitializeComponent();
        }
        public void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
