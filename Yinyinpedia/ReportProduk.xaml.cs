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
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for ReportProduk.xaml
    /// </summary>
    public partial class ReportProduk : Window
    {
        string selectedCategory = "";
        string sqlConn = @"Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
        public ReportProduk()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string queryGetCategory = "SELECT Nama_Kategori FROM MH_Kategori";
            OracleConnection conn = new OracleConnection(sqlConn);
            conn.Open();
            OracleCommand getCategory = new OracleCommand(queryGetCategory, conn);
            OracleDataReader readerCategory = getCategory.ExecuteReader();
            List<string> listCategory = new List<string>();
            while (readerCategory.Read())
            {
                listCategory.Add(readerCategory.GetString(0));
            }
            conn.Close();
            cb_Kategori.ItemsSource = listCategory;
        }

        private void btnGenerate_RptProduk_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = cb_Kategori.Text;
            CrystalProduk rptProduk = new CrystalProduk();
            rptProduk.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
            rptProduk.SetParameterValue("parameterKategoriProduk", selectedCategory);
            viewerProduk.ViewerCore.ReportSource = rptProduk;
        }

    }
}
