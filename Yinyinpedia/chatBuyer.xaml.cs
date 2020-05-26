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
        OracleTransaction trans;
        DataSet db = new DataSet();
        List<string> chatting = new List<string>();
        string username, kode, kodeHC;

        public chatBuyer(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            reply.IsEnabled = false;
            submit.IsEnabled = false;
            delete.IsEnabled = false;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select u.nama_user as SELLER from th_chat hc, mh_user u where hc.fk_penjual = u.kode_user and hc.fk_pembeli = '" + kode + "' order by hc.kode_hchat";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                dgvChat.ItemsSource = null;
                dgvChat.ItemsSource = db.Tables[0].DefaultView;

                query = "select kode_hchat ,fk_penjual from th_chat where fk_pembeli = '" + kode + "' order by 1";
                cmd = new OracleCommand(query, conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    chatting.Clear();
                    while (reader.Read())
                    {
                        chatting.Add(reader.GetString(0));
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

        public void loadChat()
        {
            listChat.Items.Clear();
            List<string> isi = new List<string>();
            List<decimal> pengirim = new List<decimal>();
            try
            {
                conn.Open();
                string query = "select dc.isi_chat as isi, dc.pengirim as sender from th_chat hc, td_chat dc where dc.statusB = 0 and hc.kode_hchat = dc.fk_hchat and hc.kode_hchat = '" + kodeHC + "' order by dc.kode_dchat";
                cmd = new OracleCommand(query, conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        isi.Add(reader.GetString(0));
                        pengirim.Add(reader.GetDecimal(1));
                    }
                }

                for (int i = 0; i < isi.Count(); i++)
                {
                    Label t = new Label();
                    t.Content = isi[i];
                    if (pengirim[i] == 1)
                    {
                        t.Background = Brushes.White;
                        t.HorizontalAlignment = HorizontalAlignment.Left;
                    }
                    else if (pengirim[i] == 2)
                    {
                        t.Background = Brushes.LightCyan;
                        t.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    listChat.Items.Add(t);
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
                kodeHC = chatting[dgvChat.SelectedIndex];
                loadChat();
                reply.IsEnabled = true;
                submit.IsEnabled = true;
                delete.IsEnabled = true;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvChat.SelectedIndex != -1)
            {
                try
                {
                    conn.Open();
                    string query = $"update td_chat set statusB = 1 where fk_hchat = '" + kodeHC + "'";
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
                listChat.Items.Clear();
                reply.IsEnabled = false;
                submit.IsEnabled = false;
                delete.IsEnabled = false;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (reply.Text != "")
            {
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    cmd = new OracleCommand()
                    {
                        Connection = conn,
                        CommandText = "autogendt_c",
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        Direction = ParameterDirection.ReturnValue,
                        ParameterName = "kodedt",
                        OracleDbType = OracleDbType.Varchar2,
                        Size = 20
                    });
                    cmd.ExecuteNonQuery();
                    string kodeDC = cmd.Parameters["kodedt"].Value.ToString();
                    DateTime tanggal_penuh = DateTime.Now;
                    string tanggal = tanggal_penuh.Day.ToString().PadLeft(2, '0') + "-" + tanggal_penuh.Month.ToString().PadLeft(2, '0') + "-" + tanggal_penuh.Year.ToString();
                    string query = $"insert into td_chat values ('{kodeDC}', '{kodeHC}', '2', '{reply.Text}', to_date('{tanggal}','DD-MM-YYYY'), 0, 0)";
                    cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    trans.Rollback();
                    conn.Close();
                }
                loadChat();
                reply.Text = "";
            }
        }
    }
}
