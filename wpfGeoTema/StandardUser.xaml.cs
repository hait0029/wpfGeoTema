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
    /// Interaction logic for StandardUser.xaml
    /// </summary>
    public partial class StandardUser : Window
    {
        public StandardUser()
        {
            InitializeComponent();
        }

        private void view_Geotemadatabase_Click(object sender, RoutedEventArgs e)
        {
            //det er for at navigere til databasen for normal user
            this.Hide();
            StandardUserDatabase STUB = new StandardUserDatabase();
            STUB.Show();
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
