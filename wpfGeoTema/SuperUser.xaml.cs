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

namespace wpfGeoTema
{
    /// <summary>
    /// Interaction logic for SuperUser.xaml
    /// </summary>
    public partial class SuperUser : Window
    {
        public SuperUser()
        {
            InitializeComponent();
        }

        private void view_Geotemadatabase_Click(object sender, RoutedEventArgs e)
        {
            //det er for at navigere til databasen for Super user
            this.Hide();
            DatabaseForSuperuser dfs = new DatabaseForSuperuser();
            dfs.Show();
        }

        private void returnbtn_Click(object sender, RoutedEventArgs e)
        {
            //return til login screen
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Hide();
        }
    }
}
