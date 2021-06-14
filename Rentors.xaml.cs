using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{
    public class renters_class
    {
        public string id_renter { get; set; }
        public string name_rentor { get; set; }
        public string phone_renter { get; set; }
        public string city_rentor { get; set; }

    }

    public partial class Rentors : Window
    {
        SqlConnection connection;
        Window parent;
        string id_employee;
        string id_shop_center;
        string id_pavilion;
        public Rentors(SqlConnection connection, Window parent, string id_shop_center, string id_pavilion, string id_employee)
        {
            InitializeComponent();
            this.Show();
            this.connection = connection;
            this.parent = parent;
            this.id_employee = id_employee;
            this.id_shop_center = id_shop_center;
            this.id_pavilion = id_pavilion;
            parent.Visibility = Visibility.Hidden;

            string sqlExpression = "SELECT * FROM RENTORS";
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void ToRentAct(object sender, RoutedEventArgs e)
        {
            renters_class row = (renters_class)grid.SelectedItem;
           /* if (date_end_box.SelectedDate > date_end_box.SelectedDate)
            {*/
                if (row != null)
                {
                    string id_rent = (DateTime.Now).ToString("yyyyddMMss");
                    string id_selected_renter = row.id_renter;
                    string date_first = date_start_box.Text;
                    string date_second = date_end_box.Text;

                    try
                    {
                        string sqlexpression = "INSERT_NEW_RENT @id_rent = " + id_rent + ", @id_renter = " + id_selected_renter + ", @shop_center = " + id_shop_center + ", @id_employee = " + id_employee + ", @id_pavilion = '" + id_pavilion + "', @date_start = '" + date_first + "', @date_end = '" + date_second + "'";
                        SqlCommand command = new SqlCommand(sqlexpression, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Заявка оформлмена! Спасибо!");
                    }
                    catch (SqlException er)
                    {
                        MessageBox.Show(er.Message);
                    }

                }
            /*}
            else
            {
                MessageBox.Show("Выберите корректную дату!");

            }*/
        }
    }

}

