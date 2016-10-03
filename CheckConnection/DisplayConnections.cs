using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using CheckConnection.Methods;
using CheckConnection.Model;
using PingForm;

namespace CheckConnection
{
    public partial class DisplayConnections : Form
    {
        delegate void SetComboBoxCellType(int iRowIndex);
        bool bIsComboBox = false;
        private DBInterface db;
        private WMIInterface wmi;        

        public DisplayConnections(DBInterface dbparam, WMIInterface wmiparam)
        {
            db = dbparam;
            wmi = wmiparam;
            InitializeComponent();
        }

        private void DisplayConnections_Load(object sender, System.EventArgs e)
        {
            ConnectionsdataGridView.Name = WinObjMethods.ConnGridName;
            BindConnectionGrid(ref ConnectionsdataGridView);
            ConnectionsdataGridView.DefaultCellStyle.WrapMode=DataGridViewTriState.True;
            ConnectionsdataGridView.AutoSizeRowsMode=DataGridViewAutoSizeRowsMode.AllCells;
            BindHistoryGrid(ref HistorydataGridView);
            HistorydataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            HistorydataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            HistorydataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            WinObjMethods.ResizeGrid(ref ConnectionsdataGridView);
            CorrectWindowSize();

            if(HistorydataGridView.Rows.Count>0)
                HistorydataGridView.Rows[0].Selected = true;
            int widthScreen = Screen.PrimaryScreen.WorkingArea.Width;
            int x = widthScreen - this.ClientSize.Width;
            int heightScreen = Screen.PrimaryScreen.WorkingArea.Height;
            int y = heightScreen - this.ClientSize.Height;
            this.Location = new Point((x / 2), (y / 2));

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
   
        private void BindConnectionGrid(ref DataGridView dgv)
        {                        
            List<Connection> connlist = wmi.GetNetworkDevices();

            WinObjMethods.AddColumn(ref dgv);
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
            List<Connection> connlist = wmi.GetNetworkDevices();

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

        private void BindHistoryGrid(ref DataGridView dgv)
        {            
            //DbMethods DB = new DbMethods();
            List<Connection> connlist = db.ReadConnectionHistory();

            WinObjMethods.AddColumn(ref dgv);

            foreach (Connection conn in connlist)
            {
                List<DNS> dnslist = db.ReadDNSHistory(conn.Id);
                List<Gateway> gtwlist = db.ReadGatewayHistory(conn.Id);

                foreach (DNS dns in dnslist) { 
                    conn.DNSServer = conn.DNSServer + dns.DNSServer + "; ";
                }
                conn.DNSServer = conn.DNSServer.Substring(0, conn.DNSServer.Length - 2);

                foreach (Gateway gtw in gtwlist)
                {
                    conn.IPGateway = conn.IPGateway + gtw.IPGateway + "; ";
                }
                conn.IPGateway = conn.IPGateway.Substring(0, conn.IPGateway.Length - 2);
            }

            if (connlist.Count > 0)
            {
                var bindsList = new BindingList<Connection>(connlist);
                //Bind BindingList directly to the DataGrid
                var source = new BindingSource(bindsList, null);
                dgv.DataSource = source;
            }

            WinObjMethods.ResizeGrid(ref dgv);
            CorrectWindowSize();            
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            List<Connection> Connection_list = wmi.GetNetworkDevices();
            List<DNS> DNS_list = wmi.GetDNSArray(Connection_list[0].Id);
            List<Gateway> Gateway_list = wmi.GetGatewayArray(Connection_list[0].Id);

            if (Connection_list.Count > 0)            
                db.SaveConnectionTable(Connection_list, DNS_list, Gateway_list);

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
            //List<Ping> Ping_list = new List<Ping>();

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
        private void ChangeCellToComboBox(int iRowIndex)
        {
            if (bIsComboBox == false)
            {                
                List<DNS> dt = wmi.GetDNSArray(0);

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
                        if (HistorydataGridView.Rows[rowIndex].Cells[i].Value != null)
                        {
                            if (
                                !ConnectionsdataGridView.Rows[0].Cells[i].Value.ToString()
                                    .Equals(HistorydataGridView.Rows[rowIndex].Cells[i].Value.ToString()))
                            {
                                ConnectionsdataGridView.Rows[0].Cells[i].Style.BackColor = Color.LightGray;
                                ConnectionsdataGridView.Rows[0].Cells[i].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                count++;
                            }
                        }
                        else
                        {
                            if (ConnectionsdataGridView.Rows[0].Cells[i].Value == null)
                            {
                                count++;
                            }
                            else
                            {
                                ConnectionsdataGridView.Rows[0].Cells[i].Style.BackColor = Color.LightGray;
                                ConnectionsdataGridView.Rows[0].Cells[i].Style.ForeColor = Color.Red;
                            }
                        }
                    }
                }
                if (count == (ConnectionsdataGridView.ColumnCount-1))
                {
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
            if (HistorydataGridView.SelectedRows.Count == 0)
            {
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
                if (HistorydataGridView.SelectedRows.Count > 1)
                {
                    var message = MessageBox.Show("Выберите только одну строку для сравнения", "Ошибка подключения",
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
