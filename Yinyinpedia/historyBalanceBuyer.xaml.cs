﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Oracle.DataAccess.Client;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for historyBalanceBuyer.xaml
    /// </summary>
    public partial class historyBalanceBuyer : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        string username, kode;

        public historyBalanceBuyer(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            loadData();
        }

        public void loadData()
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Buyer b = new Buyer(username, kode);
            b.Show();
            this.Close();
        }
    }
}
