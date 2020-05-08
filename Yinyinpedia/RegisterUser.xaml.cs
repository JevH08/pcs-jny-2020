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
using Oracle.DataAccess.Client;
using System.Data;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username;
        DataSet dbp;
        public RegisterUser()
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            LoadData();
        }

        private void LoadData()
        {
            OracleDataAdapter da = new OracleDataAdapter("select kode_user as kode,nama_user as nama,username_user as username ,email_user as email, alamat_user as alamat,telepon_user as telepon ,jenis_kelamin,decode(role,'1','ADMIN','2','PENJUAL','PEMBELI') as role  from mh_user  order by role,kode_user", conn);
            dbp = new DataSet();
            da.Fill(dbp);
        }

            private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || tusername.Text == "" || tpassword.Text == "" || address.Text == "" || email.Text == "" || city.SelectedIndex == -1 || birthDate.Text == "" || phoneNumber.Text == "")
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
                        if (dbp.Tables[0].Rows[i]["username"].ToString() == tusername.Text || dbp.Tables[0].Rows[i]["email"].ToString().ToLower() == email.Text.ToLower())
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
                        cmd.Parameters.Add(":passs", tpassword.Text);
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
                        cmd.Parameters.Add(":roleuser", "3");
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        this.Close();
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
