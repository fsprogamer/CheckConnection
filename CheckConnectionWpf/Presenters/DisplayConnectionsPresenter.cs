using CheckConnectionWpf.Data;
using CheckConnectionWpf.Views;
using Common;

namespace CheckConnectionWpf.Presenters
{
    class DisplayConnectionsPresenter: ClassWithLog
    {
        private readonly IDisplayConnectionsView _view;
        private readonly DisplayConnectionsRepository _model;
        private readonly int HistorypageSize = CheckConnectionWpf.Properties.Settings.Default.HistoryPageSize;//10;

        public DisplayConnectionsPresenter(DisplayConnectionsForm displayConnectionsForm, DisplayConnectionsRepository displayConnectionsRepository)
        {
            _view = displayConnectionsForm;
            _model = displayConnectionsRepository;     
            
            if (_model.ProgramExpired )
            {
                string mess = "Превышено максимальное число запусков.Приложение будет закрыто.";
                log.Info(mess);
                _view.ShowMessage(mess, "", Icons.Stop);                
            }      
                   
            _view.LoadActiveConnections(_model.ActiveConnections());
            _view.LoadHistoryConnections(_model.HistoryConnections());
        }
    }
}
