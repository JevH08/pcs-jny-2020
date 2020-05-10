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
    /// Interaction logic for ProductVerification.xaml
    /// </summary>
    public partial class ProductVerification : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        int min, max, hal;
        string username;

        private class Kategori
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }

        public ProductVerification(string user)
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            username = user;
            min = 0;
            max = 10;
            hal = 1;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select * from ( " + "select a.*, rownum rnum from ( " +
                    "select u.kode_user as SELLER, p.kode_produk as CODE, initcap(p.nama_produk) as NAME, p.desc_barang as DESCRIPTION, k.nama_kategori as CATEGORY, p.harga as PRICE, p.stok as STOCK, p.berat as WEIGHT, (case when p.kondisi = 0 then 'New' else 'Used' end) as CONDITION, p.tag as TAG " +
                    "from mh_produk p, mh_kategori k, mh_user u " +
                    "where p.fk_kategori = k.kode_kategori and p.status = 2 and p.fk_penjual = u.kode_user " +
                    "order by 1 ) a " +
                    "where rownum <= " + max.ToString() + ") " +
                    "where rnum > " + min.ToString();
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                db.Tables[0].Columns.Remove("RNUM");
                dgvProduct.ItemsSource = null;
                dgvProduct.ItemsSource = db.Tables[0].DefaultView;

                category.ItemsSource = null;
                query = "select * from mh_kategori where status = 0 order by Nama_kategori";
                cmd = new OracleCommand(query, conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    List<Kategori> kategori = new List<Kategori>();
                    while (reader.Read())
                    {
                        Kategori temp = new Kategori();
                        temp.Kode = reader.GetString(0);
                        temp.Nama = reader.GetString(1);
                        kategori.Add(temp);
                    }
                    category.SelectedValuePath = "Kode";
                    category.DisplayMemberPath = "Nama";
                    category.ItemsSource = kategori;
                }

                query = "select count(kode_produk) from mh_produk where status = 2";
                cmd = new OracleCommand(query, conn);
                produk.Text = cmd.ExecuteScalar().ToString();

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void reset()
        {
            name.Text = "";
            description.Text = "";
            category.SelectedIndex = -1;
            stock.Text = "";
            price.Text = "";
            weight.Text = "";
            rnew.IsChecked = true;
            tag.Text = "";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Admin a = new Admin(username);
            a.ShowDialog();
            this.Close();
        }

        private void DgvProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvProduct.SelectedIndex != -1)
            {
                DataRow dr = db.Tables[0].Rows[dgvProduct.SelectedIndex];
                name.Text = dr["name"].ToString();
                description.Text = dr["description"].ToString();
                stock.Text = dr["stock"].ToString();
                price.Text = dr["price"].ToString();
                weight.Text = dr["weight"].ToString();
                tag.Text = dr["tag"].ToString();
                if (dr["condition"].ToString() == "New")
                {
                    rnew.IsChecked = true;
                }
                else if (dr["condition"].ToString() == "Used")
                {
                    rused.IsChecked = true;
                }
                try
                {
                    conn.Open();
                    string query = "select Kode_kategori from mh_kategori where Nama_kategori = '" + dr["category"].ToString() + "'";
                    cmd = new OracleCommand(query, conn);
                    category.SelectedValue = cmd.ExecuteScalar().ToString();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (hal > 1)
            {
                min -= 10;
                max -= 10;
                hal--;
                page.Text = hal.ToString();
                loadData();
            }
            else
            {
                MessageBox.Show("Already in Front Page");
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            min = max;
            max += 10;
            hal++;
            page.Text = hal.ToString();
            loadData();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                DataRow dr = db.Tables[0].Rows[dgvProduct.SelectedIndex];
                string kode = dr["code"].ToString();
                string query = "update mh_produk set status = 0 where kode_produk = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            loadData();
            reset();
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                DataRow dr = db.Tables[0].Rows[dgvProduct.SelectedIndex];
                string kode = dr["code"].ToString();
                string query = "update mh_produk set status = 1 where kode_produk = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            loadData();
            reset();
        }
    }
}
