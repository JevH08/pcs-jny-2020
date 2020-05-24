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
        string username, kode;

        public shippingBuyer(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select ht.kode_htrans as INVOICE, to_char(ht.tgl_transaksi, 'DD-MM-YYYY') as DATE_TRANSACTION, u.nama_user as SELLER, p.nama_produk as PRODUCT, (case when dt.status = 0 then 'Process' when dt.status = 1 then 'Accepted' else 'Declined' end) as STATUS " +
                    "from htrans ht, dtrans dt, mh_user u, mh_produk p " +
                    "where ht.fk_pelanggan = '" + kode + "' and ht.status = 0 and ht.kode_htrans = dt.fk_htrans and dt.fk_produk = p.kode_produk and p.fk_penjual = u.kode_user";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                //dgvShipping.ItemsSource = null;
                //dgvShipping.ItemsSource = db.Tables[0].DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void DgvShipping_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvShipping.SelectedIndex != -1)
            {
                shippingConfirmation sc = new shippingConfirmation(username, kode, "");
                sc.Show();
                this.Close();
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Buyer b = new Buyer(username, kode);
            b.Show();
            this.Close();
        }
    }
}
