﻿using System;
using System.Windows;
using CheckConnection.Model;
using System.Windows.Data;

namespace CheckConnectionWpf.Views
{
    /// <summary>
    /// Interaction logic for ChangeConnectionForm.xaml
    /// </summary>
    public partial class ChangeConnectionForm : Window, IChangeConnectionView
    {
        //public Connection connection { get; set; }
        public ChangeConnectionForm()
        {
            InitializeComponent();
        }
        public void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
       
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
           
            Connection conn = DataContext as Connection;
        }

        public void LoadConnection(Connection connection)
        {
            this.DataContext = connection;
        }        
    }

    //public class YesNoToBooleanConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        switch (value.ToString())
    //        {
    //            case "True":                
    //                return true;
    //            case "False":                
    //                return false;
    //        }
    //        return false;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        if (value is bool)
    //        {
    //            if ((bool)value == true)
    //                return "True";
    //            else
    //                return "False";
    //        }
    //        return "no";
    //    }
    //}

}