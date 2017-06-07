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

        //public event EventHandler<PingEventArgs> PingDone;

        public PingPresenter(IPingView view, PingResultManager model)
        {
            this._view = view;
            this._model = model;

            _view.PingStarted += OnPingButtonClicked;
        }

        private void OnPingButtonClicked(object sender, PingEventArgs e)
        {                                   
            PingResult pingResult = _model.GetPingResult(e.Destination);
            _view.AddItemAtPingList(pingResult);

        }
    }
}
