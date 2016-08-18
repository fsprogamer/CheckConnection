using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using TracertForm;

namespace ReestrUser
{
    public partial class DisplayConnections : Form
    {
        public DisplayConnections()
        {
            InitializeComponent();
            BindConnectionGrid(ref ConnectionsdataGridView);
            BindHistoryGrid(ref HistorydataGridView);
            WinObjMethods.ResizeGrid(ref ConnectionsdataGridView);
            CorrectWindowSize();
        }

        private void AddColumn(ref DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;

            //create the column programatically
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxColumn colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Date",
                HeaderText = "Дата и время",
                DataPropertyName = "Date", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Name",
                HeaderText = "Название подключения",
                DataPropertyName = "Name", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "MAC",
                HeaderText = "MAC адрес",
                DataPropertyName = "MAC", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Ip_Address_v4",
                HeaderText = "IP адрес",
                DataPropertyName = "Ip_Address_v4", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Ip_Address_v6",
                HeaderText = "IP адрес v6",
                DataPropertyName = "Ip_Address_v6", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DHCP_Enabled",
                HeaderText = "DHCP включен",
                DataPropertyName = "DHCP_Enabled", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DHCPServer",
                HeaderText = "DHCP сервер",
                DataPropertyName = "DHCPServer", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DNSDomain",
                HeaderText = "Основной DNS-суффикс",
                DataPropertyName = "DNSDomain", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "IPSubnetMask",
                HeaderText = "Маска подсети",
                DataPropertyName = "IPSubnetMask", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colFileName);

            colFileName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DefaultIPGateways",
                HeaderText = "Основной шлюз",
                DataPropertyName = "DefaultIPGateways", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colFileName);

        }

        private void BindConnectionGrid(ref DataGridView dgv)
        {
            List<Connection> connlist = new List<Connection>();
            WMIMethods methods = new WMIMethods();

            methods.GetNetworkDevices(ref connlist);

            AddColumn(ref dgv);

            if (connlist.Count > 0)
            {
                var connnamesList = new BindingList<Connection>(connlist); // <-- BindingList
                //Bind BindingList directly to the DataGrid
                dgv.DataSource = connnamesList;
            }

            WinObjMethods.ResizeGrid(ref dgv);
            CorrectWindowSize();
        }
        private void BindHistoryGrid(ref DataGridView dgv)
        {
            List<Connection> connlist = new List<Connection>();
            DbMethods DB = new DbMethods();

            DB.ReadConnectionHistory(ref connlist);

            AddColumn(ref dgv);

            if (connlist.Count > 0)
            {
                var connnamesList = new BindingList<Connection>(connlist); // <-- BindingList
                //Bind BindingList directly to the DataGrid
                dgv.DataSource = connnamesList;
            }

            WinObjMethods.ResizeGrid(ref dgv);
            CorrectWindowSize();            
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            DbMethods DB = new DbMethods();
            List<Connection> Connection_list = new List<Connection>();
            WMIMethods methods = new WMIMethods();

            methods.GetNetworkDevices(ref Connection_list);
            if (Connection_list.Count > 0)            
                DB.SaveConnectionTable(ref Connection_list);

            //DB.ReadConnectionHistory(ref Connection_list);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

        }
        public void CorrectWindowSize()
        {
            int width = WinObjMethods.CountGridWidth(ConnectionsdataGridView);
            ClientSize = new Size(width, ClientSize.Height);
        }

        private void PingtoolStripButton_Click(object sender, System.EventArgs e)
        {
            WMIMethods methods = new WMIMethods();
            List<Ping> Ping_list = new List<Ping>();
            //methods.GetPingResult("ya.ru",ref Ping_list);

            //DbMethods DB = new DbMethods();
            //DB.SavePingTable(ref Ping_list);
            var PingForm = new TracertForm.PingForm();
            PingForm.Show();
        }

        private void TracerttoolStripButton_Click(object sender, System.EventArgs e)
        {
            WMIMethods methods = new WMIMethods();
            List<Tracert> Tracert_list = new List<Tracert>();
            //methods.GetTracertResult("ya.ru", ref Tracert_list);

            //DbMethods DB = new DbMethods();
            //DB.SaveTracertTable(ref Tracert_list);

            var TracertForm = new TracertForm.MainForm();
            TracertForm.Show();

        }
    }
}
