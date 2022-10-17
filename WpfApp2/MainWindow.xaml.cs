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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public class Author
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Surname { get; set; }
            public override string ToString()
            {
                return $"{Id}. { Name} {Surname} ";
            }
        }
      
        static string connectionString = "Data Source=DESKTOP-7HUNHTF;" +
            "Integrated Security=True;ApplicationIntent=ReadWrite;" +
            " Initial Catalog=Library;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        SqlDataReader reader = null;
        List<Author> authors = new List<Author>();
        public MainWindow()
        {
            InitializeComponent();

            ReadSql();
        }

        public void ReadSql()
        {
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Authors", sqlConnection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    authors.Add(new Author
                    {
                        Id = (int)(reader[0]),
                        Name = reader[1].ToString(),
                        Surname = reader[2].ToString()
                    });
                }
                foreach (Author author in authors)
                {
                    ListView1.Items.Add(author.ToString());
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                if (sqlConnection != null) { sqlConnection.Close(); }
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            int Id = 15;
            string Firstname = FirstnameTxtbx.Text.ToString();
            string Lastname = SurenameTxtbx.Text.ToString();
            string insertString = @$"INSERT INTO Authors (Id,FirstName,LastName)
                                    VALUES({++Id},'{Firstname}', '{Lastname}')";
            SqlCommand cmd = new();
            
            try
            {                
                cmd.Connection = sqlConnection;
                cmd.CommandText = insertString;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand(@"SELECT * FROM Authors", sqlConnection);
                reader = cmd1.ExecuteReader();               
                while (reader.Read())
                {
                    authors.Add(new Author
                    {
                        Id = (int)(reader[0]),
                        Name = reader[1].ToString(),
                        Surname = reader[2].ToString()
                    });
                }
                foreach (Author author in authors)
                {
                    ListView1.Items.Add(author.ToString());
                }
            }
            finally
            {
                if (sqlConnection != null){ sqlConnection.Close(); }
                if (reader != null) { reader.Close(); }
            }
        }
    }
}
