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
    public partial class DatabaseForSuperuser : Window
    {
        public DatabaseForSuperuser()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection con = new SqlConnection(@"Data Source=TEC-F-PC13\HAITTHAMSQL;Initial Catalog=GeotemaData;Integrated Security=True");

        public void Cleardata()
        {
            Landinfo.Clear();
            VerdensDel1info.Clear();
            VerdensDel2info.Clear();
            Ranginfo.Clear();
            FødselsRateinfo.Clear();
        }

        public void LoadGrid()
        {
            //den selecter og loade data fra databasen til SuperUser

            SqlCommand cmd = new SqlCommand("select * from Users", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            Datagridcountries.ItemsSource = dt.DefaultView;
        }

        public bool isValid()
        {
            return true;
        }

        private void AddUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    //det er så super user tilføj data til databasen
                    SqlCommand cmd = new SqlCommand("INSERT INTO Land VALUES (@Land, @VerdensDel1,@VerdensDel2,@Rang,@FødselsRate)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Land", Landinfo.Text);
                    cmd.Parameters.AddWithValue("@VerdensDel1", VerdensDel1info.Text);
                    cmd.Parameters.AddWithValue("@VerdensDel2", VerdensDel2info.Text);
                    cmd.Parameters.AddWithValue("@Rang", Ranginfo.Text);
                    cmd.Parameters.AddWithValue("@FødselsRate", FødselsRateinfo.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Succesfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void ClearDatabtn_Click(object sender, RoutedEventArgs e)
        {
            //det er for clear som der laver clear på alle filterne
            Cleardata();
        }

        private void returnbtn_Click(object sender, RoutedEventArgs e)
        {  //return til main page for super user

            this.Hide();
            SuperUser SU = new SuperUser();
            SU.Show();
        }

        private void UpdateUserbtn_Click(object sender, RoutedEventArgs e)
        {
            //Her fortæller vi programmet hvor den skal update info
            con.Open();
            SqlCommand cmd = new SqlCommand("update Land set Land = '" + Landinfo.Text + "', VerdensDel1 = '" + VerdensDel1info.Text + "', VerdensDel2 = '" + VerdensDel2info.Text + "', Rang = '" + Ranginfo.Text + "', FødselsRate = '" + FødselsRateinfo.Text + "' WHERE ID = '" + SearchData.Text + "'", con);

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

        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Land where ID = " + SearchData.Text + " ", con);

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
                MessageBox.Show("Not Deleted" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }


}
