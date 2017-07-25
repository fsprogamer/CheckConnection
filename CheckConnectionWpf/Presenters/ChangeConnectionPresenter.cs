using CheckConnectionWpf.Data;
using CheckConnectionWpf.Views;

namespace CheckConnectionWpf.Presenters
{
    class ChangeConnectionPresenter
    {
        private /*readonly*/ IChangeConnectionView _view;
        public /*readonly*/ ChangeConnectionRepository _model;

        public ChangeConnectionPresenter(IChangeConnectionView view, ChangeConnectionRepository modeModel)
        {
            this._view = view;
            this._model = modeModel;
            
            //_view.connection = _model.connection;
            _view.LoadConnection(_model);
        }
    }
}
