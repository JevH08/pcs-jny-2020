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
using CrystalDecisions.CrystalReports.Engine;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for ReportPengiriman.xaml
    /// </summary>
    public partial class ReportPengiriman : Window
    {
        string username;
        DateTime startDate;

        private class Kategori
        {
            public string Kode { get; set; }
            public string Nama { get; set; }
        }

        public ReportPengiriman(string user)
        {
            InitializeComponent();
            username = user;
            loadData();
        }

        public void loadData()
        {
            month.ItemsSource = null;
            List<Kategori> kategori = new List<Kategori>();
            for (int i = 1; i < 13; i++)
            {
                Kategori temp = new Kategori();
                temp.Kode = i.ToString();
                if (i == 1)
                {
                    temp.Nama = "January";
                }
                else if (i == 2)
                {
                    temp.Nama = "February";
                }
                else if (i == 3)
                {
                    temp.Nama = "March";
                }
                else if (i == 4)
                {
                    temp.Nama = "April";
                }
                else if (i == 5)
                {
                    temp.Nama = "May";
                }
                else if (i == 6)
                {
                    temp.Nama = "June";
                }
                else if (i == 7)
                {
                    temp.Nama = "July";
                }
                else if (i == 8)
                {
                    temp.Nama = "August";
                }
                else if (i == 9)
                {
                    temp.Nama = "September";
                }
                else if (i == 10)
                {
                    temp.Nama = "October";
                }
                else if (i == 11)
                {
                    temp.Nama = "November";
                }
                else if (i == 12)
                {
                    temp.Nama = "December";
                }
                kategori.Add(temp);
            }
            month.SelectedValuePath = "Kode";
            month.DisplayMemberPath = "Nama";
            month.ItemsSource = kategori;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SelectReport sr = new SelectReport(username);
            sr.Show();
            this.Close();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            if (month.SelectedIndex != -1)
            {
                startDate = new DateTime(2000, Convert.ToInt32(month.SelectedValue.ToString()), 1);
                CrystalPengiriman rptPengiriman = new CrystalPengiriman();
                rptPengiriman.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptPengiriman.SetParameterValue("getTanggalStart", startDate);
                viewerCR.ViewerCore.ReportSource = rptPengiriman;
            }
            else
            {
                MessageBox.Show("Please Specify Which Month to Show");
            }
        }
    }
}
