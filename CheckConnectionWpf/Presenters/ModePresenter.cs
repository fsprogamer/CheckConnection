using CheckConnectionWpf.Data;
using CheckConnectionWpf.Models;
using CheckConnectionWpf.Views;
using Common;
using System;

namespace CheckConnectionWpf.Presenters
{
    class ModePresenter:ClassWithLog
    {
        private readonly IModeView _view;
        private readonly ModeModel _modeModel;

        public ModePresenter(IModeView view, ModeModel modeModel)
        {
            this._view = view;
            this._modeModel = modeModel;

            _view.SelectedMode = _modeModel.Id;

            _view.ModeSelected += OnModeSelected;
            _modeModel.ModeChanged += OnModeChanged;
        }

        private void OnModeChanged(object sender, ModeModelEventArgs e)
        {
            _view.SelectedMode = e.ModeModel.Id;
        }

        public void OnModeSelected(object sender, ModeModelEventArgs e)
        {
            Mode s_mode = e.ModeModel.Id;
            // hide form
            _view.Hide();

            try
            {
                var displayConnectionsForm = new DisplayConnectionsForm();
                var displayConnectionsRepository = new DisplayConnectionsRepository() { appMode = e.ModeModel.Id };
                var displayConnectionsPresenter = new DisplayConnectionsPresenter(displayConnectionsForm, displayConnectionsRepository);
                // show other form            
                displayConnectionsForm.ShowDialog();
            }
            catch (Exception exeption)
            {
                _view.ShowMessage(exeption.Message, "Ошибка", Icons.Error);
            }
        }

    }
}
