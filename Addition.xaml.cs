using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Media.Imaging;

namespace Pavilions_program
{
    public partial class Addition : Window
    {
        SqlConnection connection;
        Window parent;
        string id_selected_shop_center;
        string id_employee;
        public Addition(SqlConnection connection, Window parent, string id_selected_shop_center, string id_employee)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            this.id_selected_shop_center = id_selected_shop_center;
            this.id_employee = id_employee;
            parent.Visibility = Visibility.Hidden;

            string sqlExpression = " SELECT shop_center_name, status, count_pavilions, city, price, floor, var_coefficient,  image FROM dbo.SHOPING_CENTERS WHERE(shop_center_id = " + id_selected_shop_center+ ") AND(status <> N'Удален')";

            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    name_center.Content = "Название ТЦ: " + reader.GetValue(0).ToString();
                    status.Content = "Статус: " + reader.GetValue(1).ToString();
                    count_pavilions.Content = "Количество павильонов: " + reader.GetValue(2).ToString() + " шт.";
                    city.Content = "Город: " + reader.GetValue(3).ToString();
                    price.Content = "Цена торгового центра: " + reader.GetValue(4).ToString() + " рублей";
                    floor.Content = "Этажность: " + reader.GetValue(5).ToString();
                    var_coef.Content = "Коэффициент добавочной стоимости: " + reader.GetValue(6).ToString();
                    byte[] img_byte = reader[7] as byte[];

                    if (img_byte != null)
                    {

                        MemoryStream mem_stream = new MemoryStream(img_byte);
                        try
                        {
                            img_field.Source = BitmapFrame.Create(mem_stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        }
                        catch (NotSupportedException)
                        {
                            img_field.Source = null;
                        }
                    }
                    reader.Close();

                }

                else
                {

                    reader.Close();


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

        private void ShowPavilions(object sender, RoutedEventArgs e)
        {
           new ShowPavilions(connection, this, id_selected_shop_center, id_employee);
        }
    }
}
