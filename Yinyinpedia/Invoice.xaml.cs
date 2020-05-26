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
using CrystalDecisions.Shared;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {
        string username, kode,kodeht;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CrystalReport1 rpt = new CrystalReport1();
            //login 
            rpt.SetDatabaseLogon("proyekpcs", "proyekpcs");
            
            ParameterDiscreteValue paramKode = new ParameterDiscreteValue();
            paramKode.Value = kodeht;
            rpt.SetParameterValue("kodehtrans", paramKode);

            //sambungkan ke crystal report
            invoice.ViewerCore.ReportSource = rpt;
        }

        public Invoice(string user,string kod,string kodht)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            kodeht = kodht;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Buyer b = new Buyer(username, kode);
            b.Show();
            this.Close();
        }
    }
}
