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
    /// Interaction logic for userBanned.xaml
    /// </summary>
    public partial class userBanned : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username;

        private class userSB
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }

        public userBanned(string user)
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            username = user;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select kode_user, username_user, nama_user from mh_user where aktif = 1 order by 3";
                cmd = new OracleCommand(query, conn);
                listUser.ItemsSource = null;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    List<userSB> user2 = new List<userSB>();
                    while (reader.Read())
                    {
                        userSB temp = new userSB();
                        temp.Kode = reader.GetString(0);
                        temp.Nama = reader.GetString(1) + " - " + reader.GetString(2);
                        user2.Add(temp);
                    }
                    listUser.SelectedValuePath = "Kode";
                    listUser.DisplayMemberPath = "Nama";
                    listUser.ItemsSource = user2;
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
            SelectReport sr = new SelectReport(username);
            sr.Show();
            this.Close();
        }

        private void User_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listUser.SelectedIndex != -1)
            {
                List<string> reason = new List<string>();
                try
                {
                    conn.Open();
                    string query = "select alasan from mh_report where fk_dilapor = '" + listUser.SelectedValue + "'";
                    cmd = new OracleCommand(query, conn);
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reason.Add(reader.GetString(0));
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.StackTrace);
                }
                alasan.Items.Clear();
                if (reason.Count > 0)
                {
                    for (int i = 0; i < reason.Count; i++)
                    {
                        Label l = new Label();
                        l.Content = reason[i];
                        if (i % 2 == 0)
                        {
                            l.Background = Brushes.LightCyan;
                        }
                        else
                        {
                            l.Background = Brushes.White;
                        }
                        l.HorizontalAlignment = HorizontalAlignment.Center;
                        alasan.Items.Add(l);
                    }
                }
                else
                {
                    Label l = new Label();
                    l.Content = "Banned by Admin";
                    l.Background = Brushes.White;
                    l.HorizontalAlignment = HorizontalAlignment.Center;
                    alasan.Items.Add(l);
                }
                
            }
        }
    }
}
