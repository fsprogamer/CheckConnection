using CheckConnectionWpf.Models;
using CheckConnectionWpf.Views;
using System;
using System.Windows;

namespace CheckConnectionWpf
{
    /// <summary>
    /// Interaction logic for ModeForm.xaml
    /// </summary>
    public partial class ModeForm : Window, IModeView
    {
        public event EventHandler<ModeModelEventArgs> ModeSelected;
        public ModeForm()
        {
            InitializeComponent();
            BindComponent();
        }

        private void BindComponent()
        {
            Closed += (object sender, EventArgs e) => { Application.Current.Shutdown(); };
        }

        public void buttonChoose_Click(object sender, EventArgs e)
        {
            var modeModel = new ModeModel(SelectedMode);
            ModeSelected(this, new ModeModelEventArgs(modeModel));
        }

        public void buttonCancel_Click(object sender, EventArgs e)
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
                Close();
            }
        }

        public Mode SelectedMode
        {
            get
            {
                return (radioButtonDiagnose.IsChecked == true) ? Mode.Advanced : Mode.Simple;
            }
            set
            {
                if (value == Mode.Advanced)
                {
                    radioButtonDiagnose.IsChecked = true;
                    radioButtonRepair.IsChecked = false;
                }
                else
                {
                    radioButtonDiagnose.IsChecked = false;
                    radioButtonRepair.IsChecked = true;
                }
            }
        }

    }
}
