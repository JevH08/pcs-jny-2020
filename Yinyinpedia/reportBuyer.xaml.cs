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
                int startDate = Convert.ToInt32(start.Text);
                int endDate = Convert.ToInt32(end.Text);
            }
            else
            {
                MessageBox.Show("Data Input Is Required");
            }
        }
    }
}
