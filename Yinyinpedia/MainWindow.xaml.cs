﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.DataAccess.Client;

namespace Yinyinpedia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleConnection conn;
        OracleCommand cmd;
        string username, password;
        int role, status;

        public MainWindow()
        {
            InitializeComponent();
            string datasource = "Data Source=orcl;User id=proyekpcs;Password=proyekpcs";
            conn = new OracleConnection(datasource);
        }

        private void Tusername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Forgot_Click(object sender, RoutedEventArgs e)
        {
            InsertUsername iu = new InsertUsername();
            iu.ShowDialog();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                username = tusername.Text;
                password = tpassword.Text;
                cmd = new OracleCommand("select count(kode_user) from mh_user where username_user = '" + username + "' ", conn);
                int user1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd = new OracleCommand("select count(kode_user) from mh_user where password_user = '" + password + "' ", conn);
                int user = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                if (user1 == 0)
                {
                    MessageBox.Show("BELUM TERDAFTAR");
                }
                else
                {
                    if (user == 0)
                    {
                        MessageBox.Show("PASSWORD SALAH");
                    }
                    else
                    {
                        cmd = new OracleCommand("select role from mh_user where username_user = '" + username + "' and password_user = '" + password + "' ", conn);
                        role = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        if (role == 1)
                        {
                            Admin a = new Admin(username);
                            a.ShowDialog();
                        }
                        else if (role == 2)
                        {
                            cmd = new OracleCommand("select status from mh_user where username_user = '" + username + "' and password_user = '" + password + "' ", conn);
                            status = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                            if (status == 0)
                            {
                                ChangePassword cp = new ChangePassword(username);
                                cp.ShowDialog();
                            }
                            Seller p = new Seller(username);
                            p.ShowDialog();
                        }
                        else if (role == 3)
                        {
                            Buyer b = new Buyer(username);
                            b.ShowDialog();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Signin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}