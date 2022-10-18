using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {

        static string connectionString = "Data Source=DESKTOP-7HUNHTF;" +
            "Integrated Security=True;ApplicationIntent=ReadWrite;" +
            " Initial Catalog=Library;";
        List<Author> authors = new List<Author>();

        public MainWindow()
        {
            InitializeComponent();

            ReadSql();
        }

        public void ReadSql()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Authors", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    authors.Add(new Author
                    {
                        Id = (int)(reader[0]),
                        Name = reader[1].ToString(),
                        Surname = reader[2].ToString()
                    });
                }
            }
            Console.WriteLine();
            Console.WriteLine();

            foreach (Author author in authors)
            {
                ListView1.Items.Add(author);

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int id;
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                SqlCommand getLastId = new SqlCommand("SELECT TOP 1 Id FROM Authors ORDER BY Id DESC", conn);
                id = Convert.ToInt32(getLastId.ExecuteScalar());
            }
            string firstName = Firstname_txtbx.Text;
            string lastName = Lastname_Txtbx.Text;
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Authors(Id, FirstName, LastName) " +
               "Values('" + ++id + "', '" + firstName + "', '" + lastName + "')", conn);

                cmd.ExecuteNonQuery();
                authors.Add(new Author
                {
                    Id = id,
                    Name = firstName,
                    Surname = lastName
                });
                ListView1.Items.Add(new Author
                {
                    Id = id,
                    Name = firstName,
                    Surname = lastName
                });

            }
        }
    }
}
