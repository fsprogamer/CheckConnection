using System;
using CheckConnectionWpf.Data;
using CheckConnectionWpf.Views;
using Common;
using CheckConnectionWpf.Models;
using System.Linq;
using PingLib.Methods;

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

            _view.ActiveConnectionSelectedIndex = 0;
            _view.HistoryConnectionSelectedIndex = 0;

            _view.TableCompareButtonClicked += OnTableCompareButtonClicked;
            _view.CompareButtonClicked += OnCompareButtonClicked;
            _view.PingButtonClicked += OnPingButtonClicked;
            _view.TracertButtonClicked += OnTracertButtonClicked;

            _view.CompareButtonClicked += OnCompareButtonClicked;
            _view.ChangeConnectionButtonClicked += OnChangeConnectionButtonClicked;
        }
        
        private void OnTableCompareButtonClicked(object sender, CompareConnectionsEventArgs e)
        {
            try
            {
                var compareConnectionsForm = new CompareConnectionsForm();
                var compareConnectionsRepository = new CompareConnectionsRepository() { ActiveConnection = e.firstConnection,
                                                                                        HistoryConnection = e.secondConnection};
                var compareConnectionsPresenter = new CompareConnectionsPresenter(compareConnectionsForm, compareConnectionsRepository);
                // show other form            
                compareConnectionsForm.ShowDialog();
            }
            catch (Exception exeption)
            {
                _view.ShowMessage(exeption.Message, "Ошибка", Icons.Error);
            }
        }

        private void OnCompareButtonClicked(object sender, CompareConnectionsEventArgs e)
        {
            try
            {
                var compareConnectionsForm = new CompareConnectionsForm();
                var compareConnectionsRepository = new CompareConnectionsRepository()
                {
                    ActiveConnection = e.firstConnection,
                    HistoryConnection = e.secondConnection
                };

                var comparedConnections = compareConnectionsRepository.ComparedConnections;
                _view.LoadComparedConnections(comparedConnections.Where(p => p.Equal == false && p.Name != "Дата и время").ToList());
            }
            catch (Exception exeption)
            {
                _view.ShowMessage(exeption.Message, "Ошибка", Icons.Error);
            }
        }

        private void OnPingButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var pingForm = new PingForm();
                var pingResultMahager = new PingResultManager();
                var pingPresenter = new PingPresenter(pingForm, pingResultMahager);
                // show other form            
                pingForm.ShowDialog();
            }
            catch (Exception exeption)
            {
                _view.ShowMessage(exeption.Message, "Ошибка", Icons.Error);
            }
        }
        private void OnTracertButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var tracertForm = new TracertForm();
                var tracert = new VRK.Net.Tracert();
                var tracertPresenter = new TracertPresenter(tracertForm, tracert);
                // show other form            
                tracertForm.ShowDialog();
            }
            catch (Exception exeption)
            {
                _view.ShowMessage(exeption.Message, "Ошибка", Icons.Error);
            }
        }

        private void OnChangeConnectionButtonClicked(object sender, ConnectionEventArgs e)
        {
            try
            {
                var changeConnectionForm = new ChangeConnectionForm();
                var changeConnectionRepository = new ChangeConnectionRepository();
                //{
                //    ActiveConnection = e.firstConnection,
                //    HistoryConnection = e.secondConnection
                //};

                var changeConnectionPresenter = new ChangeConnectionPresenter(changeConnectionForm, changeConnectionRepository);
                // show other form            
                changeConnectionForm.ShowDialog();
            }
            catch (Exception exeption)
            {
                _view.ShowMessage(exeption.Message, "Ошибка", Icons.Error);
            }
        }
    }
}
