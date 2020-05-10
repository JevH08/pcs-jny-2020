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
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataSet db = new DataSet();
        int mode;
        string username;

        public AddCategory(string user)
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
                string query = "select Kode_Kategori as CODE, Nama_Kategori as NAME, (case when status = 0 then 'Active' else 'Deleted' end) as STATUS from mh_kategori order by 1";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                dgvCategory.ItemsSource = null;
                dgvCategory.ItemsSource = db.Tables[0].DefaultView;
                query = "select count(Kode_kategori) from mh_kategori where status = 0";
                cmd = new OracleCommand(query, conn);
                category.Text = cmd.ExecuteScalar().ToString();
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
            a.ShowDialog();
            this.Close();
        }

        private void DgvCategory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvCategory.SelectedIndex != -1)
            {
                DataRow dr = db.Tables[0].Rows[dgvCategory.SelectedIndex];
                name.Text = dr["name"].ToString();
                mode = 1;
                submit.Content = "Update";
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
                    query = $"insert into mh_kategori (Nama_kategori, status) values('{name.Text}', 0)";
                }
                else
                {
                    DataRow dr = db.Tables[0].Rows[dgvCategory.SelectedIndex];
                    string kode = dr["code"].ToString();
                    query = $"update mh_kategori set Nama_kategori = '{name.Text}' where Kode_kategori = '" + kode + "'";
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
                DataRow dr = db.Tables[0].Rows[dgvCategory.SelectedIndex];
                string kode = dr["code"].ToString();
                string query = "update mh_kategori set status = 1 where Kode_kategori = '" + kode + "'";
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
