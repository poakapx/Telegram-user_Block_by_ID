using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace Ban_Bot
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (var item in GetUsers())
            {
                All_Users.Items.Add(item);
            }
        }

        private static List<string> GetUsers()
        {
            List<string> users = new List<string>();
            SqlConnection connection = new SqlConnection("Server=tcp:server-of-pavlo.database.windows.net,1433;Initial Catalog=DZ_01;Persist Security Info=False;User ID=PavelIks;Password=/45xz78/;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            connection.Open();

            using (SqlCommand command = new SqlCommand($"SELECT*FROM DZ_03", connection))
            {
                try
                {
                    //command.Parameters.Add(new SQLiteParameter("@id_q", id_question));
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add((reader.GetInt32(1).ToString()));
                    }
                    //connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            connection.Close();
            return users;


        }

        private void Ban_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(($"Client with chat id {All_Users.SelectedItem.ToString()} will banned. Are you shure?"), "Are you shure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DeleteUser(All_Users.SelectedItem.ToString());
                All_Users.Items.Remove(All_Users.SelectedItem);
            }
        }


        private static void DeleteUser(string chid)
        {
            SqlConnection connection = new SqlConnection("Server=tcp:server-of-pavlo.database.windows.net,1433;Initial Catalog=DZ_01;Persist Security Info=False;User ID=PavelIks;Password=/45xz78/;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            connection.Open();

            using (SqlCommand command = new SqlCommand($"DELETE FROM DZ_03 WHERE [Chat_ID]={Convert.ToInt32(chid)}", connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            connection.Close();
        }
    }
}