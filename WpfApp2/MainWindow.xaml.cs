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
      
        public MainWindow()
        {
            InitializeComponent();
            var connectionString = "Data Source=STHQ0116-08;Initial Catalog=Library; User ID=admin; Password=admin";

            SqlConnection sqlConnection = null;
            List<Author> authors = new List<Author>();
            sqlConnection = new SqlConnection(connectionString);
           

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string insertString = @"INSERT INTO Authors (Id,FirstName,LastName)
                                    VALUES(16,'Kenan', 'Yusubov')";
            SqlCommand cmd = new();
            try
            {
                cmd.Connection = sqlConnection;
                cmd.CommandText = insertString;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
    }
}
