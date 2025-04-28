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

namespace antalm_termek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server = localhost;database = antalm_termek; uid = root; password = ''");
        public MainWindow()
        {
            InitializeComponent();
            InitializeComponent();
            kapcs.Open();
            var sql = "SELECT * FROM antalm_termek;";
            var parancs = new MySqlCommand(sql, kapcs);
            var lekerdezes = parancs.ExecuteReader();
            while (lekerdezes.Read())
            {
                lbTermekek.Items.Add(lekerdezes["id"] + " " + lekerdezes["cikkszam"] + " " + lekerdezes["megnevezes"]);
            }
            lekerdezes.Close();

            kapcs.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kapcs.Open();
            var sql = $"INSERT INTO antalm_termek (cikkszam, megnevezes) VALUES ('{txtCikkszam.Text}', '{txtMegnevezes.Text}');";
            new MySqlCommand(sql, kapcs).ExecuteNonQuery();
            lbTermekek.Items.Clear();
            var parancs = new MySqlCommand("SELECT * FROM antalm_termek;", kapcs);
            var lekerdezes = parancs.ExecuteReader();
            while (lekerdezes.Read())
            {
                lbTermekek.Items.Add(lekerdezes["id"] + " " + lekerdezes["cikkszam"] + " " + lekerdezes["megnevezes"]);
            }
            lekerdezes.Close();

            kapcs.Close();
        }
    }
}
