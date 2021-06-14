using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{

    public class shop_class
    {

        public string id_shop_center { get; set; }
        public string name_center { get; set; }
        public string status { get; set; }
        public string count_pavilions { get; set; }
        public string city { get; set; }
        public string price { get; set; }
        public string floor { get; set; }
        public string var_coefficient { get; set; }

    }

    public partial class ManagerC : Window
    {
        SqlConnection connection;
        Window parent;
        string id_employee;
        public ManagerC(SqlConnection connection, Window parent, string id_employee)
        {
            InitializeComponent();
            this.Show();
            parent.Visibility = Visibility.Hidden;
            this.connection = connection;
            this.parent = parent;
            this.id_employee = id_employee;
            update_sell_car();


        }

        public void update_sell_car()
        {
            List<shop_class> shop_list = new List<shop_class>();

            string sqlExpression = " SELECT * FROM ShowShopingCenters ";

            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        shop_class st_rec = new shop_class();

                        st_rec.id_shop_center = reader.GetValue(0).ToString();
                        st_rec.name_center = reader.GetValue(1).ToString();
                        st_rec.status = reader.GetValue(2).ToString();
                        st_rec.count_pavilions = reader.GetValue(3).ToString();
                        st_rec.city = reader.GetValue(4).ToString();
                        st_rec.price = reader.GetValue(5).ToString();
                        st_rec.floor = reader.GetValue(6).ToString();
                        st_rec.var_coefficient = reader.GetValue(7).ToString();

                        shop_list.Add(st_rec);
                    }
                    reader.Close();

                    grid.ItemsSource = shop_list;

                    grid.Columns[0].Visibility = Visibility.Hidden;
                    grid.Columns[1].Header = "Название";
                    grid.Columns[2].Header = "Статус";
                    grid.Columns[3].Header = "Количество п.";
                    grid.Columns[4].Header = "Город";
                    grid.Columns[5].Header = "Цена (руб.)";
                    grid.Columns[6].Header = "Этажность";
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

        private void addition_click(object sender, RoutedEventArgs e)
        {
            shop_class row = (shop_class)grid.SelectedItem;
            if (row != null)
            {
                string id_selected_row = row.id_shop_center;
                this.Visibility = Visibility.Hidden;
                new Addition(connection, this, id_selected_row, id_employee);

            }


        }

        private void ToEdit(object sender, RoutedEventArgs e)
        {
            shop_class row = (shop_class)grid.SelectedItem;
            if (row != null)
            {
                string id_selected_row = row.id_shop_center;
                this.Visibility = Visibility.Hidden;
                new Editor(connection, this, id_selected_row);

            }
        }
    }
}
