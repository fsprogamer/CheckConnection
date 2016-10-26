using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;
using CheckConnection.Methods;
using CheckConnection.Model;
using log4net;

namespace CheckConnection
{
    public partial class DisplayConnections : Form
    {
        delegate void SetComboBoxCellType(int iRowIndex);
        bool bIsComboBox = false;
        private DBInterface db;
        private WMIInterface wmi;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool FormLoadComplete = false;

        public DisplayConnections(DBInterface dbparam, WMIInterface wmiparam)
        {
            db = dbparam;
            wmi = wmiparam;
            InitializeComponent();           
        }

        private void DisplayConnections_Load(object sender, System.EventArgs e)
        {
            ConnectionsdataGridView.Name = WinObjMethods.ConnGridName;

            WinObjMethods.AddColumn(ref ConnectionsdataGridView);
            BindConnectionGrid(ref ConnectionsdataGridView);

            ConnectionsdataGridView.DefaultCellStyle.WrapMode=DataGridViewTriState.True;
            ConnectionsdataGridView.AutoSizeRowsMode=DataGridViewAutoSizeRowsMode.AllCells;

            WinObjMethods.AddColumn(ref HistorydataGridView);

            string SelectedConnectionName = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");
            if (!String.IsNullOrEmpty(SelectedConnectionName))
            {
                BindHistoryGrid(ref HistorydataGridView, SelectedConnectionName);

                HistorydataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                HistorydataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                HistorydataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                WinObjMethods.ResizeGrid(ref ConnectionsdataGridView);
                CorrectWindowSize();

                if (HistorydataGridView.Rows.Count > 0)
                    HistorydataGridView.Rows[0].Selected = true;
                int widthScreen = Screen.PrimaryScreen.WorkingArea.Width;
                int x = widthScreen - this.ClientSize.Width;
                int heightScreen = Screen.PrimaryScreen.WorkingArea.Height;
                int y = heightScreen - this.ClientSize.Height;
                this.Location = new Point((x / 2), (y / 2));

            }
            FormLoadComplete = true;
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
   
        private void BindConnectionGrid(ref DataGridView dgv)
        {
            List<Connection> connlist = wmi.GetNetworkDevicesList();            
            //dgv.Columns.Add( GetDNSComboBox(connlist[0].Id) );
            //dgv.Columns.Add( GetGatewayComboBox(connlist[0].Id) );
            //SetDNSComboBox(ref ConnectionsdataGridView);

            if (connlist.Count > 0)
            {
                var bindsList = new BindingList<Connection>(connlist);
                //Bind BindingList directly to the DataGrid
                var source = new BindingSource(bindsList, null);
                dgv.DataSource = source;
            }

        }
        private void SetTextBox(ref DataGridView dgv)
        {            
            List<ConnectionParam> connlist = wmi.GetNetworkDevices();

            //Добавление данных
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell cell_txt = new DataGridViewTextBoxCell();
            //Вписываем текст в заголовок строки
            row.HeaderCell.Value = "1";
            row.CreateCells(dgv);
            //Создаем массив который будет помещен в ячейку
            cell_txt.Value =
            row.Cells[1] = cell_txt;

            //выбираем строчку по умолчанию - здесь выбрана под номером 2.
            row.Cells[1].Value = (row.Cells[1] as DataGridViewComboBoxCell).Items[2];
            //Вносим в dataGridView
            dgv.Rows.Add(row);
        }
        private void SetDNSComboBox(ref DataGridView dgv)
        {
            //Добавление столбца -тип ComboBox
           //DataGridViewColumn column1 = new DataGridViewColumn();
           // DataGridViewCell cell1 = new DataGridViewComboBoxCell();
           // column1.HeaderText = "secondcolumn";
           // column1.Name = "secondcolumn";
           // column1.CellTemplate = cell1;
           // dgv.Columns.Add(column1);

            //Добавление данных
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewComboBoxCell cell_CB = new DataGridViewComboBoxCell();
            //Вписываем текст в заголовок строки
            row.HeaderCell.Value = "1";
            row.CreateCells(dgv);
            //Создаем массив который будет помещен в ячейку
            cell_CB.Items.AddRange(new string[] { "Первый", "Второй", "Третий", "Четвертый" });
            row.Cells[1] = cell_CB;

            //выбираем строчку по умолчанию - здесь выбрана под номером 2.
            row.Cells[1].Value = (row.Cells[1] as DataGridViewComboBoxCell).Items[2];
            //Вносим в dataGridView
            dgv.Rows.Add(row);
        }

        private DataGridViewComboBoxColumn GetDNSComboBox(int conn_id)
        {            
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn()
            {
                //CellTemplate = cell,
                Name = "DNSServer",
                HeaderText = "DNS-серверы",
                DataPropertyName = "DNSServer", // Tell the column which property it should use
                DisplayMember = "DNSServer",
                ValueMember = "DNSServer",
                //Width = 120,
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells,
            }; 
            
            List<DNS> dnslist = wmi.GetDNSArray(conn_id);

            if (dnslist.Count > 0)
            {
                var bindsList = new BindingList<DNS>(dnslist); 
                var source = new BindingSource(bindsList, null);
                cmb.DataSource = source;
            }        

            return cmb;
        }

        private DataGridViewComboBoxColumn GetGatewayComboBox(int conn_id)
        {
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn()
            {
                //CellTemplate = cell,
                Name = "IPGateway",
                HeaderText = "Шлюзы",
                DataPropertyName = "IPGateway", // Tell the column which property it should use
                DisplayMember = "IPGateway",
                ValueMember = "IPGateway",
                //Width = 120,
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill,

            }; 
                      
            List<Gateway> gtwlist = wmi.GetGatewayArray(conn_id);

            if (gtwlist.Count > 0)
            {
                var bindsList = new BindingList<Gateway>(gtwlist); 
                var source = new BindingSource(bindsList, null);
                cmb.DataSource = source;
            }

            return cmb;
        }

        private void BindHistoryGrid(ref DataGridView dgv, string name)
        {            
            List<Connection> connlist = db.ReadConnectionHistory().Where(p => p.Name == name).ToList<Connection>();               

            foreach (Connection conn in connlist)
            {                
                List<DNS> dnslist = db.ReadDNSHistory(conn.Id);
                List<Gateway> gtwlist = db.ReadGatewayHistory(conn.Id);

                if (dnslist.Count > 0)
                {
                    foreach (DNS dns in dnslist)
                    {
                        conn.DNSServer += dns.DNSServer + "; ";
                    }
                    if (conn.DNSServer.Length > 2)
                        conn.DNSServer = conn.DNSServer.Substring(0, conn.DNSServer.Length - 2);
                }
                if (gtwlist.Count > 0)
                {
                    foreach (Gateway gtw in gtwlist)
                    {
                        conn.IPGateway += gtw.IPGateway + "; ";
                    }
                    if (conn.IPGateway.Length > 2)
                        conn.IPGateway = conn.IPGateway.Substring(0, conn.IPGateway.Length - 2);
                }

            }

            if (connlist.Count > 0)
            {
                var bindsList = new BindingList<Connection>(connlist);
                //Bind BindingList directly to the DataGrid
                var source = new BindingSource(bindsList, null);
                dgv.DataSource = source;
            }
            else
            {
                dgv.DataSource = null;
                dgv.Refresh();
            }

            WinObjMethods.ResizeGrid(ref dgv);
            CorrectWindowSize();            
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);            
            List<ConnectionParam> connparam = wmi.GetNetworkDevices();

            if (connparam.Count > 0)            
                db.SaveConnectionTable(connparam);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
        }
        public void CorrectWindowSize()
        {
            int width = WinObjMethods.CountGridWidth(ConnectionsdataGridView);
            ClientSize = new Size(width, ClientSize.Height);
        }

        private void PingtoolStripButton_Click(object sender, System.EventArgs e)
        {
            var PingForm = new PingForm.MainPingForm();
            PingForm.StartPosition=FormStartPosition.CenterScreen;
            PingForm.Show();
        }

        private void TracerttoolStripButton_Click(object sender, System.EventArgs e)
        {            
            List<Tracert> Tracert_list = new List<Tracert>();            

            var TracertForm = new TracertForm.MainForm();
            TracertForm.StartPosition=FormStartPosition.CenterScreen;
            TracertForm.Show();
        }

        private void ConnectionsdataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);

            //if (e.ColumnIndex == this.ConnectionsdataGridView.Columns["DNSServer"].Index)
            //{
            //    this.ConnectionsdataGridView.BeginInvoke(objChangeCellType, e.RowIndex);
            //    bIsComboBox = false;
            //}
        }

        #region Test grid
        private void ChangeCellToComboBox(string conndesc, int iRowIndex)
        {
            if (bIsComboBox == false)
            {                
                List<DNS> dt = wmi.GetDNSArray(iRowIndex);

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

        private void ComparetoolStripButton_Click(object sender, System.EventArgs e)
        {
            if (HistorydataGridView.DataSource == null)
            {
                var message = MessageBox.Show("Нет данных для сравнения", "Ошибка подключения", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else
            {
                int rowIndex = -1;
                int count = 0;
                if (CheckSelectRow(ref rowIndex))
                {
                    for (int i = 1; i < ConnectionsdataGridView.ColumnCount; i++)
                    {
                        if ((HistorydataGridView.Rows[rowIndex].Cells[i].Value != null)&&(ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Value != null))
                        {
                            if (
                                !ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Value.ToString()
                                    .Equals(HistorydataGridView.Rows[rowIndex].Cells[i].Value.ToString()))
                            {
                                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = Color.LightGray;
                                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = Color.Red;
                            }
                            else
                            {
                                count++;
                            }
                        }
                        else
                        {
                            if (ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Value == null)
                            {
                                count++;
                            }
                            else
                            {
                                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = Color.LightGray;
                                ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = Color.Red;
                            }
                        }
                    }
                }
                if (count == (ConnectionsdataGridView.ColumnCount-1))
                {
                    for (int i = 1; i < ConnectionsdataGridView.ColumnCount; i++)
                    {
                        ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = ConnectionsdataGridView.DefaultCellStyle.SelectionBackColor;
                        ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = ConnectionsdataGridView.DefaultCellStyle.SelectionForeColor;
                    }

                    var message = MessageBox.Show("Параметры подключений совпадают", "Проверка совпадений", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ViewComparetoolStripButton_Click(object sender, System.EventArgs e)
        {
            if (HistorydataGridView.DataSource == null)
            {
                var message = MessageBox.Show("Нет данных для сравнения", "Ошибка подключения", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                
                int rowIndex = -1;
                if (CheckSelectRow(ref rowIndex))
                {
                    CompareConnections compareForm = new CompareConnections();
                    compareForm.StartPosition=FormStartPosition.CenterScreen;
                    compareForm.NewDataTable(ConnectionsdataGridView, HistorydataGridView.Rows[rowIndex]);
                    compareForm.Show();
                }
            }
        }

        private bool CheckSelectRow(ref int rowSelectIndex)
        {
            log.Info("HistorydataGridView.SelectedRows.Count="+ HistorydataGridView.SelectedRows.Count.ToString());

            if (HistorydataGridView.SelectedRows.Count == 0)
            {
                log.Info("HistorydataGridView.SelectedCells[0].RowIndex=" + HistorydataGridView.SelectedCells[0].RowIndex.ToString());
                int rowIndex = HistorydataGridView.SelectedCells[0].RowIndex;
                int count = 0;
                foreach (DataGridViewCell cell in HistorydataGridView.SelectedCells)
                {
                    if (cell.RowIndex == rowIndex)
                    {
                        count++;
                    }
                }
                if ((count == HistorydataGridView.SelectedCells.Count) && (count == HistorydataGridView.ColumnCount))
                {
                    rowSelectIndex = rowIndex;
                    return true;
                }
                else
                {
                    var message = MessageBox.Show("Выберите одну строку для сравнения", "Ошибка подключения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                log.Info("HistorydataGridView.SelectedRows.Count=" + HistorydataGridView.SelectedRows.Count.ToString());

                if (HistorydataGridView.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Выберите только одну строку для сравнения", "Ошибка подключения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    rowSelectIndex = HistorydataGridView.SelectedRows[0].Index;
                    return true;
                }
            }
        }

        private void toolStripButtonChangeConnection_Click(object sender, System.EventArgs e)
        {
            int selectedrow = GetSelectedRow(ConnectionsdataGridView);           

            if ( ConnectionsdataGridView.Rows[selectedrow].Cells["Name"].Value != null) {
                string Name = ConnectionsdataGridView.Rows[selectedrow].Cells["Name"].Value.ToString();            
                var ChangeConnectionForm = new ChangeConnectionForm(wmi, Name);

                ChangeConnectionForm.StartPosition = FormStartPosition.CenterScreen;
                ChangeConnectionForm.Show();
            }
            else
            {
                log.Info("Соединение с таким наименованием отсутствуют");
                MessageBox.Show("Соединение с таким наименованием отсутствуют", "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonRefresh_Click(object sender, System.EventArgs e)
        {
            wmi.GetNetworkDevicesConfig();
            BindConnectionGrid(ref ConnectionsdataGridView);
        }

        int GetSelectedRow(DataGridView dgv)
        {
            int selectedrow = 0;
            if (dgv.SelectedRows.Count > 0)
                selectedrow = dgv.SelectedRows[0].Index;            
            return selectedrow;
        }

        string GetSelectedConnectionParam(DataGridView dgv, string paramname)
        {
            int selectedrow = GetSelectedRow(dgv);
            string Name = string.Empty;
            if (ConnectionsdataGridView.Rows[selectedrow].Cells[paramname].Value != null)
                Name = ConnectionsdataGridView.Rows[selectedrow].Cells[paramname].Value.ToString();
            return Name;
        }

        private void ConnectionsdataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (FormLoadComplete)
            {
                string name = GetSelectedConnectionParam(ConnectionsdataGridView, "Name");
                if (!String.IsNullOrEmpty(name))
                    BindHistoryGrid(ref HistorydataGridView, name);

                for (int i = 1; i < ConnectionsdataGridView.ColumnCount; i++)
                {
                    if (ConnectionsdataGridView.SelectedRows.Count > 0)
                    {
                        ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionBackColor = ConnectionsdataGridView.DefaultCellStyle.SelectionBackColor;
                        ConnectionsdataGridView.Rows[ConnectionsdataGridView.SelectedRows[0].Index].Cells[i].Style.SelectionForeColor = ConnectionsdataGridView.DefaultCellStyle.SelectionForeColor;
                    }
                }

            }
        }

        private void toolStripButtonRestore_Click(object sender, EventArgs e)
        {
            int selectedRow = GetSelectedRow(ConnectionsdataGridView);

            if (ConnectionsdataGridView.Rows[selectedRow].Cells["Name"].Value != null)
            {
                string Name = ConnectionsdataGridView.Rows[selectedRow].Cells["Name"].Value.ToString();
                int selectedHistoryRow = GetSelectedRow(HistorydataGridView);

                ConnectionParam connparam = new ConnectionParam();
                connparam.Connection = new Connection();

                //Прописываем название подключения, для которого изменяются параметы
                connparam.Connection.Name = Name;

                if (HistorydataGridView.Rows[selectedHistoryRow].Cells["Ip_Address_v4"].Value != null)
                    connparam.Connection.Ip_Address_v4 = HistorydataGridView.Rows[selectedHistoryRow].Cells["Ip_Address_v4"].Value.ToString();

                if (HistorydataGridView.Rows[selectedHistoryRow].Cells["IpSubnetMask"].Value != null)
                    connparam.Connection.IPSubnetMask = HistorydataGridView.Rows[selectedHistoryRow].Cells["IpSubnetMask"].Value.ToString();

                if(HistorydataGridView.Rows[selectedHistoryRow].Cells["DNSDomain"].Value!=null)
                    connparam.Connection.DNSDomain = HistorydataGridView.Rows[selectedHistoryRow].Cells["DNSDomain"].Value.ToString();

                if(HistorydataGridView.Rows[selectedHistoryRow].Cells["DHCP_Enabled"].Value!=null)
                    connparam.Connection.DHCP_Enabled = HistorydataGridView.Rows[selectedHistoryRow].Cells["DHCP_Enabled"].Value.ToString();

                if (HistorydataGridView.Rows[selectedHistoryRow].Cells["DNSServer"].Value != null)
                    connparam.setDNSServerSearchOrder(HistorydataGridView.Rows[selectedHistoryRow].Cells["DNSServer"].Value.ToString());

                if (HistorydataGridView.Rows[selectedHistoryRow].Cells["IPGateway"].Value != null)
                    connparam.setGateway(HistorydataGridView.Rows[selectedHistoryRow].Cells["IPGateway"].Value.ToString());

                var ChangeConnectionForm = new ChangeConnectionForm(wmi, connparam );

                ChangeConnectionForm.StartPosition = FormStartPosition.CenterScreen;
                ChangeConnectionForm.Show();
            }
            else
            {
                log.Info("Соединение с таким наименованием отсутствуют");
                MessageBox.Show("Соединение с таким наименованием отсутствуют", "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

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
