using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Management;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{

    public class WMINetworkAdapterManager : ClassWithLog, IWMINetworkAdapterManager
    {
        private readonly IWMINetworkAdapterRepo _repository;
        private readonly IWMIConnectionRepo _child_repository;

        public WMINetworkAdapterManager()
        {
            log.Info("WMINetworkAdapterManager, before");
            log.Info("WMINetworkAdapterRepo, before");
            _repository = new WMINetworkAdapterRepo();
            log.Info("WMINetworkAdapterRepo, after");
            log.Info("WMIConnectionRepo, before");
            _child_repository = new WMIConnectionRepo();
            log.Info("WMIConnectionRepo, after");
            log.Info("WMINetworkAdapterManager, after");
        }

        public IWMIManagementObjectRepo mo_repo
        {
            get { return _repository.mo_repo; }
            set { mo_repo = value; }
        }

        public IWMIManagementObjectRepo mo_con_repo
        {
            get { return _child_repository.mo_repo; }
            set { mo_con_repo = value; }
        }

        //public NetworkAdapter GetItem(Func<NetworkAdapter, bool> predicate)
        //{
        //    return _repository.GetItem(predicate);
        //}
        private void CopyFields(ref Connection conn, ref NetworkAdapter adapter)
        {
            conn.NetConnectionID = adapter.NetConnectionID;
            conn.NetConnectionStatus = adapter.NetConnectionStatus;
            conn.NetEnabled = adapter.NetEnabled;
            //conn.GUID = adapter.GUID;
        }

        public Connection GetItem(Func<Connection, bool> predicate)
        {
            Connection conn = null; 
            try
            {
                conn = _child_repository.GetItem(predicate);
                NetworkAdapter adapter = _repository.GetItem(p => p.Index == conn.Index);
                if ((conn != null) && (adapter != null))
                {
                    CopyFields( ref conn, ref adapter);
                }
            }
            catch(Exception ex)
            {
                log.Error("error GetItem",ex);
            }
            
            return conn;
        }

        //public ManagementObject GetMOItem(Func<ManagementObject, bool> predicate)
        //{
        //    return mo_repo.GetItem(predicate);
        //}

        //public ManagementObject GetConMOItem(Func<ManagementObject, bool> predicate)
        //{
        //    return mo_con_repo.GetItem(predicate);
        //}

        public List<Connection> GetItems()
        {
            List<Connection> connlist = new List<Connection>();
            foreach (NetworkAdapter adapter in _repository.GetItems())
            {
                foreach (Connection conn in _child_repository.GetItems())
                {
                    if (conn.Index == adapter.Index)
                    //if (conn.Name == adapter.Name)
                    {
                        conn.NetConnectionID = adapter.NetConnectionID;
                        conn.NetConnectionStatus = adapter.NetConnectionStatus;
                        conn.NetEnabled = adapter.NetEnabled;
                        //Читаем SettingID из NetworkAdapterConfiguration
                        //conn.GUID = adapter.GUID;

                        connlist.Add(conn);
                    }
                }
            }

            return connlist.OrderByDescending(p => p.NetConnectionID != null).ToList();
        }

        public List<Connection> GetItems(Func<NetworkAdapter, bool> predicate)
        {
            List<Connection> connlist = new List<Connection>();
            foreach (NetworkAdapter adapter in _repository.GetItems(predicate))
            {
                foreach (Connection conn in _child_repository.GetItems())
                {
                    if (conn.Index == adapter.Index)
                    //if (conn.Name == adapter.Name)
                    {
                        conn.NetConnectionID = adapter.NetConnectionID;
                        conn.NetConnectionStatus = adapter.NetConnectionStatus;
                        conn.NetEnabled = adapter.NetEnabled;
                        //Читаем SettingID из NetworkAdapterConfiguration
                        //conn.GUID = adapter.GUID;
                        connlist.Add(conn);
                    }
                }
            }

            return connlist.OrderByDescending(p => p.NetConnectionID != null).ToList();
        }

        public Connection GetItem(DataGridView dgv)
        {
            log.Info("before GetItem");

            int selectedRow = 0;
            if (dgv.SelectedRows.Count > 0)
                selectedRow = dgv.SelectedRows[0].Index;

            if (dgv.RowCount > 0)
            {
                string Name = dgv.Rows[selectedRow].Cells["Name"].Value.ToString();

                Connection conn = new Connection();

                conn.Name = Name;

                if (dgv.Rows[selectedRow].Cells["Ip_Address_v4"].Value != null)
                    conn.Ip_Address_v4 = dgv.Rows[selectedRow].Cells["Ip_Address_v4"].Value.ToString();

                if (dgv.Rows[selectedRow].Cells["IpSubnetMask"].Value != null)
                    conn.IPSubnetMask = dgv.Rows[selectedRow].Cells["IpSubnetMask"].Value.ToString();

                if (dgv.Rows[selectedRow].Cells["DNSDomain"].Value != null)
                    conn.DNSDomain = dgv.Rows[selectedRow].Cells["DNSDomain"].Value.ToString();

                if (dgv.Rows[selectedRow].Cells["DHCP_Enabled"].Value != null)
                    conn.DHCP_Enabled = dgv.Rows[selectedRow].Cells["DHCP_Enabled"].Value.ToString();

                if (dgv.Rows[selectedRow].Cells["DNSServer"].Value != null)
                    conn.DNS_list = setDNSServerSearchOrder(dgv.Rows[selectedRow].Cells["DNSServer"].Value.ToString());

                if (dgv.Rows[selectedRow].Cells["IPGateway"].Value != null)
                    conn.Gateway_list = setGateway(dgv.Rows[selectedRow].Cells["IPGateway"].Value.ToString());

                return conn;
            }
            log.Info("after GetItem");
            return null;
        }

        public List<DNS> setDNSServerSearchOrder(string strdns)
        {
            int i = 0;
            List<DNS> DNS_list = null;
            log.Info("before setDNSServerSearchOrder");
            if (!string.IsNullOrEmpty(strdns))
            {
                DNS_list = new List<DNS>(2);
                string[] dns_array = strdns.Split(';');
                foreach (string dns in dns_array)
                {
                    DNS_list.Add(new DNS { DNSServer = dns, Order_Id = i });
                    i++;
                }
            }
            log.Info("after setDNSServerSearchOrder");
            return DNS_list;
        }
        public List<Gateway> setGateway(string strgateway)
        {
            List<Gateway> Gateway_list = null;
            log.Info("before setGateway");
            if (!string.IsNullOrEmpty(strgateway))
            {
                Gateway_list = new List<Gateway>(2);
                string[] gateway_array = strgateway.Split(';');
                foreach (string gateway in gateway_array)
                {
                    Gateway_list.Add(new Gateway { IPGateway = gateway });
                }
            }
            log.Info("after setGateway");
            return Gateway_list;
        }

    }
}
