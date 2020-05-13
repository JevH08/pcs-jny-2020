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
        string username;
        int role;
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
                cmd = new OracleCommand("select count(kode_user) from mh_user where username_user = '" + username + "'  and email_user = '" + temail.Text + "'", conn);
                int user1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                conn.Close();
                if (user1 == 0)
                {
                    MessageBox.Show("Make Sure The Username and Email are Correct");
                }
                else
                {
                    conn.Open();
                    cmd = new OracleCommand("select role from mh_user where username_user = '" + username + "'  and email_user = '" + temail.Text + "'", conn);
                    int role = Convert.ToInt32( cmd.ExecuteScalar().ToString() );
                    conn.Close();
                    if (role != 1)
                    {
                        conn.Open();
                        cmd = new OracleCommand("select kode_user from mh_user where username_user = '" + username + "'  and email_user = '" + temail.Text + "'", conn);
                        string kod = cmd.ExecuteScalar().ToString();
                        conn.Close();
                        ChangePassword cp = new ChangePassword(username, kod, 2);
                        cp.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Make Sure The Username and Email are Correct");
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
