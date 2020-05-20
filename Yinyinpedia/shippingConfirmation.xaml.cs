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
    /// Interaction logic for shippingConfirmation.xaml
    /// </summary>
    public partial class shippingConfirmation : Window
    {
        string username, kode, kodeDTrans;

        public shippingConfirmation(string user, string kod, string kodeD)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            kodeDTrans = kodeD;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (rating.Text != "")
            {
                try
                {
                    int a = Convert.ToInt32(rating.Text);
                    //lanjut coding di sini

                }
                catch (Exception)
                {
                    MessageBox.Show("Data Input Must Numeric");
                }
            }
            else
            {
                MessageBox.Show("Input Data is Required");
            }
            shippingBuyer sb = new shippingBuyer(username, kode);
            sb.Show();
            this.Close();
        }
    }
}
