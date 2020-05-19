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
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for ReportProduk.xaml
    /// </summary>
    public partial class ReportProduk : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username;

        private class Kategori
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }

        public ReportProduk(string user)
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            username = user;
            bestSelling.IsChecked = true;
            category.IsEnabled = true;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                category.ItemsSource = null;
                string query = "select * from mh_kategori where status = 0 order by Nama_kategori";
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

        private void BestSelling_Checked(object sender, RoutedEventArgs e)
        {
            category.IsEnabled = true;
        }

        private void Unverified_Checked(object sender, RoutedEventArgs e)
        {
            category.IsEnabled = false;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            CrystalProduk rptProduk = new CrystalProduk();
            rptProduk.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
            if ((bool) bestSelling.IsChecked)
            {
                rptProduk.SetParameterValue("parameterKategoriProduk", category.Text);
            }
            else if ((bool)unverified.IsChecked)
            {

            }
            viewerCR.ViewerCore.ReportSource = rptProduk;
        }
    }
}
