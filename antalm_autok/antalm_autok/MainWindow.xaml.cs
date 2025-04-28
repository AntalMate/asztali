using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace antalm_autok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server = localhost;database = antalm_autok; uid = root; password = ''");
        public MainWindow()
        {
            InitializeComponent();
            kapcs.Open();
            //userek kiírása userek nevű listboxba
            var sql = "SELECT * FROM antalm_autok;";
            var parancs = new MySqlCommand(sql, kapcs);
            var lekerdezes = parancs.ExecuteReader();
            while (lekerdezes.Read())
            {
                lbAutok.Items.Add(lekerdezes["id"] + " " + lekerdezes["marka"] + " " + lekerdezes["tipus"]);
            }
            lekerdezes.Close();

            kapcs.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kapcs.Open();
            var sql = $"INSERT INTO antalm_autok (marka, tipus) VALUES ('{txtMarka.Text}', '{txtTipus.Text}');";
            new MySqlCommand(sql, kapcs).ExecuteNonQuery();
            lbAutok.Items.Clear();
            var parancs = new MySqlCommand("SELECT * FROM antalm_autok;", kapcs);
            var lekerdezes = parancs.ExecuteReader();
            while (lekerdezes.Read())
            {
                lbAutok.Items.Add(lekerdezes["id"] + " " + lekerdezes["marka"] + " " + lekerdezes["tipus"]);
            }
            lekerdezes.Close();

            kapcs.Close();
        }
    }
}
