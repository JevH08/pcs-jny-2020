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
    /// Interaction logic for shippingConfirmation.xaml
    /// </summary>
    public partial class shippingConfirmation : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        List<namaPenjual> kodeUserPenjual = new List<namaPenjual>();
        string username, kode, kodeHTrans;

        private class namaPenjual
        {
            public string KodeS { get; set; }
            public string NamaS { get; set; }
            public string KodeP { get; set; }
            public string NamaP { get; set; }
            public string KodeD { get; set; }
            public decimal Harga { get; set; }
            public string Tampil { get; set; }
        }

        public shippingConfirmation(string user, string kod, string kodeH)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            kodeHTrans = kodeH;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            rating.IsEnabled = false;
            lapor.IsEnabled = false;
            submit.IsEnabled = false;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select u.kode_user, u.nama_user, p.kode_produk, p.nama_produk, d.kode_dtrans, d.harga " +
                    "from mh_user u, htrans h, dtrans d, mh_produk p " +
                    "where h.kode_htrans = '" + kodeHTrans + "' and h.kode_htrans = d.fk_htrans and d.status = 1 and d.reportS = 0 and d.fk_produk = p.kode_produk and p.fk_penjual = u.kode_user " +
                    "order by 2";
                cmd = new OracleCommand(query, conn);
                namaSeller.ItemsSource = null;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    kodeUserPenjual.Clear();
                    while (reader.Read())
                    {
                        namaPenjual temp = new namaPenjual();
                        temp.KodeS = reader.GetString(0);
                        temp.NamaS = reader.GetString(1);
                        temp.KodeP = reader.GetString(2);
                        temp.NamaP = reader.GetString(3);
                        temp.KodeD = reader.GetString(4);
                        temp.Harga = reader.GetDecimal(5);
                        temp.Tampil = reader.GetString(1) + " - " + reader.GetString(3);
                        kodeUserPenjual.Add(temp);
                    }
                    namaSeller.SelectedValuePath = "KodeS";
                    namaSeller.DisplayMemberPath = "Tampil";
                    namaSeller.ItemsSource = kodeUserPenjual;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            if (kodeUserPenjual.Count == 0)
            {
                try
                {
                    conn.Open();
                    string query = $"update htrans set status = 2 where kode_htrans = '" + kodeHTrans + "'";
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
                shippingBuyer sb = new shippingBuyer(username, kode);
                sb.Show();
                this.Close();
            }
        }

        private void NamaSeller_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rating.IsEnabled = true;
            lapor.IsEnabled = true;
            submit.IsEnabled = true;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (rating.Text != "")
            {
                try
                {
                    int a = Convert.ToInt32(rating.Text);
                    if (a > 0 && a < 6)
                    {
                        string query = "";
                        try
                        {
                            conn.Open();
                            cmd = new OracleCommand()
                            {
                                CommandType = CommandType.StoredProcedure,
                                Connection = conn,
                                CommandText = "beriRating"
                            };
                            cmd.Parameters.Add(new OracleParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "kodeProduk",
                                OracleDbType = OracleDbType.Varchar2,
                                Size = 70,
                                Value = kodeUserPenjual[namaSeller.SelectedIndex].KodeP
                            });
                            cmd.Parameters.Add(new OracleParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "rating",
                                OracleDbType = OracleDbType.Varchar2,
                                Size = 70,
                                Value = rating.Text
                            });
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            conn.Close();
                        }
                        if (lapor.Text != "")
                        {
                            try
                            {
                                conn.Open();
                                query = $"insert into mh_report (fk_pelapor, fk_dilapor, alasan) values('{kode}', '{kodeUserPenjual[namaSeller.SelectedIndex].KodeS}', '{lapor.Text}')";
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
                                conn.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                conn.Close();
                            }
                        }
                        try
                        {
                            conn.Open();
                            query = $"update dtrans set reportS = 1, rating = '{rating.Text}' where kode_dtrans = '" + kodeUserPenjual[namaSeller.SelectedIndex].KodeD + "'";
                            cmd = new OracleCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            cmd = new OracleCommand()
                            {
                                CommandType = CommandType.StoredProcedure,
                                Connection = conn,
                                CommandText = "transfer"
                            };
                            cmd.Parameters.Add(new OracleParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "kodeUser",
                                OracleDbType = OracleDbType.Varchar2,
                                Size = 70,
                                Value = kodeUserPenjual[namaSeller.SelectedIndex].KodeS
                            });
                            cmd.Parameters.Add(new OracleParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "uang",
                                OracleDbType = OracleDbType.Varchar2,
                                Size = 70,
                                Value = kodeUserPenjual[namaSeller.SelectedIndex].Harga.ToString()
                            });
                            cmd.ExecuteNonQuery();

                            string keterangan = "Payment " + kodeUserPenjual[namaSeller.SelectedIndex].NamaP;
                            query = $"insert into history_emoney(fk_user, emoney, status, ket) values('{kodeUserPenjual[namaSeller.SelectedIndex].KodeS}', '{kodeUserPenjual[namaSeller.SelectedIndex].Harga}', 1, '{keterangan}')";
                            cmd = new OracleCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            conn.Close();
                        }
                        rating.Text = "";
                        lapor.Text = "";
                        rating.IsEnabled = false;
                        lapor.IsEnabled = false;
                        submit.IsEnabled = false;
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Input Rating Must 1 to 5");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Data Input Must Numeric");
                }
            }
            else
            {
                MessageBox.Show("Rating is Required");
            }
        }
    }
}
