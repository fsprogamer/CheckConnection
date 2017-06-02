using CheckConnectionWpf.Data;
using CheckConnectionWpf.Views;
using System.Collections.Generic;

namespace CheckConnectionWpf.Presenters
{
    class CompareConnectionsPresenter
    {
        private readonly ICompareConnectionsView _view;
        private readonly CompareConnectionsRepository _model;

        public CompareConnectionsPresenter(ICompareConnectionsView view, CompareConnectionsRepository modeModel)
        {
            this._view = view;
            this._model = modeModel;

            List<CompareConnection> compareConnections = CompareConnection.GetDifference(_model.ActiveConnection, _model.HistoryConnection);
            _view.LoadConnections(compareConnections);
        }
    }
}
    
