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
using System.Data;
using Oracle.DataAccess.Client;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for cartBuyer.xaml
    /// </summary>
    public partial class cartBuyer : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, kode, kodeHTrans;
        string[] kotas = { "Surabaya", "Aceh", "Mojokerto", "Bali", "Jakarta", "Semarang", "Yogyakarta", "Solo", "Bandung", "Kediri" };

        private class City
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }

        public cartBuyer(string user, string kod, string kodeH)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            kodeHTrans = kodeH;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            loadData();
        }

        public void loadData()
        {
            List<City> kota = new List<City>();
            for (int i = 0; i < kotas.Count(); i++)
            {
                City temp = new City();
                temp.Kode = kotas[i];
                temp.Nama = kotas[i];
                kota.Add(temp);
            }
            city.Items.Clear();
            city.ItemsSource = null;
            city.SelectedValuePath = "Kode";
            city.DisplayMemberPath = "Nama";
            city.ItemsSource = kota;

            try
            {
                conn.Open();
                string query = "select nama_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                name.Text = cmd.ExecuteScalar().ToString();
                query = "select telepon_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                phoneNumber.Text = cmd.ExecuteScalar().ToString();
                query = "select alamat_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                address.Text = cmd.ExecuteScalar().ToString();
                query = "select kota_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                string tempa = cmd.ExecuteScalar().ToString();
                city.SelectedValue = tempa;

                shipping.ItemsSource = null;
                query = "select * from mh_distributor where status = 0 order by nama_distributor";
                cmd = new OracleCommand(query, conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    List<City> del = new List<City>();
                    while (reader.Read())
                    {
                        City temps = new City();
                        temps.Kode = reader.GetString(0);
                        temps.Nama = reader.GetString(1);
                        del.Add(temps);
                    }
                    shipping.SelectedValuePath = "Kode";
                    shipping.DisplayMemberPath = "Nama";
                    shipping.ItemsSource = del;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Shipping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && phoneNumber.Text != "" && address.Text != "" && city.SelectedIndex != -1 && shipping.SelectedIndex != -1)
            {
                MessageBox.Show("Transaction Success !");
                Buyer b = new Buyer(username, kode);
                b.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Data Input Is Required");
            }
        }
    }
}
