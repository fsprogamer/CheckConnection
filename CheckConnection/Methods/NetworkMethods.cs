﻿using System;
using System.Collections.Generic;
using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public partial class WMIMethods : WMIInterface
    {   
        public int SaveConnectionParam( ConnectionParam param )
        {
            int ret = 0;

            MObject objMO = new MObject(GetManagementObject(param.Connection.Name));

            if (objMO.IpEnabled())
            {
                if (param.Connection.DHCP_Enabled == "True")
                {
                    log.Info("Before setDinamicIP");
                    objMO.setDinamicIP();
                    log.Info("After setDinamicIP");
                }
                else
                {
                    log.Info("Before setStaticIP");
                    objMO.setStaticIP(param.Connection.Ip_Address_v4, param.Connection.IPSubnetMask);
                    log.Info("After setStaticIP");

                    log.Info("Before setDNSDomain");
                    if (!String.IsNullOrEmpty(param.Connection.DNSDomain))
                        objMO.setDNSDomain(param.Connection.DNSDomain);
                    log.Info("After setDNSDomain");

                    log.Info("Before setGateway");
                    List<string> sGateway = new List<string>(2);
                    if (param.Gateway_list != null)
                    {
                        if ((param.Gateway_list[0] != null) &&
                        (!String.IsNullOrEmpty(param.Gateway_list[0].IPGateway))
                        )
                        {
                            sGateway.Add(param.Gateway_list[0].IPGateway);
                            if (param.Gateway_list.Count > 1)
                            {
                                if ((param.Gateway_list[1] != null) &&
                                (!String.IsNullOrEmpty(param.Gateway_list[1].IPGateway))
                                )
                                    sGateway.Add(param.Gateway_list[1].IPGateway);
                            }
                            objMO.setGateway(sGateway.ToArray());
                        }
                    }
                    log.Info("After setGateway");
                }

                log.Info("Before SetDNSServerSearchOrder");
                
                List<string> sDns = new List<string>(2);
                if (param.DNS_list != null)
                {
                    if ((param.DNS_list[0] != null) &&
                    (!String.IsNullOrEmpty(param.DNS_list[0].DNSServer))
                    )
                    {
                        sDns.Add(param.DNS_list[0].DNSServer);
                        if (param.DNS_list.Count > 1)
                        {
                            if ((param.DNS_list[1] != null) &&
                                (!String.IsNullOrEmpty(param.DNS_list[1].DNSServer))
                                )
                                sDns.Add(param.DNS_list[1].DNSServer);
                        }
                        objMO.setDNSServerSearchOrder(sDns.ToArray());
                    }
                }
                else
                {//Удаляем все DNS, передаем пустышку
                    objMO.setDNSServerSearchOrder(sDns.ToArray());
                }
                log.Info("After SetDNSServerSearchOrder");

                ret = 1;     
            }
            return ret;
        }


        //public int setStaticIP(string ip_address, string subnet_mask)
        //{
        //    int ret = 0;
        //    foreach (ManagementObject objMO in moCollection)
        //    {
        //        if ((bool)objMO["IPEnabled"])
        //        {
        //            try
        //            {
        //                // Set IPAddress and Subnet Mask
        //                ManagementBaseObject newIP = objMO.GetMethodParameters("EnableStatic");
        //                newIP["IPAddress"] = new string[] { ip_address };
        //                newIP["SubnetMask"] = new string[] { subnet_mask };

        //                objMO.InvokeMethod("EnableStatic", newIP, null);

        //                ret = 1;

        //            }
        //            catch (Exception ex)
        //            {
        //                log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
        //                throw;
        //            }
        //        }
        //    }
        //    return ret;
        //}

        //public int setDinamicIP() 
        //{
        //    int ret = 0;
        //    foreach (ManagementObject objMO in moCollection)
        //    {
        //        if ((bool)objMO["IPEnabled"])
        //        {
        //            try
        //            {
        //                objMO.InvokeMethod("EnableDHCP", null);
        //                ret = 1;
        //            }
        //            catch (Exception ex)
        //            {
        //                log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
        //                throw;
        //            }
        //        }
        //    }
        //    return ret;
        //}

        ///// <summary> 
        ///// Set's a new Gateway address of the local machine 
        ///// </summary> 
        ///// <param name="gateway">The Gateway IP Address</param> 
        ///// <remarks>Requires a reference to the System.Management namespace</remarks> 
        //public int setGateway(string gateway)
        //{
        //    int ret = 0;
        //    foreach (ManagementObject objMO in moCollection)
        //    {
        //        if ((bool)objMO["IPEnabled"])
        //        {
        //            try
        //            {
        //                ManagementBaseObject setGateway;
        //                ManagementBaseObject newGateway =
        //                    objMO.GetMethodParameters("SetGateways");

        //                newGateway["DefaultIPGateway"] = new string[] { gateway };
        //                newGateway["GatewayCostMetric"] = new int[] { 1 };

        //                setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
        //                ret = 1;
        //            }
        //            catch (Exception ex)
        //            {
        //                log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
        //                throw;
        //            }
        //        }
        //    }
        //    return ret;
        //}
        ///// <summary> 
        ///// Set's the DNS Server of the local machine 
        ///// </summary> 
        ///// <param name="NIC">NIC address</param> 
        ///// <param name="DNS">DNS server address</param> 
        ///// <remarks>Requires a reference to the System.Management namespace</remarks> 
        //public int setDNS(string NIC, string DNS)
        //{
        //    int ret = 0;
        //    foreach (ManagementObject objMO in moCollection)
        //    {
        //        if ((bool)objMO["IPEnabled"])
        //        {
        //            if (objMO["Caption"].Equals(NIC))
        //            {
        //                try
        //                {
        //                    ManagementBaseObject newDNS =
        //                        objMO.GetMethodParameters("SetDNSServerSearchOrder");
        //                    newDNS["DNSServerSearchOrder"] = DNS.Split(',');
        //                    ManagementBaseObject setDNS =
        //                        objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
        //                    ret = 1;
        //                }
        //                catch (Exception ex)
        //                {
        //                    log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
        //                    throw;
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}
        ///// <summary> 
        ///// Set's WINS of the local machine 
        ///// </summary> 
        ///// <param name="NIC">NIC Address</param> 
        ///// <param name="priWINS">Primary WINS server address</param> 
        ///// <param name="secWINS">Secondary WINS server address</param> 
        ///// <remarks>Requires a reference to the System.Management namespace</remarks> 
        //public int setWINS(string NIC, string priWINS, string secWINS)
        //{
        //    int ret = 0;
        //    foreach (ManagementObject objMO in moCollection)
        //    {
        //        if ((bool)objMO["IPEnabled"])
        //        {
        //            if (objMO["Caption"].Equals(NIC))
        //            {
        //                try
        //                {
        //                    ManagementBaseObject setWINS;
        //                    ManagementBaseObject wins =
        //                    objMO.GetMethodParameters("SetWINSServer");
        //                    wins.SetPropertyValue("WINSPrimaryServer", priWINS);
        //                    wins.SetPropertyValue("WINSSecondaryServer", secWINS);

        //                    setWINS = objMO.InvokeMethod("SetWINSServer", wins, null);
        //                    ret = 1;
        //                }
        //                catch (Exception ex)
        //                {
        //                    log.ErrorFormat("Ошибка при изменении ip-адреса", ex);
        //                    throw;
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}

        //public void GetWeather()
        //{
        //    string res = string.Empty;

        //    // Создать объект запроса
        //    WebRequest request = WebRequest.Create("https://api.weather.yandex.ru/v1/locations ? lang=ru-RU");

        //    IWebProxy proxy = request.Proxy;
        //    if (proxy != null)
        //    {
        //        string proxyuri = proxy.GetProxy(request.RequestUri).ToString();
        //        request.UseDefaultCredentials = true;
        //        request.Proxy = new WebProxy(proxyuri, false);
        //        request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //    }

        //    // Получить ответ с сервера
        //    WebResponse response = request.GetResponse();

        //    // Получаем поток данных из ответа
        //    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        //    {
        //        // Выводим исходный код страницы
        //        string line;
        //        while ((line = stream.ReadLine()) != null)
        //            res += line + "\n";
        //    }
        //}
    }
}