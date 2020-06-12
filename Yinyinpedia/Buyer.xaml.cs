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
            string query = "select initcap(nama_user) from mh_user where username_user = '" + username + "'";
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

        private void TopUp_Click(object sender, RoutedEventArgs e)
        {
            TopUp tu = new TopUp(username, kode,1);
            tu.Show();
            this.Close();
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            historyBalanceBuyer hb = new historyBalanceBuyer(username, kode);
            hb.Show();
            this.Close();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            profileBuyer prof = new profileBuyer(username, kode);
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
            productBuyer prod = new productBuyer(username, kode);
            prod.Show();
            this.Close();
        }

        private void Shipping_Click(object sender, RoutedEventArgs e)
        {
            shippingBuyer sb = new shippingBuyer(username, kode);
            sb.Show();
            this.Close();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            chatBuyer cb = new chatBuyer(username, kode);
            cb.Show();
            this.Close();
        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {
            reportBuyer rb = new reportBuyer(username, kode);
            rb.Show();
            this.Close();
        }
    }
}
