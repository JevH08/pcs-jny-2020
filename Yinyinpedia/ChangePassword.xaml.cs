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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, kode;
        int status;
        public ChangePassword(string user, string kod, int stat)
        {
            InitializeComponent();
            username = user;
            kode = kod;
            status = stat;
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (nPassword.Text != "")
            {
                if (cNPassword.Text != "")
                {
                    if (nPassword.Text == cNPassword.Text)
                    {
                        try
                        {
                            conn.Open();
                            string query = "update mh_user set password_user = '" + nPassword.Text + "' where kode_user = '" + kode + "'";
                            cmd = new OracleCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            OracleCommand cmds = new OracleCommand()
                            {
                                CommandType = CommandType.StoredProcedure,
                                Connection = conn,
                                CommandText = "tambah"
                            };
                            cmds.Parameters.Add(new OracleParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "kode",
                                OracleDbType = OracleDbType.Varchar2,
                                Size = 70,
                                Value = username
                            });
                            cmds.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            Console.WriteLine(ex.StackTrace);
                        }
                        if (status == 1)
                        {
                            Seller p = new Seller(username, kode);
                            p.Show();
                            this.Close();
                        }
                        else if (status == 2)
                        {
                            MainWindow mw = new MainWindow();
                            mw.Show();
                            this.Close();
                        }
                        else if (status == 3)
                        {
                            Buyer b = new Buyer(username, kode);
                            b.Show();
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("New Password & Confirm New Password Doesn't Match");
                    }
                }
                else
                {
                    MessageBox.Show("Confirm New Password Field is Required");
                }
            }
            else
            {
                MessageBox.Show("New Password Field is Required");
            }
        }
    }
}
