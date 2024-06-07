using System.Windows;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;

namespace _2301C2WpfCrud
{

    public partial class MainWindow : Window
    {
        SqlConnection Con = new SqlConnection("Data Source=.;Initial Catalog=2301C2WpfCrud;User ID=sa;Password=aptech;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        public MainWindow()
        {
            InitializeComponent();
            LoadData();

        }
        private void LoadData()
        {
            SqlCommand getData = new SqlCommand("Select * from students", Con);

            DataTable dt = new DataTable();
            Con.Open();

            SqlDataReader dataReader = getData.ExecuteReader();
            dt.Load(dataReader);
            studentGrid.ItemsSource = dt.DefaultView;
            Con.Close();
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
                SqlCommand addStud = new SqlCommand("Insert into students values(@fname,@email,@age,@cell,@city)", Con);
                Con.Open();
                addStud.CommandType = CommandType.Text;

                addStud.Parameters.AddWithValue("@fname", uname.Text);
                addStud.Parameters.AddWithValue("@email", email.Text);
                addStud.Parameters.AddWithValue("@age", age.Text);
                addStud.Parameters.AddWithValue("@cell", cellno.Text);
                addStud.Parameters.AddWithValue("@city", city.Text);

                addStud.ExecuteNonQuery();
                Con.Close();
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
            sid.Clear();
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        private void dltStudent(object sender, RoutedEventArgs e)
        {
            if(sid.Text != string.Empty)
            {
                SqlCommand deleteStd = new SqlCommand("DELETE FROM students WHERE id = @sid", Con);
                Con.Open();
                deleteStd.CommandType = CommandType.Text;
                deleteStd.Parameters.AddWithValue("@sid", sid.Text);
                deleteStd.ExecuteNonQuery();
                Con.Close();
                //if(row > 0)
                MessageBox.Show("Student deleted Successfully","Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearData();
               
            }
            else
            {
                MessageBox.Show("Student id is required to delete a record", "Can't Delete Student", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
