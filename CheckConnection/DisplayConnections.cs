using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using TracertForm;
using CheckConnection.Methods;
using CheckConnection.Model;

namespace CheckConnection
{
    public partial class DisplayConnections : Form
    {
        public DisplayConnections()
        {
            InitializeComponent();
            BindConnectionGrid(ref ConnectionsdataGridView);
            BindHistoryGrid(ref HistorydataGridView);

            //row.Cells[col.Name].Value = (row.Cells[col.Name] as DataGridViewComboBoxCell).Items[0];

            WinObjMethods.ResizeGrid(ref ConnectionsdataGridView);
            CorrectWindowSize();
        }

        private void AddColumn(ref DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;

            //create the column programatically
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Date",
                HeaderText = "Дата и время",
                DataPropertyName = "Date", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Name",
                HeaderText = "Название подключения",
                DataPropertyName = "Name", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "MAC",
                HeaderText = "MAC адрес",
                DataPropertyName = "MAC", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Ip_Address_v4",
                HeaderText = "IP адрес",
                DataPropertyName = "Ip_Address_v4", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Ip_Address_v6",
                HeaderText = "IP адрес v6",
                DataPropertyName = "Ip_Address_v6", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DHCP_Enabled",
                HeaderText = "DHCP включен",
                DataPropertyName = "DHCP_Enabled", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DHCPServer",
                HeaderText = "DHCP сервер",
                DataPropertyName = "DHCPServer", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DNSDomain",
                HeaderText = "Основной DNS-суффикс",
                DataPropertyName = "DNSDomain", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "IPSubnetMask",
                HeaderText = "Маска подсети",
                DataPropertyName = "IPSubnetMask", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DefaultIPGateways",
                HeaderText = "Основной шлюз",
                DataPropertyName = "DefaultIPGateways", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };
            dgv.Columns.Add(colName);

            //DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn()
            //{
            //    //CellTemplate = cell,
            //    Name = "DNSArray",
            //    HeaderText = "DNS-серверы",
            //    DataPropertyName = "IP_Address", // Tell the column which property it should use
            //    AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            //};                   

            //dgv.Columns.Add(cmb);

        }

        private void BindConnectionGrid(ref DataGridView dgv)
        {            
            WMIMethods methods = new WMIMethods();
            List<Connection> connlist = methods.GetNetworkDevices();
            List<DNS> dnslist = methods.GetDNSArray(connlist[0].Id);
            List<Gateway> gtwlist = methods.GetGatewayArray(connlist[0].Id);

            AddColumn(ref dgv);

            if (connlist.Count > 0)
            {
                var bindsList = new BindingList<Connection>(connlist); // <-- BindingList
                //Bind BindingList directly to the DataGrid
                dgv.DataSource = bindsList;
            }

            if (dnslist.Count > 0)
            {
                var bindsList = new BindingList<DNS>(dnslist); // <-- BindingList

                /*
                    int index = this.editingDataGridView.Columns.IndexOf(comboBoxColumn);
                    for (int i = 0; i < columns.Count; i++)
                    {
                       this.editingDataGridView.Rows[i].Cells[index].Value = columns[i].Table_ID;
                    } 
                 */

                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn()
                {
                    //CellTemplate = cell,
                    Name = "DNSArray",
                    HeaderText = "DNS-серверы",
                    DataPropertyName = "Ip_Address", // Tell the column which property it should use
                    DisplayMember = "Ip_Address",
                    ValueMember = "Id",                    
                    AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill,
                    
                };
                cmb.DataSource = bindsList;

                dgv.Columns.Add(cmb);

                dgv.Rows[0].Cells[10/*"DNSArray"*/].Value = "0";                
            }

            if (gtwlist.Count > 0)
            {
                var bindsList = new BindingList<Gateway>(gtwlist); // <-- BindingList
                //Bind BindingList directly to the ComboBox
                //dgv.DataSource = bindsList;
                //DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgv.Rows[0].Cells["DNSArray"];
                //cell.Items.AddRange(dnslist);
            }

            WinObjMethods.ResizeGrid(ref dgv);
            CorrectWindowSize();
        }
        private void BindComboBox(ref DataGridView dgv)
        {
            WMIMethods methods = new WMIMethods();
            List<Connection> connlist = methods.GetNetworkDevices();
            List<DNS> dnslist = methods.GetDNSArray(connlist[0].Id);
            List<Gateway> gtwlist = methods.GetGatewayArray(connlist[0].Id);

            if (dnslist.Count > 0)
            {
                var bindsList = new BindingList<DNS>(dnslist); // <-- BindingList
                //Bind BindingList directly to the ComboBox
                //(DataGridViewComboBoxColumn)dgv.Columns["DNSArray"]. .DataSource = bindsList;
                //System.Object[] ItemObject = new System.Object[10];
                //for (int i = 0; i <= 9; i++)
                //{
                //    ItemObject[i] = "Item" + i;
                //}
                //listBox1.Items.AddRange(ItemObject);
            }
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
            WMIMethods methods = new WMIMethods();
            List<Connection> Connection_list = methods.GetNetworkDevices();
            List<DNS> DNS_list = methods.GetDNSArray(Connection_list[0].Id);
            List<Gateway> Gateway_list = methods.GetGatewayArray(Connection_list[0].Id);

            if (Connection_list.Count > 0)            
                DB.SaveConnectionTable(Connection_list, DNS_list, Gateway_list);

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
            //WMIMethods methods = new WMIMethods();
            List<Ping> Ping_list = new List<Ping>();
            //methods.GetPingResult("ya.ru",ref Ping_list);

            //DbMethods DB = new DbMethods();
            //DB.SavePingTable(ref Ping_list);
            var PingForm = new TracertForm.PingForm();
            PingForm.Show();
        }

        private void TracerttoolStripButton_Click(object sender, System.EventArgs e)
        {
            //WMIMethods methods = new WMIMethods();
            List<Tracert> Tracert_list = new List<Tracert>();
            //methods.GetTracertResult("ya.ru", ref Tracert_list);

            //DbMethods DB = new DbMethods();
            //DB.SaveTracertTable(ref Tracert_list);

            var TracertForm = new TracertForm.MainForm();
            TracertForm.Show();

        }
    }
}
