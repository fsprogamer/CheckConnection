using CheckConnectionWpf.Models;
using System;

namespace CheckConnectionWpf.Views
{
    interface IModeView
    {
        event EventHandler<ModeModelEventArgs> ModeSelected;
        Mode SelectedMode { get; set; }

        void Hide();
        void ShowMessage(string cantMakeDir, string v, Icons error);
    }
}
