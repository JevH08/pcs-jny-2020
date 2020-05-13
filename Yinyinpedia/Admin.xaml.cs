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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        string username;

        public Admin(string user)
        {
            InitializeComponent();
            username = user;
            header.Text = "WELCOME, ADMIN " + username.ToUpper();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void AddSeller_Click(object sender, RoutedEventArgs e)
        {
            AddSeller addSeller = new AddSeller(username);
            addSeller.Show();
            this.Close();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategory addCategory = new AddCategory(username);
            addCategory.Show();
            this.Close();
        }

        private void AddDeliveryService_Click(object sender, RoutedEventArgs e)
        {
            DeliveryServices delivery = new DeliveryServices(username);
            delivery.Show();
            this.Close();
        }

        private void Verification_Click(object sender, RoutedEventArgs e)
        {
            ProductVerification p = new ProductVerification(username);
            p.Show();
            this.Close();
        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {
            SelectReport selectR = new SelectReport(username);
            selectR.Show();
            this.Close();
        }
    }
}
