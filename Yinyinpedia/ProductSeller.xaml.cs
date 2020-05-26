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
    /// Interaction logic for ProductSeller.xaml
    /// </summary>
    public partial class ProductSeller : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        string username, kode;
        int min, max, hal, mode;
        List<string> kodeProduk = new List<string>();

        private class Kategori
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }

        public ProductSeller(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            min = 0;
            max = 10;
            hal = 1;
            mode = 0;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select * from ( " + "select a.*, rownum rnum from ( " +
                    "select initcap(p.nama_produk) as NAME, p.desc_barang as DESCRIPTION, k.nama_kategori as CATEGORY, p.harga as PRICE, p.stok as STOCK, p.berat as WEIGHT, (case when p.kondisi = 0 then 'New' else 'Used' end) as CONDITION, p.tag as TAG, p.totalrating as RATING, (case when p.status = 0 then 'Verified' when p.status = 1 then 'Rejected' else 'Process' end) as STATUS " +
                    "from mh_produk p, mh_kategori k " +
                    "where p.fk_kategori = k.kode_kategori and p.status < 3 and fk_penjual = '" + kode + "' " +
                    "order by 1 ) a " +
                    "where rownum <= " + max.ToString() + ") " +
                    "where rnum > " + min.ToString();
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                db.Tables[0].Columns.Remove("RNUM");
                dgvProduct.ItemsSource = null;
                dgvProduct.ItemsSource = db.Tables[0].DefaultView;

                category.ItemsSource = null;
                query = "select * from mh_kategori where status = 0 order by Nama_kategori";
                cmd = new OracleCommand(query, conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    List<Kategori> kategori = new List<Kategori>();
                    while (reader.Read())
                    {
                        Kategori temp = new Kategori();
                        temp.Kode = reader.GetString(0);
                        temp.Nama = reader.GetString(1);
                        kategori.Add(temp);
                    }
                    category.SelectedValuePath = "Kode";
                    category.DisplayMemberPath = "Nama";
                    category.ItemsSource = kategori;
                }

                kodeProduk.Clear();
                query = "select * from mh_produk order by nama_produk";
                cmd = new OracleCommand(query, conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kodeProduk.Add(reader.GetString(0));
                    }
                }

                query = "select count(kode_produk) from mh_produk where fk_penjual = '" + kode + "' and status < 3";
                cmd = new OracleCommand(query, conn);
                produk.Text = cmd.ExecuteScalar().ToString();

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            processData();
        }

        public void reset()
        {
            name.Text = "";
            description.Text = "";
            category.SelectedIndex = -1;
            stock.Text = "";
            price.Text = "";
            weight.Text = "";
            rnew.IsChecked = true;
            tag.Text = "";
            mode = 0;
            submit.Content = "Create";
        }

        private void processData()
        {
            try
            {
                conn.Open();
                string query = "select kode_produk from mh_produk where status = 2 and fk_penjual = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                string produkKode = cmd.ExecuteScalar().ToString();
                query = "select nama_produk from mh_produk where kode_produk = '" + produkKode + "'";
                cmd = new OracleCommand(query, conn);
                string namaProduk = cmd.ExecuteScalar().ToString();
                string[] temp = namaProduk.Split(' ');
                if (temp.Count() == 0)
                {
                    cmd = new OracleCommand()
                    {
                        Connection = conn,
                        CommandText = "larangan",
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        Direction = ParameterDirection.Input,
                        ParameterName = "kode",
                        OracleDbType = OracleDbType.Varchar2,
                        Size = 20,
                        Value = produkKode
                    });
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        Direction = ParameterDirection.Input,
                        ParameterName = "nama",
                        OracleDbType = OracleDbType.Varchar2,
                        Size = 200,
                        Value = namaProduk
                    });
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    foreach (string word in temp)
                    {
                        cmd = new OracleCommand()
                        {
                            Connection = conn,
                            CommandText = "larangan",
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add(new OracleParameter()
                        {
                            Direction = ParameterDirection.Input,
                            ParameterName = "kode",
                            OracleDbType = OracleDbType.Varchar2,
                            Size = 20,
                            Value = produkKode
                        });
                        cmd.Parameters.Add(new OracleParameter()
                        {
                            Direction = ParameterDirection.Input,
                            ParameterName = "nama",
                            OracleDbType = OracleDbType.Varchar2,
                            Size = 200,
                            Value = word
                        });
                        cmd.ExecuteNonQuery();
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
            Seller s = new Seller(username, kode);
            s.Show();
            this.Close();
        }

        private void DgvProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvProduct.SelectedIndex != -1)
            {
                DataRow dr = db.Tables[0].Rows[dgvProduct.SelectedIndex];
                if (dr["status"].ToString() == "Verified")
                {
                    name.Text = dr["name"].ToString();
                    description.Text = dr["description"].ToString();
                    stock.Text = dr["stock"].ToString();
                    price.Text = dr["price"].ToString();
                    weight.Text = dr["weight"].ToString();
                    tag.Text = dr["tag"].ToString();
                    if (dr["condition"].ToString() == "New")
                    {
                        rnew.IsChecked = true;
                    }
                    else if (dr["condition"].ToString() == "Used")
                    {
                        rused.IsChecked = true;
                    }
                    try
                    {
                        conn.Open();
                        string query = "select Kode_kategori from mh_kategori where Nama_kategori = '" + dr["category"].ToString() + "'";
                        cmd = new OracleCommand(query, conn);
                        category.SelectedValue = cmd.ExecuteScalar().ToString();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        Console.WriteLine(ex.StackTrace);
                    }
                    submit.Content = "Update";
                    mode = 1;
                    name.IsEnabled = false;
                    weight.IsEnabled = false;
                    category.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Status Product Must Verified");
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            loadData();
            reset();
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (hal > 1)
            {
                min -= 10;
                max -= 10;
                hal--;
                page.Text = hal.ToString();
                loadData();
            }
            else
            {
                MessageBox.Show("Already in Front Page");
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            min = max;
            max += 10;
            hal++;
            page.Text = hal.ToString();
            loadData();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && description.Text != "" && category.SelectedIndex != -1 && stock.Text != "" && price.Text != "" && weight.Text != "" && tag.Text != "")
            {
                try
                {
                    int a = Convert.ToInt32(stock.Text);
                    int b = Convert.ToInt32(price.Text);
                    int c = Convert.ToInt32(weight.Text);
                    if (a > 0)
                    {
                        try
                        {
                            conn.Open();
                            string query = "";
                            if (mode == 0)
                            {
                                if ((bool)rnew.IsChecked)
                                {
                                    query = $"insert into mh_produk(nama_produk, desc_barang, fk_kategori, fk_penjual, stok, harga, berat, kondisi, tag, status) values('{name.Text}', '{description.Text}', '{category.SelectedValue}', '{kode}', '{stock.Text}', '{price.Text}', '{weight.Text}', 0, '{tag.Text}', 2)";
                                }
                                else if ((bool)rused.IsChecked)
                                {
                                    query = $"insert into mh_produk(nama_produk, desc_barang, fk_kategori, fk_penjual, stok, harga, berat, kondisi, tag, status) values('{name.Text}', '{description.Text}', '{category.SelectedValue}', '{kode}', '{stock.Text}', '{price.Text}', '{weight.Text}', 1, '{tag.Text}', 2)";
                                }
                            }
                            else if (mode == 1)
                            {
                                if ((bool)rnew.IsChecked)
                                {
                                    query = $"update mh_produk set nama_produk = '{name.Text}', desc_barang = '{description.Text}', fk_kategori = '{category.SelectedValue}', stok = '{stock.Text}', harga = '{price.Text}', berat = '{weight.Text}', kondisi = 0, tag = '{tag.Text}', status = 2 where kode_produk = '" + kodeProduk[dgvProduct.SelectedIndex] + "'";
                                }
                                else if ((bool)rused.IsChecked)
                                {
                                    query = $"update mh_produk set nama_produk = '{name.Text}', desc_barang = '{description.Text}', fk_kategori = '{category.SelectedValue}', stok = '{stock.Text}', harga = '{price.Text}', berat = '{weight.Text}', kondisi = 1, tag = '{tag.Text}', status = 2 where kode_produk = '" + kodeProduk[dgvProduct.SelectedIndex] + "'";
                                }
                            }
                            cmd = new OracleCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            Console.WriteLine(ex.StackTrace);
                        }
                        loadData();
                        reset();
                    }
                    else
                    {
                        MessageBox.Show("Input Stock Must More Than 0");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Input Stock / Price / Weight Must Numeric");
                }
            }
            else
            {
                MessageBox.Show("Input Data Product Is Incomplete");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                string query = "update mh_produk set status = 3 where kode_produk = '" + kodeProduk[dgvProduct.SelectedIndex] + "'";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            loadData();
            reset();
        }
    }
}
