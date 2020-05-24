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
    /// Interaction logic for shippingBuyer.xaml
    /// </summary>
    public partial class shippingBuyer : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        List<string> namaPenjual = new List<string>();
        List<string> kodePenjual = new List<string>();
        List<string> produk = new List<string>();
        List<decimal> status = new List<decimal>();
        List<string> statusH = new List<string>();
        string username, kode;

        private class headerTrans
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }

        public shippingBuyer(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            submit.IsEnabled = false;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select kode_htrans from htrans where fk_pelanggan = '" + kode + "' and status < 2 order by 1 desc";
                cmd = new OracleCommand(query, conn);
                htrans.ItemsSource = null;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    List<headerTrans> kodeHtrans = new List<headerTrans>();
                    while (reader.Read())
                    {
                        headerTrans temp = new headerTrans();
                        temp.Kode = reader.GetString(0);
                        temp.Nama = reader.GetString(0);
                        kodeHtrans.Add(temp);
                    }
                    htrans.SelectedValuePath = "Kode";
                    htrans.DisplayMemberPath = "Nama";
                    htrans.ItemsSource = kodeHtrans;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Buyer b = new Buyer(username, kode);
            b.Show();
            this.Close();
        }

        private void Htrans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (htrans.SelectedIndex != -1)
            {
                namaPenjual.Clear();
                kodePenjual.Clear();
                produk.Clear();
                status.Clear();
                statusH.Clear();
                string temp = "";
                try
                {
                    conn.Open();
                    string query = "select u.nama_user, u.kode_user, p.nama_produk, d.status, h.status " +
                        "from mh_user u, htrans h, dtrans d, mh_produk p " +
                        "where h.kode_htrans = '" + htrans.SelectedValue  + "' and h.kode_htrans = d.fk_htrans and d.fk_produk = p.kode_produk and p.fk_penjual = u.kode_user " +
                        "order by 2";
                    cmd = new OracleCommand(query, conn);
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            namaPenjual.Add(reader.GetString(0));
                            kodePenjual.Add(reader.GetString(1));
                            produk.Add(reader.GetString(2));
                            status.Add(reader.GetDecimal(3));
                            statusH.Add(reader.GetString(4));
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.StackTrace);
                }
                listDTrans.Items.Clear();
                for (int i = 0; i < namaPenjual.Count; i++)
                {
                    Label l = new Label();
                    if (temp != kodePenjual[i])
                    {
                        listDTrans.Items.Add("");
                        temp = kodePenjual[i];
                        l = new Label();
                        l = new Label();
                        if (statusH[i] == "0")
                        {
                            l.Content = namaPenjual[i] + " - Status : Still Processing";
                        }
                        else if (statusH[i] == "1")
                        {
                            l.Content = namaPenjual[i] + " - Status : Shipping";
                        }
                        l.HorizontalAlignment = HorizontalAlignment.Center;
                        listDTrans.Items.Add(l);
                    }
                    l = new Label();
                    if (status[i] == 0)
                    {
                        l.Content = produk[i] + " - Status : Still Processing";
                    }
                    else if (status[i] == 1)
                    {
                        l.Content = produk[i] + " - Status : Seller Accepted";
                    }
                    else if (status[i] == 2)
                    {
                        l.Content = produk[i] + " - Status : Seller Declined";
                    }
                    l.HorizontalAlignment = HorizontalAlignment.Center;
                    listDTrans.Items.Add(l);
                }
                submit.IsEnabled = true;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int cek = 0;
            for (int i = 0; i < status.Count; i++)
            {
                if (status[i] == 0)
                {
                    cek = 1;
                }
            }
            if (cek == 0)
            {
                shippingConfirmation sc = new shippingConfirmation(username, kode, htrans.SelectedValue.ToString());
                sc.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Kindly Wait, Products Still Have A Process Status");
            }
        }
    }
}
