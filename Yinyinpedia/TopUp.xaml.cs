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
    /// Interaction logic for TopUp.xaml
    /// </summary>
    public partial class TopUp : Window
    {
        string user, kod;
        OracleConnection conn;
        int from;
        string kodeHTRANS;
        public TopUp(string username, string kode,int asal)
        {
            InitializeComponent();
            user = username;
            kod = kode;
            conn = new OracleConnection() { ConnectionString = "Data Source = ORCL; User id = proyekpcs; password = proyekpcs" };
            from = asal;
        }
        public TopUp(string username, string kode, int asal, string kodeH)
        {
            InitializeComponent();
            user = username;
            kod = kode;
            conn = new OracleConnection() { ConnectionString = "Data Source = ORCL; User id = proyekpcs; password = proyekpcs" };
            from = asal;
            kodeHTRANS = kodeH;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (from == 1)
            {
                Buyer b = new Buyer(user, kod);
                b.Show();
                this.Close();
            }
            else if(from == 2)
            {
                cartBuyer cb = new cartBuyer(user, kod, kodeHTRANS);
                cb.Show();
                this.Close();
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (nominal.Text == "")
            {
                MessageBox.Show("Please Fill in Nominal");
            }
            else
            {
                if (Convert.ToInt32(nominal.Text) < 10000)
                {
                    MessageBox.Show("Minimum Top Up is Rp10000");
                }
                else
                {
                    conn.Open();
                    string query = "select saldo from mh_user where kode_user ='" + kod + "'";
                    OracleCommand cmd = new OracleCommand(query, conn);

                    int db = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    conn.Close();
                    int t = db + Convert.ToInt32(nominal.Text);
                    query = "update mh_user set saldo = " + t + " where kode_user = '" + kod + "'";
                    cmd = new OracleCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd = new OracleCommand("insert into history_emoney values ('', :fk, :emoney, :stat, '','TOP UP')", conn);
                    cmd.Parameters.Add(":fk", kod);
                    cmd.Parameters.Add(":emoney", Convert.ToInt32(nominal.Text));
                    cmd.Parameters.Add(":stat", 2);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Top Up Success! +Rp" + nominal.Text);

                    if (from == 1)
                    {
                        Buyer b = new Buyer(user, kod);
                        b.Show();
                        this.Close();
                    }
                    else if (from == 2)
                    {
                        cartBuyer cb = new cartBuyer(user, kod, kodeHTRANS);
                        cb.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}
