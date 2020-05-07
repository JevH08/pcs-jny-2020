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
using System.Data;
using Oracle.DataAccess.Client;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for AddSeller.xaml
    /// </summary>
    public partial class AddSeller : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username;
        DataSet dbp;

        public AddSeller(string user)
        {
            InitializeComponent();
            username = user;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            LoadData(1);
        }

        private void LoadData(int pages)
        {
            OracleDataAdapter da = new OracleDataAdapter("select kode_user as kode,nama_user as nama,username_user as username ,email_user as email, alamat_user as alamat,telepon_user as telepon ,jenis_kelamin,decode(role,'1','ADMIN','2','PENJUAL','PEMBELI') as role  from mh_user  order by role,kode_user", conn);
            dbp = new DataSet();
            da.Fill(dbp);
            int max;
            if (((double)dbp.Tables[0].Rows.Count / (double)10) == (int)((double)dbp.Tables[0].Rows.Count / (double)10))
            {
                max = (int)Math.Round(((double)dbp.Tables[0].Rows.Count / (double)10));
            }
            else
            {
                max = (int)Math.Round(((double)dbp.Tables[0].Rows.Count / (double)10) + 0.5);
            }
            if (pages < 1) pages = 1;
            else if (pages > max) pages = max;
            page.Text = pages + "";
            int start = (pages - 1) * 10;
            int end = start + 9;
            int i = 0; int a = 0;
            while (i < dbp.Tables[0].Rows.Count)
            {
                if (a < start || a > end)
                {
                    dbp.Tables[0].Rows.RemoveAt(i);
                }
                else
                {
                    i++;
                }
                a++;
            }
            dgvSeller.ItemsSource = null;
            dgvSeller.ItemsSource = dbp.Tables[0].DefaultView;
            

            name.Text = ""; tusername.Text = "";address.Text = ""; email.Text = ""; city.SelectedIndex = -1; birthDate.Text = ""; phoneNumber.Text = "";
            male.IsChecked = true;
            female.IsChecked = true;
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            int p1 = Convert.ToInt32(page.Text) - 1;
            page.Text = p1 + "";
            LoadData(p1);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int p1 = Convert.ToInt32(page.Text) + 1;
            page.Text = p1 + "";
            LoadData(p1);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || tusername.Text == "" ||  address.Text == "" || email.Text == "" || city.SelectedIndex == -1 || birthDate.Text == "" || phoneNumber.Text == "")
            {
                MessageBox.Show("DATA TIDAK LENGKAP");
            }
            else
            {
                if (email.Text.Contains("@") && email.Text.Contains("."))
                {
                    int daftar = 0;
                    for (int i = 0; i < dbp.Tables[0].Rows.Count; i++)
                    {
                        if (dbp.Tables[0].Rows[i]["username"].ToString()== tusername.Text|| dbp.Tables[0].Rows[i]["email"].ToString().ToLower() == email.Text.ToLower())
                        {
                            daftar = 1;
                        }
                    }
                    if (daftar == 1)
                    {
                        MessageBox.Show("USERNAME / EMAIL SUDAH PERNAH TERDAFTAR");
                    }
                    else
                    {
                        OracleCommand cmd = new OracleCommand("INSERT INTO mh_user (nama_user,username_user,password_user,email_user,alamat_user,kota_user,telepon_user,jenis_kelamin,tgl_lahir,role) values(:nama,:users,:passs,:email,:alamat,:kota,:telp,:jenis,to_date(:lahir,'DD-MM-YYYY'),:roleuser)", conn);

                        cmd.Parameters.Add(":nama", name.Text.ToUpper());
                        cmd.Parameters.Add(":users", tusername.Text);
                        cmd.Parameters.Add(":passs", tusername.Text);
                        cmd.Parameters.Add(":email", email.Text.ToUpper());
                        cmd.Parameters.Add(":alamat", address.Text.ToUpper());
                        cmd.Parameters.Add(":kota", city.Text.ToUpper());
                        cmd.Parameters.Add(":telp", phoneNumber.Text);
                        if ((bool)female.IsChecked == true)
                        {
                            cmd.Parameters.Add(":jenis", "P");
                        }
                        else if ((bool)male.IsChecked == true)
                        {
                            cmd.Parameters.Add(":jenis", "L");
                        }
                        cmd.Parameters.Add(":lahir", birthDate.Text);
                        cmd.Parameters.Add(":roleuser", "2");
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LoadData(Convert.ToInt32(page.Text));
                    }
                }
                else
                {
                    MessageBox.Show("EMAIL TIDAK VALID");
                }
            }
        }
    }
}
