using CheckConnection.Model;
using CheckConnectionWpf.Data;
using CheckConnectionWpf.Methods;
using CheckConnectionWpf.Views;
using System.Collections.Generic;
using System.Linq;

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

            var query = from active in ReflectionProperties<Connection>.GetPropertiesValueList(_model.ActiveConnection)
                         join history in ReflectionProperties<Connection>.GetPropertiesValueList(_model.HistoryConnection) 
                         on active.Name equals history.Name
                        where active.Name != "Index"
                        select new CompareConnection (){ Name = active.Name, Active = active.Value, History = history.Value };

            List<CompareConnection> compareConnections = query.ToList<CompareConnection>(); 
            _view.LoadConnections(compareConnections);
        }
    }
}
    
