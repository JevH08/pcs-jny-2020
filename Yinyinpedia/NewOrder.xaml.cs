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
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        string user,kode;
        OracleConnection conn;
        OracleCommand cmd;
        DataSet dbp;
        DataSet kode_ht;
        string dkode,pkode;
        public NewOrder(string username,string kod)
        {
            InitializeComponent();
            user = username;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            loadData();
        }

        private class Kategori
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }
        public void loadData()
        {
            conn.Open();
            OracleDataAdapter da = new OracleDataAdapter("select p.nama_produk as nama_produk, k.nama_kategori as kategori, dt.jumlah as jumlah, dt.harga as harga_barang, dt.subtotal as subtotal from mh_produk p, dtrans dt, htrans ht, mh_kategori k where dt.fk_htrans = ht.kode_htrans and p.kode_produk = dt.fk_produk and p.fk_kategori = k.kode_kategori  and dt.status = 0 and p.fk_penjual like '%US20200510_004%' order by ht.kode_htrans", conn);
            dbp = new DataSet();
            da.Fill(dbp);
            dgNew.ItemsSource = null;
            dgNew.ItemsSource = dbp.Tables[0].DefaultView;
            da = new OracleDataAdapter("select ht.kode_htrans as kode, dt.kode_dtrans as dkode , p.stok as stok, p.kode_produk as kodepro FROM htrans ht,dtrans dt, mh_produk p where dt.fk_htrans = ht.kode_htrans and p.kode_produk = dt.fk_produk and dt.status = 0 and p.fk_penjual like '%"+kode+"%' order by ht.kode_htrans", conn);
            kode_ht = new DataSet();
            da.Fill(kode_ht);
            
            category.ItemsSource = null;
            string query = "select * from mh_kategori where status = 0 order by Nama_kategori";
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
            conn.Close();
        }

        private void reset()
        {
            notrans.Text = ""; nmbrg.Text = "";category.SelectedIndex = -1;jumorder.Text = "";stok.Text = "";subtotal.Text = "";harga.Text = "";dgNew.SelectedIndex = -1; dkode = ""; pkode = "";
            accept.IsEnabled = false;
            decline.IsEnabled = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Stok_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DgNew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DgNew_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgNew.SelectedIndex != -1)
            {
                DataRow dr = dbp.Tables[0].Rows[dgNew.SelectedIndex];
                notrans.Text = kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["kode"].ToString();
                nmbrg.Text = dr["nama_produk"].ToString();
                category.Text = dr["kategori"].ToString();
                jumorder.Text = dr["jumlah"].ToString();
                stok.Text = kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["stok"].ToString();
                harga.Text = dr["harga_barang"].ToString();
                subtotal.Text = dr["subtotal"].ToString();
                dkode = kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["dkode"].ToString();
                pkode = kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["kodepro"].ToString();
                accept.IsEnabled = true;
                decline.IsEnabled = true;
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            string query = "update dtrans set status = 2 where kode_dtrans = " + dkode;
            cmd = new OracleCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            loadData();
            reset();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(stok.Text) >= Convert.ToInt32(jumorder.Text))
            {
                string query = "update dtrans set status = 1 where kode_dtrans = " + dkode;
                cmd = new OracleCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                int stokbaru = Convert.ToInt32(stok.Text) - Convert.ToInt32(jumorder.Text);
                query = "update mh_produk set stok = " + stokbaru + " where kode_produk = " + pkode;
                cmd = new OracleCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                loadData();
                reset();
            }
            else
            {
                MessageBox.Show("stok tidak mencukupi");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Seller s = new Seller(user,kode);
            s.Show();
            this.Close();
        }
    }
}
