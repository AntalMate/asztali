using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mysqllogin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server = server.fh2.hu;database = v2labgwj_11a; uid = v2labgwj_11a; password = 'VGFR2GJjqudMt8Q4SA5j'");
        public MainWindow()
        {
            InitializeComponent();
            kapcs.Open();
            //userek kiírása userek nevű listboxba
            var sql = "SELECT * FROM antalmd_user;";
            var parancs = new MySqlCommand(sql, kapcs);
            var reader = parancs.ExecuteReader();
            while (reader.Read())
            {
                userek.Items.Add(reader["nev"].ToString());
            }
            reader.Close();

            kapcs.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kapcs.Open();
            var sql = $"SELECT * FROM antalmd_user WHERE nev= '{usern.Text}'  AND jelszo='{passw.Text}';";
            lbDebug.Content = sql;
            var parancs = new MySqlCommand(sql, kapcs);
            var reader = parancs.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Sikeres bejelentkezés!");
            }
            else
            {
                MessageBox.Show("Sikertelen bejelentkezés!");
            }
            reader.Close();
            kapcs.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (regPass.Password != regPassAgain.Password)
            {
                MessageBox.Show("A két jelszó nem egyezik meg! nemsigma");
                return;
            }
            kapcs.Open();
            // ha már létezik a felhasználó
            var reader = new MySqlCommand($"SELECT * FROM antalmd_user WHERE nev= '{regUser.Text}'", kapcs).ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Ez a felhasználónév már létezik! nemsigma");
            }
            else
            {
                reader.Close();
                //nincs ilyen felhasználó lehet regisztrálni
                var sql = $"INSERT INTO antalmd_user (nev, jelszo) VALUES ('{regUser.Text}', '{regPass.Password}');";
                lbDebug.Content = sql;
                new MySqlCommand(sql, kapcs).ExecuteNonQuery();
                userek.Items.Add(regUser.Text);
                MessageBox.Show("Sikeres regisztráció!");
            }
            kapcs.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //a már bejelentkezett felhasználó jelszavának frissítője az oldPass, newPass, newPassAgain textboxok segítségével
            kapcs.Open();
            var reader = new MySqlCommand($"SELECT * FROM antalmd_user WHERE nev= '{usern.Text}'", kapcs).ExecuteReader();
            if (reader.Read()) {
                if (reader["jelszo"].ToString() == oldPass.Password)
                {
                    if (newPass.Password == newPassAgain.Password)
                    {
                        //jelszó frissítése
                        var sql = $"UPDATE antalmd_user SET jelszo = '{newPass.Password}' WHERE nev= '{usern.Text}';";
                        reader.Close();
                        new MySqlCommand(sql, kapcs).ExecuteNonQuery();
                        MessageBox.Show("Sikeres jelszóváltoztatás!");
                    }
                    else
                    {
                        MessageBox.Show("A két új jelszó nem egyezik meg! nemsigma");
                    }
                }
                else
                {
                    MessageBox.Show("A régi jelszó nem megfelelő! nemsigma");
                }
            }
            else
            {
                MessageBox.Show("Nincs ilyen felhasználó! nemsigma");


            }
            
            
        }
    }
}
