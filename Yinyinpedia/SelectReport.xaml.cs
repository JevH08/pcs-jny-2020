using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        string user;

        public SelectReport(string username)
        {
            InitializeComponent();
            user = username;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Admin a = new Admin(user);
            a.Show();
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (cbReport.SelectedIndex == 0)
            {
                ReportPengiriman rptKirim = new ReportPengiriman(user);
                rptKirim.Show();
                this.Close();
            }
            else if (cbReport.SelectedIndex == 1)
            {
                ReportProduk rptProduk = new ReportProduk(user);
                rptProduk.Show();
                this.Close();
            }
            else if (cbReport.SelectedIndex == 2)
            {
                ReportTransaksiPenjual rptTransPen = new ReportTransaksiPenjual(user);
                rptTransPen.Show();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Please Select a Report");
            }
        }
    }
}
