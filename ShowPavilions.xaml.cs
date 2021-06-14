using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{
    public class pavilions_class
    {

        public string id_shop_center { get; set; }
        public string id_pavilion { get; set; }
        public string name_center { get; set; }
        public string floor { get; set; }
        public string status { get; set; }
        public string area { get; set; }
        public string price { get; set; }
        public string var_coefficient { get; set; }

    }
    public partial class ShowPavilions : Window
    {
        SqlConnection connection;
        Window parent;
        string selected_id_shop_center;
        string id_employee;

        public ShowPavilions(SqlConnection connection, Window parent, string selected_id_shop_center, string id_employee)
        {
            InitializeComponent();
            this.Show();
            parent.Visibility = Visibility.Hidden;
            this.connection = connection;
            this.parent = parent;
            this.selected_id_shop_center = selected_id_shop_center;
            this.id_employee = id_employee;
            string sqlExpression = "SELECT * FROM ShowPavilions WHERE shop_center_id = " + selected_id_shop_center +"";
            List<pavilions_class> pavilions_list = new List<pavilions_class>();
            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        pavilions_class st_rec = new pavilions_class();

                        st_rec.id_shop_center = reader.GetValue(0).ToString();
                        st_rec.id_pavilion = reader.GetValue(1).ToString();
                        st_rec.name_center = reader.GetValue(2).ToString();
                        st_rec.floor = reader.GetValue(3).ToString();
                        st_rec.status = reader.GetValue(4).ToString();
                        st_rec.area = reader.GetValue(5).ToString();
                        st_rec.price = reader.GetValue(6).ToString();
                        st_rec.var_coefficient = reader.GetValue(7).ToString();


                        pavilions_list.Add(st_rec);
                    }

                    reader.Close();
                    grid.ItemsSource = pavilions_list;

                    grid.Columns[0].Visibility = Visibility.Hidden;
                    grid.Columns[1].Header = "No-Павильона";
                    grid.Columns[2].Header = "Торговый центр";
                    grid.Columns[3].Header = "Этаж";
                    grid.Columns[4].Header = "Статус";
                    grid.Columns[5].Header = "Площадь кв.м";
                    grid.Columns[6].Header = "Цена (кв.м/руб.)";
                    grid.Columns[7].Header = "Коэффициент";

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

        private void ToRent(object sender, RoutedEventArgs e)
        {
            pavilions_class row = (pavilions_class)grid.SelectedItem;
            if (row != null)
            {
                string id_selected_pavilion = row.id_pavilion;
                this.Visibility = Visibility.Hidden;
                new Rentors(connection, this, selected_id_shop_center, id_selected_pavilion, id_employee);

            }
            
        }
    }
}
