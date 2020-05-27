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
    /// Interaction logic for repotingBuyer.xaml
    /// </summary>
    public partial class repotingBuyer : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, kode, kodeBuyer, kodeH;

        public repotingBuyer(string user, string kod, string kodeU, string kodeHeader)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            kodeBuyer = kodeU;
            kodeH = kodeHeader;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (lapor.Text != "")
            {
                try
                {
                    conn.Open();
                    string query = $"insert into mh_report (fk_pelapor, fk_dilapor, alasan) values('{kode}', '{kodeBuyer}', '{lapor.Text}')";
                    cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    cmd = new OracleCommand()
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = conn,
                        CommandText = "cek_report"
                    };
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        Direction = ParameterDirection.Input,
                        ParameterName = "temp",
                        OracleDbType = OracleDbType.Varchar2,
                        Size = 70,
                        Value = "a"
                    });
                    cmd.ExecuteNonQuery();

                    query = $"update dtrans set reportB = 1 where kode_dtrans in (" +
                        $"select d.kode_dtrans from htrans h, dtrans d, mh_produk p, mh_user u " +
                        $"where  h.kode_htrans = '{kodeH}' and h.kode_htrans = d.fk_htrans and d.fk_produk = p.kode_produk and p.fk_penjual = u.kode_user and u.kode_user = '{kode}')";
                    cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    conn.Close();
                }
                MessageBox.Show("Already Done, Thank You For Your Cooperation");
                shippingSeller ss = new shippingSeller(username, kode);
                ss.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Reason is Required");
            }
        }
    }
}
