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
    /// Interaction logic for chatSeller.xaml
    /// </summary>
    public partial class chatSeller : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, kode;

        public chatSeller(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            reply.IsEnabled = false;
            submit.IsEnabled = false;
            loadData();
        }

        public void loadData()
        {
            try
            {

            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Seller s = new Seller(username, kode);
            s.Show();
            this.Close();
        }

        private void DgvChat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvChat.SelectedIndex != -1)
            {
                reply.IsEnabled = true;
                submit.IsEnabled = true;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (reply.Text != "")
            {
                reply.IsEnabled = false;
                submit.IsEnabled = false;
            }
        }
    }
}
