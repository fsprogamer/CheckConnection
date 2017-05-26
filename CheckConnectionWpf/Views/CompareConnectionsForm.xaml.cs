using System;
using System.Collections.Generic;
using System.Windows;
using CheckConnection.Model;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CheckConnectionWpf.Views
{
    /// <summary>
    /// Interaction logic for CompareConnections.xaml
    /// </summary>
    public partial class CompareConnectionsForm : Window, ICompareConnectionsView
    {
        public CompareConnectionsForm()
        {
            InitializeComponent();
        }

        public void LoadActiveConnection(Connection connection)
        {
            var properties = typeof(Connection).GetProperties()
                                          .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
                                          .Select(p => new
                                          {
                                              PropertyName = p.Name,
                                              p.GetCustomAttributes(typeof(DisplayAttribute),
                                             false).Cast<DisplayAttribute>().Single().Name
                                          });

            List<string> propertyValueList = new List<string>();
            string value = string.Empty;
            foreach (var propinfo in properties)
            {
                value = string.Empty;
                try
                {
                    value = GetPropValue(connection, propinfo.PropertyName).ToString();
                }
                catch(Exception ex)
                {
                }
                propertyValueList.Add(value);                
            }
            listBoxActiveConnection.ItemsSource = propertyValueList;
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public void LoadHistoryConnection(Connection connection)
        {
            var properties = typeof(Connection).GetProperties()
                                          .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
                                          .Select(p => new
                                          {
                                              PropertyName = p.Name,
                                              p.GetCustomAttributes(typeof(DisplayAttribute),
                                             false).Cast<DisplayAttribute>().Single().Name
                                          });

            List<string> propertyValueList = new List<string>();

            string value = string.Empty;
            foreach (var propinfo in properties)
            {
                value = string.Empty;
                try
                {
                    value = GetPropValue(connection, propinfo.PropertyName).ToString();
                }
                catch (Exception ex)
                {
                }
                propertyValueList.Add(value);
            }
            listBoxHistoryConnection.ItemsSource = propertyValueList;
        }
    }
}
