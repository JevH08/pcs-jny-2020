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
    /// Interaction logic for chatBuyer.xaml
    /// </summary>
    public partial class chatBuyer : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        List<Chat> chatting = new List<Chat>();
        string username, kode;

        private class Chat
        {
            public string KodeH { get; set; }
            public string kodeS { get; set; }
        }

        public chatBuyer(string user, string kod)
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
                conn.Open();
                string query = "select u.nama_user as SELLER from th_chat hc, mh_user u where hc.fk_penjual = u.kode_user and hc.fk_pembeli = '" + kode + "' and hc.statusB = 0 order by hc.kode_hchat";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                dgvChat.ItemsSource = null;
                dgvChat.ItemsSource = db.Tables[0].DefaultView;

                query = "select kode_hchat ,fk_penjual from th_chat where fk_pembeli = '" + kode + "' and statusB = 0 order by 1";
                cmd = new OracleCommand(query, conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    chatting.Clear();
                    while (reader.Read())
                    {
                        Chat temp = new Chat();
                        temp.KodeH = reader.GetString(0);
                        temp.kodeS = reader.GetString(1);
                        chatting.Add(temp);
                    }
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

        private void DgvChat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvChat.SelectedIndex != -1)
            {
                try
                {
                    conn.Open();
                    string query = "select dc.isi_chat as isi, dc.pengirim as sender from th_chat hc, td_chat dc where hc.kode_hchat = dc.fk_hchat and hc.kode_hchat = '" + chatting[dgvChat.SelectedIndex].KodeH + "'";
                    cmd = new OracleCommand(query, conn);
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string isi = reader.GetString(0);
                            int pengirim = Convert.ToInt32(reader.GetString(1));
                            Label l = new Label();
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.StackTrace);
                }
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
