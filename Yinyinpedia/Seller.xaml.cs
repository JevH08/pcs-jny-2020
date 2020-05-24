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

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for Seller.xaml
    /// </summary>
    public partial class Seller : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, kode;

        public Seller(string user, string kod)
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
            header.Text = "Welcome, Seller " + cmd.ExecuteScalar().ToString();
            conn.Close();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select saldo from mh_user where username_user = '" + username + "'";
                cmd = new OracleCommand(query, conn);
                saldo.Text = cmd.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Tarik_Click(object sender, RoutedEventArgs e)
        {
            if (saldo.Text != "0")
            {
                try
                {
                    conn.Open();
                    string query = "update mh_user set saldo = 0 where username_user = '" + username + "'";
                    cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    string keterangan = "Withdraw " + saldo.Text;
                    query = $"insert into history_emoney(fk_user, emoney, status, ket) values('{kode}', '{saldo.Text}', 0, '{keterangan}')";
                    cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successful Withdrawal");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.StackTrace);
                }
                loadData();
            }
            else
            {
                MessageBox.Show("Empty Balance");
            }
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            historyBalanceSeller hb = new historyBalanceSeller(username, kode);
            hb.Show();
            this.Close();
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
            ProductSeller prod = new ProductSeller(username, kode);
            prod.Show();
            this.Close();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            NewOrder no = new NewOrder(username, kode);
            no.Show();
            this.Close();
        }

        private void Shipping_Click(object sender, RoutedEventArgs e)
        {
            shippingSeller ss = new shippingSeller(username, kode);
            ss.Show();
            this.Close();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            chatSeller cs = new chatSeller(username, kode);
            cs.Show();
            this.Close();
        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {
            reportSeller sr = new reportSeller(username, kode);
            sr.Show();
            this.Close();
        }
    }
}
