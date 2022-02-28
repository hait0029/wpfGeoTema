using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Data.SqlClient;
using System.Data;

namespace wpfGeoTema
{
    /// <summary>
    /// Interaction logic for StandardUserDatabase.xaml
    /// </summary>
    public partial class StandardUserDatabase : Window
    {
        public StandardUserDatabase()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection con = new SqlConnection(@"Data Source=TEC-F-PC13\HAITTHAMSQL;Initial Catalog=GeotemaData;Integrated Security=True");


        public void LoadGrid()
        {
            //den selecter og loade data fra databasen til Standarduser
            SqlCommand cmd = new SqlCommand("select * from Users", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            Datagridcountries.ItemsSource = dt.DefaultView;
        }
        private void returnbtn_Click(object sender, RoutedEventArgs e)
        {
            //return til main page for standard user
            this.Hide();
            StandardUser STU = new StandardUser();
            STU.Show();
        }
    }
}
