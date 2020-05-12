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

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for SelectReport.xaml
    /// </summary>
    public partial class SelectReport : Window
    {
        public SelectReport()
        {
            InitializeComponent();
        }

        private void btn_PilihReport_Click(object sender, RoutedEventArgs e)
        {
            string selectedReport = cb_SelectReport.Text;
            if(selectedReport == "Jasa Pengiriman")
            {
                ReportPengiriman rptKirim = new ReportPengiriman();
                rptKirim.Show();
                this.Close();
            }
            else if (selectedReport == "Produk")
            {
                ReportProduk rptProduk = new ReportProduk();
                rptProduk.Show();
                this.Close();
            }
            else if (selectedReport == "Transaksi Penjual")
            {
                ReportTransaksiPenjual rptTransPen = new ReportTransaksiPenjual();
                rptTransPen.Show();
                this.Close();
            }
        }
    }
}
