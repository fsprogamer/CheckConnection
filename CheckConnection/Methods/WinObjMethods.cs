using System.Windows.Forms;

namespace CheckConnection.Methods
{
    public static class WinObjMethods
    {
        public const string ConnGridName = "ConnectionsdataGridView";
        public static int CountGridWidth(DataGridView dgv)
            {
                int width = 0;
                foreach (DataGridViewColumn column in dgv.Columns)
                    if (column.Visible == true)
                        width += column.Width;
                width += 100;
                return width/* += 20*/;
        }

        public static void ResizeGrid(ref DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count - 1; i++)
            {
                int colw = dgv.Columns[i].Width;
                dgv.Colum‌​ns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[i].Width = colw;
            }
        }

        public static void AddColumn(ref DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;

            //dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

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
                //Width = 100,
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
                Name = "IPGateway",
                HeaderText = "Шлюзы ...",
                DataPropertyName = "IPGateway", // Tell the column which property it should use
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
            //dgv.Columns["DNSServer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns["DNSServer"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public static DataGridView GetConnectionGrid()
        {
            DataGridView dgv = new DataGridView();
            dgv.Name = ConnGridName;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            dgv.AutoSize = true;

            AddColumn(ref dgv);
            return dgv;
        }
    }
}
