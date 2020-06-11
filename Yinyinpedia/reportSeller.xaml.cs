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
    /// Interaction logic for reportSeller.xaml
    /// </summary>
    public partial class reportSeller : Window
    {
        string username, kode;

        public reportSeller(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Seller s = new Seller(username, kode);
            s.Show();
            this.Close();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            if (start.Text != "" && end.Text != "")
            {
                try
                {
                    int startYear = Convert.ToInt32(start.Text);
                    int endYear = Convert.ToInt32(end.Text);
                    DateTime paramStartYear = new DateTime(startYear-1, 1, 1);
                    DateTime paramEndYear = new DateTime(endYear+1, 12, 31);
                    CrystalPenjualanSeller rptPenjSeller = new CrystalPenjualanSeller();
                    rptPenjSeller.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                    rptPenjSeller.SetParameterValue("getTanggalStart", paramStartYear);
                    rptPenjSeller.SetParameterValue("getTanggalEnd", paramEndYear);
                    rptPenjSeller.SetParameterValue("kode", kode);
                    viewerCR.ViewerCore.ReportSource = rptPenjSeller;
                }
                catch (Exception)
                {
                    MessageBox.Show("Data Input Must Numeric");
                }
            }
            else
            {
                MessageBox.Show("Data Input Is Required");
            }
        }
    }
}
