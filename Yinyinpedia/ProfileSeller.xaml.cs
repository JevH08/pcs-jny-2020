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
    /// Interaction logic for ProfileSeller.xaml
    /// </summary>
    public partial class ProfileSeller : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, kode;
        string[] kotas = {"Surabaya", "Aceh", "Mojokerto", "Bali", "Jakarta", "Semarang", "Yogyakarta", "Solo", "Bandung", "Kediri" };

        public ProfileSeller(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            loadData();
        }

        private class City
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
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
                query = "select jenis_kelamin from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                string temp = cmd.ExecuteScalar().ToString();
                if (temp == "P")
                {
                    female.IsChecked = true;
                }
                else
                {
                    male.IsChecked = true;
                }
                query = "select to_char(tgl_lahir, 'DD-MM-YYYY') from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                birthDate.Text = cmd.ExecuteScalar().ToString();
                query = "select username_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                tusername.Text = cmd.ExecuteScalar().ToString();
                query = "select email_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                email.Text = cmd.ExecuteScalar().ToString();
                query = "select telepon_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                phoneNumber.Text = cmd.ExecuteScalar().ToString();
                query = "select norek from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                norek.Text = cmd.ExecuteScalar().ToString();
                query = "select alamat_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                address.Text = cmd.ExecuteScalar().ToString();
                query = "select kota_user from mh_user where kode_user = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                temp = cmd.ExecuteScalar().ToString();
                city.SelectedValue = temp;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword cp = new ChangePassword(username, kode, 1);
            cp.Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Seller s = new Seller(username, kode);
            s.Show();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                string query = "";
                if ((bool) male.IsChecked)
                {
                    query = $"update mh_user set nama_user = '{name.Text.ToUpper()}', jenis_kelamin = 'L', tgl_lahir = to_date('{birthDate.Text}', 'DD-MM-YYYY'), email_user = '{email.Text}', telepon_user = '{phoneNumber.Text}', norek = '{norek.Text}', alamat_user = '{address.Text}', kota_user = '{city.SelectedValue.ToString()}' where kode_user = '" + kode + "'";
                }
                else if ((bool)female.IsChecked)
                {
                    query = $"update mh_user set nama_user = '{name.Text.ToUpper()}', jenis_kelamin = 'P', tgl_lahir = to_date('{birthDate.Text}', 'DD-MM-YYYY'), email_user = '{email.Text}', telepon_user = '{phoneNumber.Text}', norek = '{norek.Text}', alamat_user = '{address.Text}', kota_user = '{city.SelectedValue.ToString()}' where kode_user = '" + kode + "'";
                }
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Profile Update Succeeded");
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
