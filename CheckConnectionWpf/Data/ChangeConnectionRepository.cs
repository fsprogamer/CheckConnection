using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CheckConnection.Model;
using System.Runtime.CompilerServices;

namespace CheckConnectionWpf.Data
{
    class ChangeConnectionRepository : INotifyPropertyChanged
    {
        private Connection _connection;
        public Connection connection
        {
            get { return _connection; }
            set
            {
                if (value != _connection)
                {
                    _connection = value;
                    OnPropertyChangedOldStyle("connection");
                }
            }
        }                 

        public ChangeConnectionRepository(Connection connection)
        {
            this.connection = connection;
        }

        public event PropertyChangedEventHandler PropertyChanged;
      
        private void OnPropertyChangedOldStyle(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
