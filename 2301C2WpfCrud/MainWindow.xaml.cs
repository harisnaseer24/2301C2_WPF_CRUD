using System.Windows;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;

namespace _2301C2WpfCrud
{

    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=2301C2WpfCrud;User ID=sa;Password=aptech;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        public MainWindow()
        {
            InitializeComponent();
            LoadData();

        }
        private void LoadData()
        {
            SqlCommand getData = new SqlCommand("Select * from students", con);

            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader dataReader = getData.ExecuteReader();
            dt.Load(dataReader);
            studentGrid.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private bool IsValid()
        {
            if(uname.Text == string.Empty)
            {
                MessageBox.Show("Name cannnot be empty","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } 
            if(email.Text == string.Empty)
            {
                MessageBox.Show("Email cannnot be empty","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } 
            if(age.Text == string.Empty)
            {
                MessageBox.Show("Age cannnot be empty","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(cellno.Text == string.Empty)
            {
                MessageBox.Show("Cell No cannnot be empty","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } 
            if(city.Text == string.Empty)
            {
                MessageBox.Show("City cannnot be empty","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                SqlCommand addStud = new SqlCommand("Insert into students values(@fname,@email,@age,@cell,@city)", con);
                con.Open();
                addStud.CommandType = CommandType.Text;

                addStud.Parameters.AddWithValue("@fname", uname.Text);
                addStud.Parameters.AddWithValue("@email", email.Text);
                addStud.Parameters.AddWithValue("@age", age.Text);
                addStud.Parameters.AddWithValue("@cell", cellno.Text);
                addStud.Parameters.AddWithValue("@city", city.Text);

                addStud.ExecuteNonQuery();
                con.Close();
                LoadData();
                ClearData();
            }
        }
        private void ClearData()
        {
            uname.Clear();
            email.Clear();
            age.Clear();
            cellno.Clear();
            city.Clear();
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

      
    }
}
