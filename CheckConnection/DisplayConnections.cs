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
        delegate void SetComboBoxCellType(int iRowIndex);
        bool bIsComboBox = false;

        public DisplayConnections()
        {
            InitializeComponent();
        }

        private void DisplayConnections_Load(object sender, System.EventArgs e)
        {
            BindConnectionGrid(ref ConnectionsdataGridView);

            //ChangeCellToComboBox(0);

            BindHistoryGrid(ref HistorydataGridView);
            WinObjMethods.ResizeGrid(ref ConnectionsdataGridView);
            CorrectWindowSize();

            #region Test_grid
            

            //WMIMethods methods = new WMIMethods();
            //List<Connection> dt = methods.GetNetworkDevices();

            //AddColumn(ref ConnectionsdataGridView);

            
            //if (dt.Count > 0)
            //{
            //    var bindsList = new BindingList<Connection>(dt); // <-- BindingList
            //    //Bind BindingList directly to the DataGrid
            //    var source = new BindingSource(bindsList, null);
            //    ConnectionsdataGridView.DataSource = source;
            //}
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
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
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DHCPServer",
                HeaderText = "DHCP сервер",
                DataPropertyName = "DHCPServer", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DNSDomain",
                HeaderText = "Основной DNS-суффикс",
                DataPropertyName = "DNSDomain", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "IPSubnetMask",
                HeaderText = "Маска подсети",
                DataPropertyName = "IPSubnetMask", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DefaultIPGateways",
                HeaderText = "Шлюзы ...",
                DataPropertyName = "DefaultIPGateways", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            dgv.Columns.Add(colName);

            colName = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "DNSServer",
                HeaderText = "DNS-серверы...",
                DataPropertyName = "DNSServer", // Tell the column which property it should use
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill
            };

            dgv.Columns.Add(colName);

        }

        private void BindConnectionGrid(ref DataGridView dgv)
        {            
            WMIMethods methods = new WMIMethods();
            List<Connection> connlist = methods.GetNetworkDevices();
            //List<DNS> dnslist = methods.GetDNSArray(connlist[0].Id);
            //List<Gateway> gtwlist = methods.GetGatewayArray(connlist[0].Id);

            AddColumn(ref dgv);

            if (connlist.Count > 0)
            {
                var bindsList = new BindingList<Connection>(connlist); // <-- BindingList
                //Bind BindingList directly to the DataGrid
                var source = new BindingSource(bindsList, null);
                dgv.DataSource = source;                                
            }

            //dgv.Columns.Add( GetDNSComboBox(connlist[0].Id) );
            //dgv.Columns.Add( GetGatewayComboBox(connlist[0].Id) );

            //dgv.Rows[0].Cells["DNSArray"].Value = "0";
            //dgv.Rows[0].Cells["GatewayArray"].Value = "0";

        }
        private DataGridViewComboBoxColumn GetDNSComboBox(int conn_id)
        {
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

            WMIMethods methods = new WMIMethods();
            List<DNS> dnslist = methods.GetDNSArray(conn_id);

            var bindsList = new BindingList<DNS>(dnslist); // <-- BindingList
            if (bindsList.Count > 0)
                cmb.DataSource = bindsList;        

            return cmb;
        }

        private DataGridViewComboBoxColumn GetGatewayComboBox(int conn_id)
        {
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn()
            {
                //CellTemplate = cell,
                Name = "GatewayArray",
                HeaderText = "Шлюзы",
                DataPropertyName = "Ip_Address", // Tell the column which property it should use
                DisplayMember = "Ip_Address",
                ValueMember = "Id",
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill,

            }; 
            WMIMethods methods = new WMIMethods();            
            List<Gateway> gtwlist = methods.GetGatewayArray(conn_id);

            var bindsList = new BindingList<Gateway>(gtwlist); // <-- BindingList
            if (bindsList.Count > 0)
                cmb.DataSource = bindsList;

            return cmb;
        }

        private void BindHistoryGrid(ref DataGridView dgv)
        {            
            DbMethods DB = new DbMethods();
            List<Connection> connlist = DB.ReadConnectionHistory();

            AddColumn(ref dgv);

            if (connlist.Count > 0)
            {
                var connnamesList = new BindingList<Connection>(connlist); // <-- BindingList
                //Bind BindingList directly to the DataGrid
                dgv.DataSource = connnamesList;
            }
            foreach (Connection conn in connlist)
            {
                //dgv.Columns.Add(GetDNSComboBox(conn.Id));
                //dgv.Columns.Add(GetGatewayComboBox(conn.Id));
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
            List<Ping> Ping_list = new List<Ping>();
  
            var PingForm = new TracertForm.PingForm();
            PingForm.Show();
        }

        private void TracerttoolStripButton_Click(object sender, System.EventArgs e)
        {            
            List<Tracert> Tracert_list = new List<Tracert>();            

            var TracertForm = new TracertForm.MainForm();
            TracertForm.Show();
        }

        private void ConnectionsdataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);

            if (e.ColumnIndex == this.ConnectionsdataGridView.Columns["DNSServer"].Index)
            {
                this.ConnectionsdataGridView.BeginInvoke(objChangeCellType, e.RowIndex);
                bIsComboBox = false;
            }
        }

        #region Test grid
        private void ChangeCellToComboBox(int iRowIndex)
        {
            if (bIsComboBox == false)
            {
                WMIMethods methods = new WMIMethods();
                List<DNS> dt = methods.GetDNSArray(0);

                DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
                dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
               
                if (dt.Count > 0)
                {
                    var bindsList = new BindingList<DNS>(dt);
                    var source = new BindingSource(bindsList, null);
                    dgComboCell.DataSource = source;
                }

                //dgComboCell.DataSource = dt;
                dgComboCell.ValueMember = "DNSServer";
                dgComboCell.DisplayMember = "DNSServer";

                ConnectionsdataGridView.Rows[iRowIndex].Cells[ConnectionsdataGridView.CurrentCell.ColumnIndex] = dgComboCell;
                bIsComboBox = true;
            }
        }

        private void ConnectionsdataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }
        #endregion

        //private void ChangeCellToComboBox(int iRowIndex)
        //{
        //    if (bIsComboBox == false)
        //    {
        //        DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
        //        dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

        //        List<DNS> dt = new List<DNS>();

        //        for (int i = 0; i < 5; i++)
        //        {
        //            DNS dr = new DNS();
        //            dr.DNSServer = "Name - " + i.ToString();
        //            dr.Id = i;
        //            dt.Add(dr);
        //        }
        //        if (dt.Count > 0)
        //        {
        //            var bindsList = new BindingList<DNS>(dt);
        //            var source = new BindingSource(bindsList, null);
        //            dgComboCell.DataSource = source;
        //        }

        //        //dgComboCell.DataSource = dt;
        //        dgComboCell.ValueMember = "DNSServer";
        //        dgComboCell.DisplayMember = "DNSServer";

        //        ConnectionsdataGridView.Rows[iRowIndex].Cells[ConnectionsdataGridView.CurrentCell.ColumnIndex] = dgComboCell;
        //        bIsComboBox = true;
        //    }
        //}

    }
}
