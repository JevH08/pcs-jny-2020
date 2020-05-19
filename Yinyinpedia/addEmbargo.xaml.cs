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
    /// Interaction logic for addEmbargo.xaml
    /// </summary>
    public partial class addEmbargo : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        int mode;
        string username;

        public addEmbargo(string user)
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
            username = user;
            mode = 0;
            loadData();
        }

        public void loadData()
        {
            try
            {
                conn.Open();
                string query = "select kode_embargo as CODE, Nama_embargo as NAME, (case when status = 0 then 'Active' else 'Deleted' end) as STATUS from mh_embargo order by 1";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                dgvEmbargo.ItemsSource = null;
                dgvEmbargo.ItemsSource = db.Tables[0].DefaultView;
                query = "select count(Kode_embargo) from mh_embargo where status = 0";
                cmd = new OracleCommand(query, conn);
                embargo.Text = cmd.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void reset()
        {
            name.Text = "";
            mode = 0;
            submit.Content = "Create";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Admin a = new Admin(username);
            a.Show();
            this.Close();
        }

        private void DgvEmbargo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvEmbargo.SelectedIndex != -1)
            {
                DataRow dr = db.Tables[0].Rows[dgvEmbargo.SelectedIndex];
                if (dr["status"].ToString() != "Deleted")
                {
                    name.Text = dr["name"].ToString();
                    mode = 1;
                    submit.Content = "Update";
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                string query = "";
                if (mode == 0)
                {
                    int masuk = 0;
                    for (int i = 0; i < db.Tables[0].Rows.Count; i++)
                    {
                        if (name.Text.ToLower() == db.Tables[0].Rows[i]["name"].ToString().ToLower())
                        {
                            masuk = 1;
                        }
                    }
                    if (masuk == 0)
                    {
                        query = $"insert into mh_embargo (Nama_embargo) values('{name.Text}')";
                    }
                    else
                    {
                        MessageBox.Show("Already Registered");
                    }
                }
                else
                {
                    DataRow dr = db.Tables[0].Rows[dgvEmbargo.SelectedIndex];
                    string kode = dr["code"].ToString();
                    query = $"update mh_embargo set nama_embargo = '{name.Text}' where kode_embargo = '" + kode + "'";
                }
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            loadData();
            reset();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                DataRow dr = db.Tables[0].Rows[dgvEmbargo.SelectedIndex];
                string kode = dr["code"].ToString();
                string query = "update mh_embargo set status = 1 where kode_embargo = '" + kode + "'";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
            loadData();
            reset();
        }
    }
}
