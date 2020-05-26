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
        int berattotal = 0;
        string[] kotas = { "Surabaya", "Aceh", "Mojokerto", "Bali", "Jakarta", "Semarang", "Yogyakarta", "Solo", "Bandung", "Kediri" };
        List<Delivery> d;
        int tot = 0,tot1 = 0;

        private class City
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }

        private class Delivery
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
            public int total { get; set; }
            public int diskon { get; set; }
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
                query = "select subtotal from htrans where kode_htrans = '" + kodeHTrans + "'";
                cmd = new OracleCommand(query, conn);
                grandtotal.Text = cmd.ExecuteScalar().ToString();
                tot = Convert.ToInt32(grandtotal.Text);
                query = "select berat from htrans where kode_htrans = '" + kodeHTrans + "'";
                cmd = new OracleCommand(query, conn);
                berattotal = Convert.ToInt32(cmd.ExecuteScalar().ToString());
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
                    d = new List<Delivery>();
                    while (reader.Read())
                    {
                        Delivery temps = new Delivery();
                        temps.Kode = reader["kode_distributor"].ToString();
                        temps.Nama = reader["nama_distributor"].ToString() + " - " + berattotal * Convert.ToInt32(reader["harga_per_kilo"].ToString());
                        temps.total = berattotal * Convert.ToInt32(reader["harga_per_kilo"].ToString());
                        if (berattotal >= Convert.ToInt32(reader["batas_harga"].ToString()))
                        {
                            temps.diskon = berattotal * Convert.ToInt32(reader["harga_per_kilo"].ToString()) * Convert.ToInt32(reader["diskon"].ToString()) / 100;
                        }
                        else
                        {
                            temps.diskon = 0;
                        }
                        d.Add(temps);
                    }
                    shipping.SelectedValuePath = "Kode";
                    shipping.DisplayMemberPath = "Nama";
                    shipping.ItemsSource = d;
                }
                
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Topup_Click(object sender, RoutedEventArgs e)
        {
            TopUp tu = new TopUp(username, kode, 2, kodeHTrans);
            tu.Show();
            this.Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (shipping.SelectedIndex == -1 || name.Text == "" || phoneNumber.Text == "" || city.SelectedIndex ==-1 || address.Text == "")
            {
                MessageBox.Show("Invalid Data");
            }
            else
            {
                conn.Open();
                string query = "select saldo from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                int saldo = Convert.ToInt32( cmd.ExecuteScalar().ToString());
                conn.Close();

                if (saldo < Convert.ToInt32(grandtotal.Text))
                {
                    MessageBox.Show("Not Enough Balance");
                }
                else
                {
                    saldo -= Convert.ToInt32(grandtotal.Text);
                    OracleCommand cmd = new OracleCommand("UPDATE htrans SET shipping = :hargaship, promo = :diskonship, grandtotal = :total, fk_distributor = :fk, nama_tujuan = :nama,kota_tujuan = :kota,alamat_tujuan = :alamat,telepon_tujuan = :tel WHERE kode_htrans = :kode", conn);

                    cmd.Parameters.Add(":hargaship", d[shipping.SelectedIndex].total);
                    cmd.Parameters.Add(":diskonship", promoDelivery.Text);
                    cmd.Parameters.Add(":total", grandtotal.Text);
                    cmd.Parameters.Add(":fk", shipping.SelectedValue);
                    cmd.Parameters.Add(":nama", name.Text);
                    cmd.Parameters.Add(":kota", city.SelectedValue);
                    cmd.Parameters.Add(":alamat", address.Text);
                    cmd.Parameters.Add(":tel", phoneNumber.Text);
                    cmd.Parameters.Add(":kode", kodeHTrans);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    cmd = new OracleCommand("UPDATE mh_user set saldo = :uang WHERE kode_user = :code", conn);
                    cmd.Parameters.Add(":uang",saldo);
                    cmd.Parameters.Add(":code", kode);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    cmd = new OracleCommand("insert into history_emoney values ('', :fk, :emoney, :stat, '',:ket)", conn);
                    cmd.Parameters.Add(":fk", kode);
                    cmd.Parameters.Add(":emoney", Convert.ToInt32(grandtotal.Text));
                    cmd.Parameters.Add(":stat", 3);
                    cmd.Parameters.Add(":ket", "Payment " + kodeHTrans );
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Transaction Successful");

                    Invoice b = new Invoice(username, kode, kodeHTrans);
                    b.Show();
                    this.Close();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want to Cancel ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                OracleCommand cmd = new OracleCommand("delete from dtrans where fk_htrans = :kode", conn);
                cmd.Parameters.Add(":kode", kodeHTrans);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                 cmd = new OracleCommand("delete from htrans where kode_htrans = :kode", conn);
                cmd.Parameters.Add(":kode", kodeHTrans);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Buyer b = new Buyer(username,kode);
                b.Show();
                this.Close();
            }
        }

        private void Shipping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            promoDelivery.Text = d[shipping.SelectedIndex].diskon + "";
            grandtotal.Text = (tot + d[shipping.SelectedIndex].total + - d[shipping.SelectedIndex].diskon) + "";
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
