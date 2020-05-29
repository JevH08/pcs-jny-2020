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
    /// Interaction logic for reportBuyer.xaml
    /// </summary>
    public partial class reportBuyer : Window
    {
        string username, kode;

        public reportBuyer(string user, string kod)
        {
            InitializeComponent();
            username = user;
            kode = kod;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Buyer b = new Buyer(username, kode);
            b.Show();
            this.Close();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            if (start.Text != "" && end.Text != "")
            {
                string[] startDate = start.Text.Split('-');
                string[] endDate = end.Text.Split('-');
                DateTime startingTime = new DateTime(Convert.ToInt32(startDate[2]), Convert.ToInt32(startDate[1]), Convert.ToInt32(startDate[0]));
                DateTime endTime = new DateTime(Convert.ToInt32(endDate[2]), Convert.ToInt32(endDate[1]), Convert.ToInt32(endDate[0]));
                CrystalBuyerMainReport rptMainBuyer = new CrystalBuyerMainReport();
                CrystalBuyerSubReport rptSubBuyer = new CrystalBuyerSubReport();
                rptMainBuyer.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptSubBuyer.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptMainBuyer.SetParameterValue("startTime", startingTime);
                rptMainBuyer.SetParameterValue("endTime", endTime);
                rptMainBuyer.SetParameterValue("userName", username);
                rptSubBuyer.SetParameterValue("startTime", startingTime);
                rptSubBuyer.SetParameterValue("endTime", endTime);
                viewerCR.ViewerCore.ReportSource = rptMainBuyer;
            }
            else
            {
                MessageBox.Show("Data Input Is Required");
            }
        }
    }
}
