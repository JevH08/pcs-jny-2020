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

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for InsertUsername.xaml
    /// </summary>
    public partial class InsertUsername : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, password;
        int role, status;
        public InsertUsername()
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                username = tusername.Text;
                cmd = new OracleCommand("select count(kode_user) from mh_user where username_user = '" + username + "' ", conn);
                int user1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                if (user1 == 0)
                {
                    MessageBox.Show("PASTIKAN USERNAME BENAR");
                }
                else
                {
                    
                    cmd = new OracleCommand("select role from mh_user where username_user = '" + username + "' ", conn);
                    role = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (role == 2)
                    {
                        MessageBox.Show("Password anda sama dengan username anda");
                        string query = "update mh_user set status = 0,  password_user = '" + username + "' where username_user = '" + username + "'";
                        cmd = new OracleCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        this.Close();
                    }
                    else if (role == 3)
                    {
                        NewPassword np = new NewPassword(tusername.Text);
                        np.ShowDialog();
                        this.Close();
                    }
                    
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
