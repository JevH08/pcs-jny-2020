﻿using System;
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
        DataSet kode_ht,pengiriman,produk;
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
            OracleDataAdapter da = new OracleDataAdapter("select p.nama_produk as nama_produk, k.nama_kategori as kategori, dt.jumlah as jumlah, dt.harga as harga_barang, dt.subtotal as subtotal from mh_produk p, dtrans dt, htrans ht, mh_kategori k where dt.fk_htrans = ht.kode_htrans and p.kode_produk = dt.fk_produk and p.fk_kategori = k.kode_kategori  and dt.status = 0 and p.fk_penjual = '" + kode + "' order by ht.kode_htrans", conn);
            dbp = new DataSet();
            da.Fill(dbp);
            dgNew.ItemsSource = null;
            dgNew.ItemsSource = dbp.Tables[0].DefaultView;
            da = new OracleDataAdapter("select ht.kode_htrans as kode, dt.kode_dtrans as dkode , p.stok as stok, p.kode_produk as kodepro, ht.berat as berat, ht.subtotal as sub, ht.shipping as ship, ht.promo as promo, ht.fk_distributor as pengirim, ht.grandtotal as grand,ht.fk_pelanggan as pelanggan FROM htrans ht,dtrans dt, mh_produk p where dt.fk_htrans = ht.kode_htrans and p.kode_produk = dt.fk_produk and dt.status = 0 and p.fk_penjual = '" + kode + "' order by ht.kode_htrans", conn);
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
            query = "select count(dt.kode_dtrans) from mh_produk p, dtrans dt, htrans ht where dt.fk_htrans = ht.kode_htrans and p.kode_produk = dt.fk_produk and dt.status = 0 and p.fk_penjual = '" + kode + "'";
            cmd = new OracleCommand(query, conn);
            newOrder.Text = cmd.ExecuteScalar().ToString();

            conn.Close();
        }

        private void jalanpro()
        {
            OracleCommand cmd = new OracleCommand()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = conn,
                CommandText = "update_ship"
            };


            cmd.Parameters.Add(new OracleParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "tnamacd",
                OracleDbType = OracleDbType.Varchar2,
                Size = 20,
                Value = noTransc.Text
            });

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void reset()
        {
            noTransc.Text = ""; name.Text = "";category.SelectedIndex = -1;numberItem.Text = "";stock.Text = "";subtotal.Text = "";price.Text = "";dgNew.SelectedIndex = -1; dkode = ""; pkode = "";
            accpet.IsEnabled = false;
            decline.IsEnabled = false;
            dgNew.SelectedIndex = -1;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Seller s = new Seller(user, kode);
            s.Show();
            this.Close();
        }

        private void DgNew_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgNew.SelectedIndex != -1)
            {
                DataRow dr = dbp.Tables[0].Rows[dgNew.SelectedIndex];
                noTransc.Text = kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["kode"].ToString();
                name.Text = dr["nama_produk"].ToString();
                category.Text = dr["kategori"].ToString();
                numberItem.Text = dr["jumlah"].ToString();
                stock.Text = kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["stok"].ToString();
                conn.Open();
                OracleDataAdapter da = new OracleDataAdapter("select * from mh_distributor where kode_distributor = '" + kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["pengirim"].ToString() + "'", conn);
                pengiriman = new DataSet();
                da.Fill(pengiriman);
                conn.Close();
                price.Text = dr["harga_barang"].ToString();
                subtotal.Text = dr["subtotal"].ToString();
                dkode = kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["dkode"].ToString();
                pkode = kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["kodepro"].ToString();
                accpet.IsEnabled = true;
                decline.IsEnabled = true;
            }
        }

        private void Accpet_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(stock.Text) >= Convert.ToInt32(numberItem.Text))
            {
                string query = "update dtrans set status = 1 where kode_dtrans = '" + dkode + "'";
                cmd = new OracleCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                int stokbaru = Convert.ToInt32(stock.Text) - Convert.ToInt32(numberItem.Text);
                query = "update mh_produk set stok = " + stokbaru + " where kode_produk = '" + pkode + "'";
                cmd = new OracleCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                jalanpro();
                dkode = "";
                loadData();
                reset();
            }
            else
            {
                MessageBox.Show("stok tidak mencukupi");
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            string query = "update dtrans set status = 2 where kode_dtrans = '" + dkode + "'";
            cmd = new OracleCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int refund = Convert.ToInt32(dbp.Tables[0].Rows[dgNew.SelectedIndex]["subtotal"].ToString());
            int berat = Convert.ToInt32(kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["berat"].ToString());

            conn.Open();
            OracleDataAdapter da = new OracleDataAdapter("select * from mh_produk where kode_produk = '" + kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["kodepro"].ToString() + "'", conn);
            produk = new DataSet();
            da.Fill(produk);
            conn.Close();

            int beratpro = Convert.ToInt32(produk.Tables[0].Rows[0]["berat"]);
            int jum = Convert.ToInt32(dbp.Tables[0].Rows[dgNew.SelectedIndex]["jumlah"]);

            berat -= (beratpro * jum);
            int hargaship = berat * Convert.ToInt32(pengiriman.Tables[0].Rows[0]["harga_per_kilo"].ToString());
            int promoship = 0;
            if (berat >= Convert.ToInt32(pengiriman.Tables[0].Rows[0]["batas_harga"].ToString()))
            {
                promoship = hargaship * Convert.ToInt32(pengiriman.Tables[0].Rows[0]["batas_harga"].ToString()) / 100;
            }
            
            int grandtotal = Convert.ToInt32(kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["grand"].ToString());

            int sublama = Convert.ToInt32(kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["sub"].ToString());
            int shiplama = Convert.ToInt32(kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["ship"].ToString());
            int promlama = Convert.ToInt32(kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["promo"].ToString());

            sublama -= refund;
            int grandtotalbaru = sublama + hargaship - promoship;
            refund = grandtotal - grandtotalbaru;
            conn.Open();
            cmd = new OracleCommand("select saldo from mh_user where kode_user = '" + kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["pelanggan"].ToString() + "'", conn);
            int S = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            
            conn.Close();
            S += refund;

            conn.Open();
            query = "update mh_user set saldo = "+S+" where kode_user = '" + kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["pelanggan"].ToString() + "'";
            cmd = new OracleCommand(query, conn);
            
            cmd.ExecuteNonQuery();
            conn.Close();

            query = "update htrans set subtotal = " + sublama + ", shipping = "+ hargaship+ ", promo = "+promoship+ ",grandtotal = "+ grandtotalbaru+", berat = "+berat+" where kode_htrans = '" + kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["kode"].ToString() + "'";
            cmd = new OracleCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            cmd = new OracleCommand("insert into history_emoney values ('', :fk, :emoney, :stat, '',:ket)", conn);
            cmd.Parameters.Add(":fk", kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["pelanggan"].ToString());
            cmd.Parameters.Add(":emoney",refund);
            cmd.Parameters.Add(":stat", 4);
            cmd.Parameters.Add(":ket", "REFUND BARANG PADA NOTA :  " + kode_ht.Tables[0].Rows[dgNew.SelectedIndex]["kode"].ToString());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            jalanpro();
            dkode = "";
            loadData();
            reset();
        }
    }
}
