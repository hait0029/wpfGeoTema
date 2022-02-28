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
    /// Interaction logic for AdminsCrud.xaml
    /// </summary>
    public partial class AdminsCrud : Window
    {
        //Den loader AdminsCrud window med databasen
        public AdminsCrud()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection con = new SqlConnection(@"Data Source=TEC-F-PC13\HAITTHAMSQL;Initial Catalog=GeotemaData;Integrated Security=True");
        
        //her fortæller vi clear knap hvilke text tekst bokse skal slettes
        public void Cleardata()
        {
            usernameinfo.Clear();
            Passwordinfo.Clear();
            SearchUser.Clear();
        }

        //Her modtager vi hvilke data fra databasen vi vil gerne have
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from Users", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            Datagridusers.ItemsSource = dt.DefaultView;
        }

        //det command for clear knappen at den skal slette
        private void ClearDatabtn_Click(object sender, RoutedEventArgs e)
        {
            Cleardata();

        }
        // det er et tjekke system til Crud hvis man glemmer at skrive f.eks. navn eller password så får man et pop der fortæller hvad der mangler
        public bool isValid()
        {
            if(usernameinfo.Text == String.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Passwordinfo.Text == String.Empty)
            {
                MessageBox.Show("Password is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        //Dette er tiløje knappen
        private void AddUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    //Her fortæller vi programmet hvor den skal insert info
                    SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES (@Username, @Password)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", usernameinfo.Text);
                    cmd.Parameters.AddWithValue("@Password", Passwordinfo.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Succesfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    Cleardata();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Dette er opdatere knappen
        private void UpdateUserbtn_Click(object sender, RoutedEventArgs e)
        {
            //Her fortæller vi programmet hvor den skal update info
            con.Open();
            SqlCommand cmd = new SqlCommand("update Users set Username = '" + usernameinfo.Text + "', Password = '" + Passwordinfo.Text + "' WHERE ID = '"+SearchUser.Text+"'", con);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been updated", "updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                Cleardata();
                LoadGrid();
            }
        }
        //Dette er slet knappen
        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Users where ID = " +SearchUser.Text+ " ", con);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                Cleardata();
                LoadGrid();
                con.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" +ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        //Dette er Return knappen, man bruger det til hvis du vil return til main menu 
        private void returnbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Adminstrator ad = new Adminstrator();
            ad.Show();
        }
    }
}
