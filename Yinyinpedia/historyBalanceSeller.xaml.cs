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
    /// Interaction logic for historyBalanceSeller.xaml
    /// </summary>
    public partial class historyBalanceSeller : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        string username, kode;
        List<History> h;

        public historyBalanceSeller(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            loadData();
        }

        private class History
        {
            public string emoney { get; set; }
            public string tgl { get; set; }
            public string status { get; set; }
            public string keterangan { get; set; }
        }

        public void loadData()
        {
            try
            {
                conn.Open();

                string query = "select emoney , decode (status,0,'WITHDRAW','INCOME') as status, to_char(to_date(tgl_emoney),'DD-MM-YYYY') as tgl,ket from history_emoney where fk_user = '" + kode + "' order by kode_history desc";
                cmd = new OracleCommand(query, conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    h = new List<History>();
                    while (reader.Read())
                    {
                        History temp = new History();
                        temp.emoney = reader["emoney"].ToString();
                        temp.tgl = reader["tgl"].ToString();
                        temp.status = reader["status"].ToString();
                        temp.keterangan = reader["ket"].ToString();
                        h.Add(temp);
                    }
                }
                conn.Close();
                for (int i = 0; i < h.Count; i++)
                {
                    TextBlock t = new TextBlock();
                    t.Width = 395;
                    if (h[i].status == "WITHDRAW")
                    {
                        t.Text = h[i].status + "\n" + h[i].tgl + "\n" + "\n" + "                                                                                          - Rp " + h[i].emoney;
                        t.Background = Brushes.MistyRose;
                        t.Foreground = Brushes.Red;
                    }
                    else
                    {
                        t.Text = h[i].keterangan + "\n" + h[i].tgl + "\n" + "\n" +  "                                                                                          + Rp " + h[i].emoney;
                        t.Background = Brushes.LightCyan;
                        t.Foreground = Brushes.Green;
                    }
                    dgvBalance.Items.Add(t);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
