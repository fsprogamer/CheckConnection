using CheckConnectionWpf.Data;
using CheckConnectionWpf.Views;

namespace CheckConnectionWpf.Presenters
{
    class ChangeConnectionPresenter
    {
        private readonly IChangeConnectionView _view;
        private readonly ChangeConnectionRepository _model;

        public ChangeConnectionPresenter(IChangeConnectionView view, ChangeConnectionRepository modeModel)
        {
            this._view = view;
            this._model = modeModel;

            //List<CompareConnection> compareConnections = CompareConnection.GetDifference(_model.ActiveConnection, _model.HistoryConnection);
            //_view.LoadConnections(compareConnections);
        }
    }
}
