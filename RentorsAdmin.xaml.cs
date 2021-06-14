using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{

    public partial class RentorsAdmin : Window
    {
        public class renters_class
        {
            public string id_renter { get; set; }
            public string name_rentor { get; set; }
            public string phone_renter { get; set; }
            public string city_rentor { get; set; }

        }


        SqlConnection connection;
        Window parent;
        public RentorsAdmin(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            this.Show();
            this.connection = connection;
            this.parent = parent;
            parent.Visibility = Visibility.Hidden;
            function_show();

        }

        public void function_show()
        {
            string sqlExpression = "SELECT * FROM RENTORS WHERE Status <> 'Удален'";
            List<renters_class> renters_list = new List<renters_class>();
            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        renters_class st_rec = new renters_class();

                        st_rec.id_renter = reader.GetValue(0).ToString();
                        st_rec.name_rentor = reader.GetValue(1).ToString();
                        st_rec.phone_renter = reader.GetValue(2).ToString();
                        st_rec.city_rentor = reader.GetValue(3).ToString();


                        renters_list.Add(st_rec);
                    }

                    reader.Close();
                    grid.ItemsSource = renters_list;

                    grid.Columns[0].Visibility = Visibility.Hidden;
                    grid.Columns[1].Header = "Наименование";
                    grid.Columns[2].Header = "Телефон";
                    grid.Columns[3].Header = "Город";


                }

                else
                {
                    reader.Close();
                    grid.ItemsSource = null;
                }
            }

            catch (SqlException e)
            {
                MessageBox.Show("Произошла ошибка: " + e.Number + "." + e.Message);
                this.Close();
            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void search_function_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string sqlExpression = String.Format($"SELECT * FROM RENTORS WHERE (Name LIKE '%{search_function.Text}%' AND status <> 'Удален') ");
            List<renters_class> renters_list = new List<renters_class>();
            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        renters_class st_rec = new renters_class();

                        st_rec.id_renter = reader.GetValue(0).ToString();
                        st_rec.name_rentor = reader.GetValue(1).ToString();
                        st_rec.phone_renter = reader.GetValue(2).ToString();
                        st_rec.city_rentor = reader.GetValue(3).ToString();


                        renters_list.Add(st_rec);
                    }

                    reader.Close();
                    grid.ItemsSource = renters_list;

                    grid.Columns[0].Visibility = Visibility.Hidden;
                    grid.Columns[1].Header = "Наименование";
                    grid.Columns[2].Header = "Телефон";
                    grid.Columns[3].Header = "Город";


                }

                else
                {
                    reader.Close();
                    grid.ItemsSource = null;
                }
            }

            catch (SqlException er)
            {
                MessageBox.Show("Произошла ошибка: " + er.Number + "." + er.Message);
                this.Close();
            }



        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            search_function.Text = "";
            function_show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            renters_class row = (renters_class)grid.SelectedItem;
            if (row != null)
            {
                string tmp_id = row.id_renter;
                try
                {
                    string sqlexpression = "DELETE FROM RENTORS WHERE renter_id = @id_value";
                    SqlCommand command = new SqlCommand(sqlexpression, connection);
                    SqlParameter id_param = new SqlParameter("@id_value", tmp_id);
                    command.Parameters.Add(id_param);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись была удалена! ");
                    function_show();
                }
                catch (SqlException er)
                {
                    MessageBox.Show(er.Message);
                }

            }
        }

        private void ToAddRenter_Click(object sender, RoutedEventArgs e)
        {
            new AddRenterWindow(connection, this);
        }

        private void ToEditRenter_Click(object sender, RoutedEventArgs e)
        {
            renters_class row = (renters_class)grid.SelectedItem;

            if (row != null)
            {
                string tmp_id = row.id_renter;
                new EditExistRenter(connection, this, tmp_id);
            }
        }
    }
}
