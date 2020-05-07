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
    /// Interaction logic for NewPassword.xaml
    /// </summary>
    public partial class NewPassword : Window
    {
        string user;
        OracleConnection conn;
        OracleCommand cmd;
        string username, password;
        int role, status;
        public NewPassword( string username )
        {
            InitializeComponent();
            user = username;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                if (tpassword.Text != "")
                {
                    
                    string query = "update mh_user set  password_user = '" + tpassword.Text + "' where username_user = '" + user + "'";
                    cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("PASSWORD TIDAK BOLEH KOSONG");
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
