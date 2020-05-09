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
    /// Interaction logic for DeliveryServices.xaml
    /// </summary>
    public partial class DeliveryServices : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, kodepilih;
        DataSet dbp;

        public DeliveryServices(string user)
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            username = user;
            LoadData();
        }

        private void LoadData()
        {
            OracleDataAdapter da = new OracleDataAdapter("select kode_distributor as CODE,nama_distributor as NAME,harga_per_kilo as PRICE,batas_harga as MINIMUM,diskon as DISCOUNT from mh_distributor where status = 0  order by kode_distributor", conn);
            dbp = new DataSet();
            da.Fill(dbp);
            dgvDelivery.ItemsSource = null;
            dgvDelivery.ItemsSource = dbp.Tables[0].DefaultView;
            name.Text = ""; price.Text = ""; discount.Text = ""; minimum.Text = "";
            string query = "select count(kode_distributor) from mh_distributor where status = 0";
            cmd = new OracleCommand(query, conn);
            conn.Open();
            delivery.Text = cmd.ExecuteScalar().ToString();
            conn.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Admin a = new Admin(username);
            a.ShowDialog();
            this.Close();
        }

        private void DgvDelivery_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvDelivery.SelectedIndex != -1)
            {
                DataRow r = dbp.Tables[0].Rows[dgvDelivery.SelectedIndex];
                kodepilih = r["code"].ToString();
                name.Text = r["name"].ToString();
                price.Text = r["price"].ToString();
                minimum.Text = r["minimum"].ToString();
                discount.Text = r["discount"].ToString();
                submit.Content = "Update";
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (submit.Content.ToString() == "Create")
            {
                if (name.Text != "" && price.Text != "" && minimum.Text != "" && discount.Text != "")
                {
                    int masuk = 0;
                    for (int i = 0; i < dbp.Tables[0].Rows.Count; i++)
                    {
                        if (name.Text.ToLower() == dbp.Tables[0].Rows[i]["nama"].ToString().ToLower())
                        {
                            masuk = 1;
                        }
                    }
                    if (masuk == 0)
                    {
                        cmd = new OracleCommand("INSERT INTO mh_distributor (nama_distributor,harga_per_kilo,batas_harga,diskon) values(:nama,:hargakilo,:batas,:potongan)", conn);

                        cmd.Parameters.Add(":nama", name.Text);
                        cmd.Parameters.Add(":hargakilo", Convert.ToInt32(price.Text));
                        cmd.Parameters.Add(":batas", Convert.ToInt32(minimum.Text));
                        cmd.Parameters.Add(":potongan", Convert.ToInt32(discount.Text));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("SUDAH PERNAH TERDAFTAR");
                    }
                }
                else
                {
                    MessageBox.Show("DATA TIDAK VALID");
                }
            }
            else
            {
                cmd = new OracleCommand("UPDATE mh_distributor SET nama_distributor = :nama, harga_per_kilo = :price, batas_harga = :minim, diskon = :disk WHERE kode_distributor = :kode", conn);

                cmd.Parameters.Add(":nama", name.Text);
                cmd.Parameters.Add(":price", Convert.ToInt32(price.Text));
                cmd.Parameters.Add(":minim", Convert.ToInt32(minimum.Text));
                cmd.Parameters.Add(":disk", Convert.ToInt32(discount.Text));
                cmd.Parameters.Add(":kode", kodepilih);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadData();
                submit.Content = "Create";
                kodepilih = "";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (kodepilih != "")
            {
                cmd = new OracleCommand("UPDATE mh_distributor SET status = :status WHERE kode_distributor = :kode", conn);

                cmd.Parameters.Add(":status", 1);
                cmd.Parameters.Add(":kode", kodepilih);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadData();
                submit.Content = "Create";
                kodepilih = "";
            }
        }
    }
}
