using CheckConnectionWpf.Models;
using CheckConnectionWpf.Views;
using Common;
using PingLib.Methods;
using PingLib.Model;

namespace CheckConnectionWpf.Presenters
{
    class PingPresenter : ClassWithLog
    {
        private readonly IPingView _view;
        private readonly PingResultManager _model;        

        public PingPresenter(IPingView view, PingResultManager model)
        {
            this._view = view;
            this._model = model;

            _view.PingStarted += OnPingButtonClicked;
            _model.PingResultCompleted += OnPingResultCompleted;
        }

        private void OnPingButtonClicked(object sender, PingEventArgs e)
        {
            _view.TracertButtonEnable = false;
            _model.GetPingResultAsync(e.Destination);            
        }

        private void OnPingResultCompleted(object sender, PingResultEventArgs e)
        {
            _view.AddItemAtPingList(e.PingResult);
            _view.TracertButtonEnable = true;
        }
    }
}
