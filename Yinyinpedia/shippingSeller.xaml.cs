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
    /// Interaction logic for shippingSeller.xaml
    /// </summary>
    public partial class shippingSeller : Window
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

        public shippingSeller(string user, string kod)
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
            listDTrans.Items.Clear();
            submit.IsEnabled = false;
            try
            {
                conn.Open();
                string query = "select distinct h.kode_htrans " +
                    "from htrans h, dtrans d, mh_user u, mh_produk p " +
                    "where h.kode_htrans = d.fk_htrans and d.status < 2 and d.reportB = 0 and d.fk_produk = p.kode_produk and p.fk_penjual = u.kode_user and u.kode_user = '" + kode + "' order by 1 desc";
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
            Seller s = new Seller(username, kode);
            s.Show();
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
                        "where h.kode_htrans = '" + htrans.SelectedValue + "' and h.fk_pelanggan = u.kode_user and h.kode_htrans = d.fk_htrans and d.fk_produk = p.kode_produk and p.fk_penjual = '" + kode + "'";
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
                        if (statusH[i] == "0")
                        {
                            l.Content = namaPenjual[i] + " - Status : Still Processing";
                        }
                        else if (statusH[i] == "1")
                        {
                            l.Content = namaPenjual[i] + " - Status : Shipping";
                        }
                        else if (statusH[i] == "2")
                        {
                            l.Content = namaPenjual[i] + " - Status : Arrived";
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
                    l.HorizontalAlignment = HorizontalAlignment.Center;
                    listDTrans.Items.Add(l);
                }
                submit.IsEnabled = true;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int cek = 0;
            if (statusH[0] == "2")
            {
                cek = 1;
            }
            if (cek == 1)
            {
                if (MessageBox.Show("Want to Report The Buyer ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    repotingBuyer rb = new repotingBuyer(username, kode, kodePenjual[htrans.SelectedIndex], htrans.SelectedValue.ToString());
                    rb.Show();
                    this.Close();
                }
                else
                {
                    try
                    {
                        conn.Open();
                        string query = $"update dtrans set reportB = 1 where kode_dtrans in (" +
                       $"select d.kode_dtrans from htrans h, dtrans d, mh_produk p, mh_user u " +
                       $"where  h.kode_htrans = '{htrans.SelectedValue}' and h.kode_htrans = d.fk_htrans and d.fk_produk = p.kode_produk and p.fk_penjual = u.kode_user and u.kode_user = '{kode}')";
                        cmd = new OracleCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        conn.Close();
                    }
                    loadData();
                }
            }
            else
            {
                MessageBox.Show("Please Kindly Wait, Products Still Shipping");
            }
        }
    }
}
