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
    /// Interaction logic for productBuyer.xaml
    /// </summary>
    public partial class productBuyer : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        DataTable cart;
        string username, kode;

        public productBuyer(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            amount.IsEnabled = false;
            tanya.IsEnabled = false;
            add.IsEnabled = false;
            chat.IsEnabled = false;
            cart = new DataTable();
            cart.Columns.Add("PRODUCT NAME");
            cart.Columns.Add("SELLER");
            cart.Columns.Add("PRICE");
            cart.Columns.Add("AMMOUNT");
            cart.Columns.Add("SUBTOTAL");
            //DataRow dra = cart.NewRow();
            //dra[0] = "aa";
            //dra[1] = "aa";
            //dra[2] = "aa";
            //dra[3] = "aa";
            //dra[4] = "aa";
            //cart.Rows.Add(dra);
            dgvCart.ItemsSource = cart.DefaultView;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                
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
            if (cart.Rows.Count == 0)
            {
                Buyer b = new Buyer(username, kode);
                b.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Transaction Not Yet Complete");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DgvProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            amount.IsEnabled = true;
            add.IsEnabled = true;
            tanya.IsEnabled = true;
            chat.IsEnabled = true;
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            amount.IsEnabled = false;
            add.IsEnabled = false;
            tanya.IsEnabled = false;
            chat.IsEnabled = false;
            amount.Text = "";
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DgvCart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Remove Cart Succeeded");
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string kodeH = "";
            try
            {
                conn.Open();
                string query = "select kode_htrans from htrans where status = 0";
                cmd = new OracleCommand(query, conn);
                kodeH = cmd.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            cartBuyer cb = new cartBuyer(username, kode, kodeH);
            cb.Show();
            this.Close();
        }
    }
}
