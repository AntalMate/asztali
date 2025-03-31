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
        MySqlConnection kapcs = new MySqlConnection("server=localhost; database=asztali_11a; uid=root; password='';");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         kapcs.Open();
            var sql = $"SELECT * FROM user WHERE nev= '{usern.Text}'  AND jelszo='{passw.Text}';";
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
            kapcs.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (regPass.Password == regPassAgain.Password)
            {
                MessageBox.Show("A két jelszó megegyezik! sigma");
            }
        }
    }
}
