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
        OracleConnection conn;
        OracleCommand cmd;
        string username, kode;

        public Buyer(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            loadData();
            conn.Open();
            string query = "select nama_user from mh_user where username_user = '" + username + "'";
            cmd = new OracleCommand(query, conn);
            header.Text = "Welcome, Buyer " + cmd.ExecuteScalar().ToString();
            conn.Close();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select saldo from mh_user where username_user = '" + username + "'";
                cmd = new OracleCommand(query, conn);
                saldo.Text = "Rp " + cmd.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ProfileSeller prof = new ProfileSeller(username, kode);
            prof.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want to Log Out ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Shipping_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TopUp_Click(object sender, RoutedEventArgs e)
        {
            TopUp tu = new TopUp(username, kode);
            tu.Show();
            this.Close();
        }
    }
}
