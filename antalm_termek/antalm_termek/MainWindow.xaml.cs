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
        public int selId;
        MySqlConnection kapcs = new MySqlConnection("server = localhost;database = antalm_termek; uid = root; password = ''");
        public MainWindow()
        {
            InitializeComponent();
            frissit();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kapcs.Open();
            var sql = $"INSERT INTO antalm_termek (cikkszam, megnevezes) VALUES ('{txtCikkszam.Text}', '{txtMegnevezes.Text}');";
            new MySqlCommand(sql, kapcs).ExecuteNonQuery();
            kapcs.Close();
            frissit();
        }

        private void lbTermekek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Az éppen kiválasztott elem cikkszámát és megnevezését kiírja a szövegdobozokba
            var selectedItem = lbTermekek.SelectedItem.ToString();
            var parts = selectedItem.Split(' ');
            modCikk.Text = parts[1];
            modNev.Text = parts[2];
            selId = Convert.ToInt32(parts[0]);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            kapcs.Open();
            var sql = $"UPDATE antalm_termek SET cikkszam = '{modCikk.Text}' WHERE id= '{selId}';";
            var sql2 = $"UPDATE antalm_termek SET megnevezes = '{modNev.Text}' WHERE id= '{selId}';";
            new MySqlCommand(sql, kapcs).ExecuteNonQuery();
            new MySqlCommand(sql2, kapcs).ExecuteNonQuery();
            MessageBox.Show("Sikeres módosítás!");
            kapcs.Close();
            frissit();
        }

        private void frissit()
        {
            kapcs.Open();
            var sql = "SELECT * FROM antalm_termek;";
            var parancs = new MySqlCommand(sql, kapcs);
            var lekerdezes = parancs.ExecuteReader();
            lbTermekek.Items.Clear();
            while (lekerdezes.Read())
            {
                lbTermekek.Items.Add(lekerdezes["id"] + " " + lekerdezes["cikkszam"] + " " + lekerdezes["megnevezes"]);
            }
            lekerdezes.Close();

            kapcs.Close();
        }
    }
}
