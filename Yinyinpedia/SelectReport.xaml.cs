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

            }
            else if (selectedReport == "Produk")
            {

            }
            else if (selectedReport == "Transaksi Penjual")
            {

            }
        }
    }
}
