using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using CheckConnection.Model;
using Common;

namespace CheckConnection.Methods
{

    public class WMINetworkAdapterManager : ClassWithLogger<WMINetworkAdapterManager>, IWMINetworkAdapterManager
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

        //public List<Connection> GetItems(Func<NetworkAdapter, bool> predicate)
        //{
        //    List<Connection> lst = new List<Connection>();

        //    try
        //    {
        //        lst = (from conn in _child_repository.GetItems()
        //           join adapter in _repository.GetItems(predicate) on conn.Index equals adapter.Index
        //           orderby !string.IsNullOrEmpty(adapter.NetConnectionID) descending
        //           select new Connection()
        //           {
        //               Id = conn.Id,
        //               Date = conn.Date,
        //               NetConnectionID = adapter.NetConnectionID,
        //               Name = conn.Name,
        //               MAC = conn.MAC,
        //               Ip_Address_v4 = conn.Ip_Address_v4,
        //               Ip_Address_v6 = conn.Ip_Address_v6,
        //               DHCP_Enabled = conn.DHCP_Enabled,
        //               DHCPServer = conn.DHCPServer,
        //               DNSDomain = conn.DNSDomain,
        //               IPSubnetMask = conn.IPSubnetMask,
        //               DNS_list = conn.DNS_list,
        //               Gateway_list = conn.Gateway_list,
        //               NetConnectionStatus = adapter.NetConnectionStatus,
        //               NetEnabled = adapter.NetEnabled,
        //               Index = conn.Index,
        //               GUID = conn.GUID
        //           }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("error GetItems", ex);
        //    }

        //    return lst;
        //}

        public List<Connection> GetItems(Func<NetworkAdapter, bool> predicate)
        {
            List<Connection> connlist = new List<Connection>();
            foreach (NetworkAdapter adapter in _repository.GetItems(predicate))
            {
                foreach (Connection conn in _child_repository.GetItems())
                {
                    if (conn.Index == adapter.Index)
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
                conn.Ip_Address_v4 = dgv.Rows[selectedRow].Cells["Ip_Address_v4"].Value?.ToString();
                conn.IPSubnetMask = dgv.Rows[selectedRow].Cells["IpSubnetMask"].Value?.ToString();
                conn.DNSDomain = dgv.Rows[selectedRow].Cells["DNSDomain"].Value?.ToString();
                conn.DHCP_Enabled = dgv.Rows[selectedRow].Cells["DHCP_Enabled"].Value?.ToString();
                conn.DNS_list = setDNSServerSearchOrder(dgv.Rows[selectedRow].Cells["DNSServer"].Value?.ToString());
                conn.Gateway_list = setGateway(dgv.Rows[selectedRow].Cells["IPGateway"].Value?.ToString());

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
