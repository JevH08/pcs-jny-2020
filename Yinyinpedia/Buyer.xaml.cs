using System;
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
using Oracle.DataAccess.Client;
using System.Data;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for Buyer.xaml
    /// </summary>
    public partial class Buyer : Window
    {
        string username, kode;
        OracleConnection conn;
        public Buyer(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            OracleCommand cmd;
            conn = new OracleConnection()
            {
                ConnectionString = "Data Source = ORCL; User Id = proyekpcs; password = proyekpcs;"
            };
            conn.Open();
            string query = " select saldo from mh_user where kode_user = '" + kode +"'";
            cmd = new OracleCommand(query, conn);
            saldo.Text = "SALDO ANDA : " + cmd.ExecuteScalar().ToString();
            conn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TopUp tu = new TopUp(username, kode);
            tu.Show();
            this.Close();
        }
    }
}
