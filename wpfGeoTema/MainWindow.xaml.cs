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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace wpfGeoTema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string username;
        public static int Usertype;

        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Login button
        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            
            
            username = UsernameTextbox.Text;
            Usertype = DropDownType.SelectedIndex;
            string password = PasswordTextbox.Text;

            if (username != string.Empty && password != string.Empty)
            {
                //Connection String til sql så man kan får forbindelse men serveren
                SqlConnection con = new SqlConnection(@"Data Source=TEC-F-PC13\HAITTHAMSQL;Initial Catalog=GeotemaData;Integrated Security=True");
                con.Open();

                //Det tjekker hvis databasen har info der matcher med det der bliver bedt om
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.LoginUser WHERE username='" + this.UsernameTextbox.Text + "' AND UserPassword='" + this.PasswordTextbox.Text + "' AND userType='" + this.DropDownType.SelectedIndex.ToString() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())

                
                if (DropDownType.SelectedIndex.ToString() == "2")
                {
                    //Admins navigation
                    dr.Close();
                    Adminstrator ad = new Adminstrator();
                    ad.Show();
                    this.Hide();
                }

                else if (DropDownType.SelectedIndex.ToString() == "1")
                {
                    //superuser navigation
                    dr.Close();
                    SuperUser SU = new SuperUser();
                    SU.Show();
                    this.Hide();

                }
                else if (DropDownType.SelectedIndex.ToString() == "0")
                {
                    //user navigation
                    dr.Close();
                    StandardUser StU = new StandardUser();
                    StU.Show();
                    this.Hide();

                }
        
            }

            else
            {
               
                MessageBox.Show("Write input!!!", "FEJL 40:", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

     
    }
}
