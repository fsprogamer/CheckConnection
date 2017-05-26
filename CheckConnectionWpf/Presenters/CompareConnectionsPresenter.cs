using CheckConnectionWpf.Data;
using CheckConnectionWpf.Views;

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

            _view.LoadActiveConnection(_model.ActiveConnection);
            _view.LoadHistoryConnection(_model.HistoryConnection);
        }
    }
}
    
