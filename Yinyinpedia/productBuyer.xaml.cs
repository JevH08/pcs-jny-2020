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
        DataSet db = new DataSet();
        DataSet dbp;
        DataTable cart, submitbuy;
        OracleConnection conn;
        OracleCommand cmd;
        OracleCommandBuilder builder;
        OracleTransaction trans;
        OracleDataAdapter dabuilder;
        List<Barang> brg;
        List<string> listbeli;
        List<int> listberat;
        string username, kode;
        int totalberat = 0, totalbeli = 0, productbeli = 0;

        private class Barang
        {
            public string KodeB { get; set; }
            public string KodeP { get; set; }
            public int Berat { get; set; }
        }

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
            cart.Columns.Add("PRODUCT_NAME");
            cart.Columns.Add("SELLER");
            cart.Columns.Add("PRICE");
            cart.Columns.Add("AMMOUNT");
            cart.Columns.Add("SUBTOTAL");
            dgvCart.ItemsSource = cart.DefaultView;
            listbeli = new List<string>();
            listberat = new List<int>();
            loadData(1,0, "",0);
            city.SelectedIndex = 0;
            buildData();
        }

        public void buildData()
        {
            dabuilder = new OracleDataAdapter("select * from dtrans order by kode_dtrans", conn);
            // step 1 pakai builder, tancapkan builder ke DATA ADAPTER KITA
            builder = new OracleCommandBuilder(dabuilder);
            submitbuy = new DataTable();
            dabuilder.Fill(submitbuy);
        }

        public void loadData(int pages,int rate, string tag, int kota)
        {
            try
            {
                string[] search = tag.Split(' ');
                conn.Open();
                if (rate == 0)
                {
                    OracleDataAdapter da = new OracleDataAdapter("select p.nama_produk as nama, k.nama_kategori as kategori,  u.nama_user as seller, p.stok as stok, p.harga as harga,p.totalrating as rating, p.desc_barang as deskripsi, p.tag as tag from mh_produk p, mh_user u, mh_kategori k where p.status = 0 and p.fk_kategori = k.kode_kategori and p.fk_penjual = u.kode_user and p.kondisi = " + kota + " and u.aktif = 0 order by p.fk_kategori, to_number(p.totalrating),p.kode_produk", conn);
                    dbp = new DataSet();
                    da.Fill(dbp);
                    dgvProduct.ItemsSource = null;
                    dgvProduct.ItemsSource = dbp.Tables[0].DefaultView;
                    OracleCommand cmdCD = new OracleCommand("select p.kode_produk,p.fk_penjual,p. berat as berat from mh_produk p, mh_user u where p.status = 0 and p.fk_penjual = u.kode_user and p.kondisi = " + kota + " and u.aktif = 0 order by p.fk_kategori,to_number(p.totalrating),p.kode_produk", conn);
                    OracleDataReader reader = cmdCD.ExecuteReader();
                    brg = new List<Barang>();
                    while (reader.Read())
                    {
                        Barang temp = new Barang();
                        temp.KodeB = reader.GetString(0);
                        temp.KodeP = reader.GetString(1);
                        temp.Berat = Convert.ToInt32(reader["berat"].ToString());
                        brg.Add(temp);
                    }
                }
                else
                {
                    OracleDataAdapter da = new OracleDataAdapter("select p.nama_produk as nama, k.nama_kategori as kategori,  u.nama_user as seller, p.stok as stok, p.harga as harga,p.totalrating as rating, p.desc_barang as deskripsi, p.tag as tag from mh_produk p, mh_user u, mh_kategori k where p.status = 0 and p.fk_kategori = k.kode_kategori and p.fk_penjual = u.kode_user and p.kondisi = " + kota + " and u.aktif = 0 order by p.fk_kategori,to_number(p.totalrating) desc,p.kode_produk", conn);
                    dbp = new DataSet();
                    da.Fill(dbp);
                    dgvProduct.ItemsSource = null;
                    dgvProduct.ItemsSource = dbp.Tables[0].DefaultView;
                    OracleCommand cmdCD = new OracleCommand("select p.kode_produk,p.fk_penjual,p. berat as berat from mh_produk p, mh_user u where p.status = 0 and p.fk_penjual = u.kode_user and p.kondisi = " + kota + " and u.aktif = 0 order by p.fk_kategori,to_number(p.totalrating) desc,p.kode_produk", conn);
                    OracleDataReader reader = cmdCD.ExecuteReader();
                    brg = new List<Barang>();
                    while (reader.Read())
                    {
                        Barang temp = new Barang();
                        temp.KodeB = reader.GetString(0);
                        temp.KodeP = reader.GetString(1);
                        temp.Berat = Convert.ToInt32(reader["berat"].ToString());
                        brg.Add(temp);
                    }
                }
                conn.Close();
                for (int j = 0; j < dbp.Tables[0].Rows.Count; j++)
                {
                    int cek = 0;
                    for (int k = 0; k < search.Length; k++)
                    {
                        if (dbp.Tables[0].Rows[j]["tag"].ToString().ToUpper().Contains(search[k].ToUpper()))
                        {
                            cek = 1;
                        }
                    }
                    if (cek == 0)
                    {
                        dbp.Tables[0].Rows.RemoveAt(j);
                        brg.RemoveAt(j);
                        j--;
                    }
                }
                int max;
                if (((double)dbp.Tables[0].Rows.Count / (double)10) == (int)((double)dbp.Tables[0].Rows.Count / (double)10))
                {
                    max = (int)Math.Round(((double)dbp.Tables[0].Rows.Count / (double)10));
                }
                else
                {
                    max = (int)Math.Round(((double)dbp.Tables[0].Rows.Count / (double)10) + 0.5);
                }
                if (pages < 1) pages = 1;
                else if (pages > max) pages = max;
                page.Text = pages + "";
                int start = (pages - 1) * 10;
                int end = start + 9;
                int i = 0; int a = 0;
                while (i < dbp.Tables[0].Rows.Count)
                {
                    if (a < start || a > end)
                    {
                        dbp.Tables[0].Rows.RemoveAt(i);
                        brg.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                    a++;
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
            if (rating.SelectedIndex != -1)
            {
                if (rating.SelectedIndex == 0)
                {
                    loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
                }
                else
                {
                    loadData(Convert.ToInt32(page.Text), 1, keyword.Text, city.SelectedIndex);
                }
            }
            else
            {
                loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            city.SelectedIndex = 0;
            rating.SelectedIndex = -1;
            keyword.Text = "";
            loadData(Convert.ToInt32(page.Text), 0, "", 0);
        }

        private void Rating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rating.SelectedIndex!= -1)
            {
                if (rating.SelectedIndex == 0)
                {
                    loadData(Convert.ToInt32( page.Text), 0,keyword.Text,city.SelectedIndex);
                }
                else
                {
                    loadData(Convert.ToInt32(page.Text), 1,keyword.Text, city.SelectedIndex);
                }
            }
            else
            {
                loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
            }
        }

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rating.SelectedIndex != -1)
            {
                if (rating.SelectedIndex == 0)
                {
                    loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
                }
                else
                {
                    loadData(Convert.ToInt32(page.Text), 1, keyword.Text, city.SelectedIndex);
                }
            }
            else
            {
                loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
            }
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
            int hal = Convert.ToInt32(page.Text);
            page.Text = (hal-1) + "";
            if (rating.SelectedIndex != -1)
            {
                if (rating.SelectedIndex == 0)
                {
                    loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
                }
                else
                {
                    loadData(Convert.ToInt32(page.Text), 1, keyword.Text, city.SelectedIndex);
                }
            }
            else
            {
                loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int hal = Convert.ToInt32(page.Text);
            page.Text = (hal + 1) + "";
            if (rating.SelectedIndex != -1)
            {
                if (rating.SelectedIndex == 0)
                {
                    loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
                }
                else
                {
                    loadData(Convert.ToInt32(page.Text), 1, keyword.Text, city.SelectedIndex);
                }
            }
            else
            {
                loadData(Convert.ToInt32(page.Text), 0, keyword.Text, city.SelectedIndex);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (dgvProduct.SelectedIndex != -1)
            {
                if (amount.Text == "" || Convert.ToInt32(amount.Text) < 1)
                {
                    MessageBox.Show("Invalid Ammount");
                }
                else
                {
                    int stock = Convert.ToInt32(dbp.Tables[0].Rows[dgvProduct.SelectedIndex]["stok"].ToString());
                    int jum = Convert.ToInt32(amount.Text);
                    if (stock < jum)
                    {
                        MessageBox.Show("Sorry, Insufficient Stock");
                    }
                    else
                    {
                        DataRow dr = cart.NewRow();
                        dr["PRODUCT_NAME"] = dbp.Tables[0].Rows[dgvProduct.SelectedIndex]["nama"].ToString();
                        dr["SELLER"] = dbp.Tables[0].Rows[dgvProduct.SelectedIndex]["seller"].ToString();
                        dr["PRICE"] = dbp.Tables[0].Rows[dgvProduct.SelectedIndex]["harga"].ToString();
                        dr["AMMOUNT"] = jum;
                        dr["SUBTOTAL"] = jum * Convert.ToInt32(dbp.Tables[0].Rows[dgvProduct.SelectedIndex]["harga"].ToString());
                        cart.Rows.Add(dr);
                        listbeli.Add(brg[dgvProduct.SelectedIndex].KodeB);
                        listberat.Add(brg[dgvProduct.SelectedIndex].Berat * jum);
                        totalberat += brg[dgvProduct.SelectedIndex].Berat * jum;
                        totalbeli += jum * Convert.ToInt32(dbp.Tables[0].Rows[dgvProduct.SelectedIndex]["harga"].ToString());
                        productbeli += jum;
                        products.Text = productbeli+"";
                        price.Text = totalbeli +"";
                        amount.IsEnabled = false;
                        add.IsEnabled = false;
                        tanya.IsEnabled = false;
                        chat.IsEnabled = false;
                        submit.IsEnabled = true;
                        amount.Text = "";
                        dgvProduct.SelectedIndex = -1;
                    }
                }
            }
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            if (tanya.Text != "")
            {
                try
                {
                    string kodeHC = "", query;
                    conn.Open();
                    trans = conn.BeginTransaction();
                    //Header
                    cmd = new OracleCommand()
                    {
                        Connection = conn,
                        CommandText = "cekHC",
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        Direction = ParameterDirection.ReturnValue,
                        ParameterName = "hasil",
                        OracleDbType = OracleDbType.Varchar2,
                        Size = 20
                    });
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        Direction = ParameterDirection.Input,
                        ParameterName = "kodeB",
                        OracleDbType = OracleDbType.Varchar2,
                        Size = 20,
                        Value = kode
                    });
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        Direction = ParameterDirection.Input,
                        ParameterName = "kodeS",
                        OracleDbType = OracleDbType.Varchar2,
                        Size = 20,
                        Value = brg[dgvProduct.SelectedIndex].KodeP.ToString()
                    });
                    cmd.ExecuteNonQuery();
                    kodeHC = cmd.Parameters["hasil"].Value.ToString();
                    if (kodeHC == "Tidak Ada")
                    {
                        cmd = new OracleCommand()
                        {
                            Connection = conn,
                            CommandText = "autogenht_c",
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add(new OracleParameter()
                        {
                            Direction = ParameterDirection.ReturnValue,
                            ParameterName = "kodeht",
                            OracleDbType = OracleDbType.Varchar2,
                            Size = 20
                        });
                        cmd.ExecuteNonQuery();
                        kodeHC = cmd.Parameters["kodeht"].Value.ToString();
                        query = $"insert into th_chat values ('{kodeHC}', '{kode}', '{brg[dgvProduct.SelectedIndex].KodeP}')";
                        cmd = new OracleCommand(query, conn);
                        cmd.ExecuteNonQuery();
                    }
                    //Detail
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
                    query = $"insert into td_chat values ('{kodeDC}', '{kodeHC}', '2', '{tanya.Text}', to_date('{tanggal}','DD-MM-YYYY'), 0, 0)";
                    cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Message Sent");
                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    trans.Rollback();
                    conn.Close();
                }
                tanya.Text = "";
            }
            else
            {
                MessageBox.Show("Blank Chat");
            }
        }

        private void DgvCart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvCart.SelectedIndex != -1)
            {
                listbeli.RemoveAt(dgvCart.SelectedIndex);
                totalberat -= listberat[dgvCart.SelectedIndex];
                listberat.RemoveAt(dgvCart.SelectedIndex);
                totalbeli -= Convert.ToInt32(cart.Rows[dgvCart.SelectedIndex]["SUBTOTAL"].ToString());
                productbeli -= Convert.ToInt32(cart.Rows[dgvCart.SelectedIndex]["AMMOUNT"].ToString());
                products.Text =  productbeli +"";
                price.Text =  totalbeli +"";
                cart.Rows.RemoveAt(dgvCart.SelectedIndex);
                MessageBox.Show("Remove Cart Succeeded");
                dgvCart.SelectedIndex = -1;
            }
            if (cart.Rows.Count == 0 ) {
                submit.IsEnabled = false;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            trans = conn.BeginTransaction();
            string kodeH = "";
            try
            {
                cmd = new OracleCommand()
                {
                    Connection = conn,
                    CommandText = "autogenht",
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new OracleParameter()
                {
                    Direction = ParameterDirection.ReturnValue,
                    ParameterName = "kodeht",
                    OracleDbType = OracleDbType.Varchar2,
                    Size = 13
                });
                cmd.ExecuteNonQuery();
                kodeH = cmd.Parameters["kodeht"].Value.ToString();
                DateTime tanggal_penuh = DateTime.Now;
                string tanggal = tanggal_penuh.Day.ToString().PadLeft(2, '0') + "-" + tanggal_penuh.Month.ToString().PadLeft(2, '0') + "-" + tanggal_penuh.Year.ToString();
                cmd = new OracleCommand("INSERT INTO htrans(kode_htrans,tgl_transaksi,berat,subtotal,fk_pelanggan,status) values(:kode,to_date(:tgl,'DD-MM-YYYY'),:brt,:sub,:beli,0)", conn);
                cmd.Parameters.Add(":kode", kodeH);
                cmd.Parameters.Add(":tgl", tanggal);
                cmd.Parameters.Add(":brt", totalberat);
                cmd.Parameters.Add(":sub", totalbeli);
                cmd.Parameters.Add(":beli", kode);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < cart.Rows.Count; i++)
                {
                    DataRow dr = submitbuy.NewRow();
                    DataRow dr1 = cart.Rows[i];
                    dr["fk_htrans"] = kodeH;
                    dr["fk_produk"] = listbeli[i];
                    dr["jumlah"] = dr1["AMMOUNT"];
                    dr["harga"] = dr1["PRICE"];
                    dr["subtotal"] = dr1["SUBTOTAL"];
                    dr["status"] = 0;
                    submitbuy.Rows.Add(dr);
                }
                dabuilder.Update(submitbuy);
                buildData();
                trans.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                trans.Rollback();
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            cartBuyer cb = new cartBuyer(username, kode, kodeH);
            cb.Show();
            this.Close();
        }
    }
}
