using CheckConnection.Model;
using CheckConnectionWpf.Data;
using CheckConnectionWpf.ExtensionMethods;
using CheckConnectionWpf.Methods;
using CheckConnectionWpf.Models;
using CheckConnectionWpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CheckConnectionWpf
{
    public partial class DisplayConnectionsForm : Window, IDisplayConnectionsView
    {
        public event Action ActiveConnectionSelected;
        public event Action AnalyzeButtonClicked;
        public event Action ChangeButtonClicked;
        public event Action HistoryConnectionSelected;
        
        public event Action RefreshButtonClicked;
        public event Action RefreshDHCPButtonClicked;
        public event Action RepairButtonClicked;
        public event Action RestoreButtonClicked;   

        public event EventHandler<CompareConnectionsEventArgs> TableCompareButtonClicked;
        public event EventHandler<CompareConnectionsEventArgs> CompareButtonClicked;
        public event EventHandler PingButtonClicked;
        public event EventHandler TracertButtonClicked;

        public DisplayConnectionsForm()
        {
            InitializeComponent();
            BindComponent();
        }
        
        private void BindComponent()
        {
            WinObjMethods.AddColumn(ref ConnectionsdataGridView);
            WinObjMethods.AddColumn(ref HistorydataGridView);
            Closed += (object sender, EventArgs e) => { Application.Current.Shutdown(); };
        }

        public Connection SelectedActiveConnection
        {
            get
            {
                return (Connection)ConnectionsdataGridView.SelectedItem;                
            }
            set
            {
                ConnectionsdataGridView.SelectedItem = value;
            }
        }

        public Connection SelectedHistoryConnection
        {
            get
            {
                return (Connection)HistorydataGridView.SelectedItem;
            }
            set
            {
                HistorydataGridView.SelectedItem = value;
            }
        }

        public int ActiveConnectionSelectedIndex
        {
            set
            {
                ConnectionsdataGridView.SelectedIndex = value;
            }
        }

        public int HistoryConnectionSelectedIndex
        {
            set
            {
                HistorydataGridView.SelectedIndex = value;
            }
        }

        public void LoadActiveConnections(IList<Connection> connections)
        {            
            if (connections?.Count > 0)
            {
                ConnectionsdataGridView.ItemsSource = connections;
            }
        }

        public void LoadHistoryConnections(IList<Connection> connections)
        {
            if (connections?.Count > 0)
            {
                HistorydataGridView.ItemsSource = connections;
            }
        }

        public void LoadComparedConnections(IList<CompareConnection> connections)
        {
            if (connections?.Count > 0)
            {
                foreach(CompareConnection conn in connections)                    
                {
                    int selected = ConnectionsdataGridView.Columns.IndexOf(ConnectionsdataGridView.Columns.FirstOrDefault(c => c.Header.ToString() == conn.Name));                   

                    var selectedRow = ConnectionsdataGridView.GetSelectedRow();
                    var columnCell = ConnectionsdataGridView.GetCell(selectedRow, selected);

                    columnCell.Style = (Style)FindResource("DifferentDataGridCell");
                }
            }
        }

        public void ShowMessage(string text, string caption, Icons icon)
        {            
            if (icon == Icons.Error)
            {
                 MessageBox.Show(this, text, caption,
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
            }
            else if (icon == Icons.Stop)
            {
                MessageBox.Show(this, text, caption,
                           MessageBoxButton.OK,
                           MessageBoxImage.Stop);
                Application.Current.Shutdown();
            }
        }

        private void buttonTableCompare_Click(object sender, RoutedEventArgs e)
        {            
            TableCompareButtonClicked(this, 
                                      new CompareConnectionsEventArgs(SelectedActiveConnection,
                                                                      SelectedHistoryConnection)
                                     );
        }

        private void buttonCompare_Click(object sender, RoutedEventArgs e)
        {
            CompareButtonClicked(this,
                                 new CompareConnectionsEventArgs(SelectedActiveConnection,
                                                                 SelectedHistoryConnection)
                                );

            #region
            //ConnectionsdataGridView.Items.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = Color.Red;

            //Style style = new Style();
            //style.TargetType = typeof(DataGridCell);
            //DataTrigger DT = new DataTrigger();

            //Binding DataTriggerBinding = new Binding( "bindingName1");
            //DataTriggerBinding.Mode = BindingMode.Default;
            //DataTriggerBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //DT.Binding = DataTriggerBinding;
            //DT.Value = "10.249.150.1";
            //Setter DataTriggerSetter = new Setter();
            //DataTriggerSetter.Property = DataGridCell.BackgroundProperty;
            //DataTriggerSetter.Value = Brushes.Red;
            //DT.Setters.Add(DataTriggerSetter);
            //style.Triggers.Add(DT);

            //dataGridTextColumn.CellStyle = style;
            #endregion
        }

        #region
        //private void CreateDynamicColumn()
        //{
        //    //creates object of datagridtextcolum class
        //    DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();

        //    // sets header name
        //    dataGridTextColumn.Header = "Dynamic Column";

        //    //creates object of binding class , accepts name of property as parameter , whose value to be display
        //    Binding binding = new Binding("PropertyName");
        //    // sets binding mode
        //    binding.Mode = BindingMode.TwoWay;

        //    // sets how trigger to be fired.
        //    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

        //    // assigned the object of binding class to object of datagridtextcolumn class;
        //    dataGridTextColumn.Binding = binding;


        //    //code for add style to row
        //    Style Rowstyle = new Style(); // creates object of style class  
        //    Rowstyle.TargetType = typeof(DataGridRow);//sets target type as DataGrid row

        //    Setter setterBackground = new Setter(); // create objects of setter class  
        //    setterBackground.Property = DataGridRow.BackgroundProperty;
        //    setterBackground.Value = Brushes.Black;
        //    Rowstyle.Setters.Add(setterBackground);

        //    Setter setterForeGround = new Setter();
        //    setterForeGround.Property = DataGridRow.ForegroundProperty;
        //    setterForeGround.Value = Brushes.White;
        //    Rowstyle.Setters.Add(setterForeGround);
        //    ConnectionsdataGridView.RowStyle = Rowstyle;



        //    Style DataGridCellStyle = new Style();
        //    DataGridCellStyle.TargetType = typeof(DataGridCell);
        //    DataTrigger DT = new DataTrigger();

        //    Binding DataTriggerBinding = new Binding("bindingName");
        //    DataTriggerBinding.Mode = BindingMode.Default;
        //    DataTriggerBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        //    DT.Binding = DataTriggerBinding;
        //    DT.Value = "<=100";
        //    Setter DataTriggerSetter = new Setter();
        //    DataTriggerSetter.Property = DataGridCell.BackgroundProperty;
        //    DataTriggerSetter.Value = Brushes.Red;
        //    DT.Setters.Add(DataTriggerSetter);
        //    DataGridCellStyle.Triggers.Add(DT);

        //    // added created column into column list of datagrid (dgvRates);
        //    ConnectionsdataGridView.Columns.Add(dataGridTextColumn);

        //}
        #endregion

        private void ConnectionsdataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = ConnectionsdataGridView.GetSelectedRow();
            if(selectedRow!=null)
            for (int index = 0; index < ConnectionsdataGridView.Columns.Count; index++)
            {
                var columnCell = ConnectionsdataGridView.GetCell(selectedRow, index);
                if (columnCell?.Style != null) {
                        columnCell.Style = null; 
                }
            }
            
        }

        private void buttonPing_Click(object sender, RoutedEventArgs e)
        {
            PingButtonClicked(this,new EventArgs());
        }

        private void buttonTracert_Click(object sender, RoutedEventArgs e)
        {
            TracertButtonClicked(this, new EventArgs());
        }        
    }
}
