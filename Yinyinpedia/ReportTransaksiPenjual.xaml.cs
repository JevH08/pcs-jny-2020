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
    /// Interaction logic for ReportTransaksiPenjual.xaml
    /// </summary>
    public partial class ReportTransaksiPenjual : Window
    {
        DateTime startDate;
        public ReportTransaksiPenjual()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cb_Bulan.Items.Add("Januari");
            cb_Bulan.Items.Add("Februari");
            cb_Bulan.Items.Add("Maret");
            cb_Bulan.Items.Add("April");
            cb_Bulan.Items.Add("Mei");
            cb_Bulan.Items.Add("Juni");
            cb_Bulan.Items.Add("Juli");
            cb_Bulan.Items.Add("Agustus");
            cb_Bulan.Items.Add("September");
            cb_Bulan.Items.Add("Oktober");
            cb_Bulan.Items.Add("November");
            cb_Bulan.Items.Add("Desember");
        }

        private void btnGenerate_RptProduk_Click(object sender, RoutedEventArgs e)
        {
            if(cb_Bulan.Text == "Januari")
            {
                startDate = new DateTime(2000, 1, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "Februari")
            {
                startDate = new DateTime(2000, 2, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "Maret")
            {
                startDate = new DateTime(2000, 3, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "April")
            {
                startDate = new DateTime(2000, 4, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "Mei")
            {
                startDate = new DateTime(2000, 5, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "Juni")
            {
                startDate = new DateTime(2000, 6, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "Juli")
            {
                startDate = new DateTime(2000, 7, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "Agustus")
            {
                startDate = new DateTime(2000, 8, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "September")
            {
                startDate = new DateTime(2000, 9, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "Oktober")
            {
                startDate = new DateTime(2000, 10, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "November")
            {
                startDate = new DateTime(2000, 11, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            else if (cb_Bulan.Text == "Desember")
            {
                startDate = new DateTime(2000, 12, 1);
                CrystalTransaksiPenjual rptTransPenjual = new CrystalTransaksiPenjual();
                rptTransPenjual.SetDatabaseLogon("proyekpcs", "proyekpcs", "orcl", "");
                rptTransPenjual.SetParameterValue("getTanggalStart", startDate);
                viewerLaporanTransaksi.ViewerCore.ReportSource = rptTransPenjual;
            }
            
        }
    }
}
