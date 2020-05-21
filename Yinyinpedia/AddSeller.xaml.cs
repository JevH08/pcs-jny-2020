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
        DataSet dbp;
        string username;

        public AddSeller(string user)
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            username = user;
            male.IsChecked = true;
            LoadData(1);
        }

        private void LoadData(int pages)
        {
            conn.Open();
            string query = "select kode_user as CODE,nama_user as NAME,username_user as USERNAME ,email_user as EMAIL, alamat_user as ADDRESS,telepon_user as TELEPHONE,norek as BANK ,jenis_kelamin as GENDER,(case when role = 1 then 'Admin' when role = 2 then 'Seller' else 'Buyer' end) as ROLE  from mh_user order by ROLE, CODE";
            cmd = new OracleCommand(query, conn);
            cmd.ExecuteReader();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            dbp = new DataSet();
            da.Fill(dbp);
            conn.Close();
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
            

            cmd = new OracleCommand()
            {
                Connection = conn,
                CommandText = "hitung",
                CommandType = CommandType.StoredProcedure
            };
            //peran sm hitung terbalik namanya 
            cmd.Parameters.Add(new OracleParameter()
            {
                Direction = ParameterDirection.ReturnValue,
                ParameterName = "peran",
                OracleDbType = OracleDbType.Int32
            });
            cmd.Parameters.Add(new OracleParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "hitung",
                OracleDbType = OracleDbType.Varchar2,
                Value = "3"
            });
            conn.Open();
            cmd.ExecuteNonQuery();
            jumpen.Text = cmd.Parameters["peran"].Value.ToString();
            conn.Close();

            cmd.Parameters[1].Value = "3";
            conn.Open();
            cmd.ExecuteNonQuery();
            jumpem.Text = cmd.Parameters["peran"].Value.ToString();
            conn.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Admin a = new Admin(username);
            a.Show();
            this.Close();
        }

        private void DgvSeller_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRow dr = dbp.Tables[0].Rows[dgvSeller.SelectedIndex];
            if (dr["role"].ToString() != "Admin")
            {
                if (MessageBox.Show("Are You Sure Want to Delete " + dr["username"].ToString() + " ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string kode = dr["code"].ToString();
                        string query = "delete from mh_user where kode_user = '" + kode + "'";
                        cmd = new OracleCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        Console.WriteLine(ex.StackTrace);
                    }
                    LoadData(Convert.ToInt32(page.Text));
                }
            }
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
            if (name.Text == "" || tusername.Text == "" ||  address.Text == "" || email.Text == "" || city.SelectedIndex == -1 || birthDate.Text == "" || phoneNumber.Text == "" || norek.Text =="")
            {
                MessageBox.Show("Data Input Is Required");
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
                        MessageBox.Show("Username / Email Already Used");
                    }
                    else
                    {
                        cmd = new OracleCommand("INSERT INTO mh_user (nama_user,username_user,password_user,email_user,alamat_user,kota_user,telepon_user,norek,jenis_kelamin,tgl_lahir,role) values(:nama,:users,:passs,:email,:alamat,:kota,:telp,:bank,:jenis,to_date(:lahir,'DD-MM-YYYY'),:roleuser)", conn);

                        cmd.Parameters.Add(":nama", name.Text.ToUpper());
                        cmd.Parameters.Add(":users", tusername.Text);
                        cmd.Parameters.Add(":passs", tusername.Text);
                        cmd.Parameters.Add(":email", email.Text);
                        cmd.Parameters.Add(":alamat", address.Text);
                        cmd.Parameters.Add(":kota", city.Text.ToUpper());
                        cmd.Parameters.Add(":telp", phoneNumber.Text);
                        cmd.Parameters.Add(":bank", norek.Text);
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
                        name.Text = ""; tusername.Text = ""; address.Text = ""; email.Text = ""; city.SelectedIndex = -1; birthDate.Text = ""; phoneNumber.Text = "";
                        male.IsChecked = true;
                        female.IsChecked = false;
                    }
                }
                else
                {
                    MessageBox.Show("Email is not Valid");
                }
            }
        }
    }
}
