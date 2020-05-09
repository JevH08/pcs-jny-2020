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
            header.Text = "WELCOME, SELLER " + username.ToUpper();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            loadData();
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

        private void Tarik_Click(object sender, RoutedEventArgs e)
        {
            if (saldo.Text != "Rp 0")
            {
                try
                {
                    conn.Open();
                    string query = "update mh_user set saldo = 0 where username_user = '" + username + "'";
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

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ProfileSeller prof = new ProfileSeller(username, kode);
            prof.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.ShowDialog();
            this.Close();
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            ProductSeller prod = new ProductSeller(username, kode);
            prod.ShowDialog();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
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
    }
}
