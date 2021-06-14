using System;
using System.Windows;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Media.Imaging;

namespace Pavilions_program
{
    
    public partial class Editor : Window
    {
        SqlConnection connection;
        Window parent;
        string selected_row;
        public Editor(SqlConnection connection, Window parent, string selected_row)
        {
            InitializeComponent();
            this.parent = parent;
            this.selected_row = selected_row;
            this.connection = connection;
            this.Show();
            parent.Visibility = Visibility.Hidden;


            string sqlExpression = " SELECT shop_center_name, status, count_pavilions, city, price, floor, var_coefficient,  image FROM dbo.SHOPING_CENTERS WHERE(shop_center_id = " + selected_row + ") AND(status <> N'Удален')";

            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    name_center_lab.Text = reader.GetValue(0).ToString();
                    status_lab.Text = reader.GetValue(1).ToString();
                    count_pavilions_lab.Text =  reader.GetValue(2).ToString();
                    city_lab.Text = reader.GetValue(3).ToString();
                    price_lab.Text = reader.GetValue(4).ToString();
                    floor_lab.Text = reader.GetValue(5).ToString();
                    var_coef_lab.Text = reader.GetValue(6).ToString();
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

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void ToAccept_Click(object sender, RoutedEventArgs e)
        {

            string shop_name = name_center_lab.Text;
            string status = status_lab.Text;
            string count_pavilions = count_pavilions_lab.Text;
            string city = city_lab.Text;
            string price = price_lab.Text;
            string floor = floor_lab.Text;
            //string var_cof = var_coef_lab.Text;

            try
            {
                
                string sqlExpression = " UPDATE dbo.SHOPING_CENTERS SET shop_center_name = @shop_name, status = @status, count_pavilions = @count_pav, city = @city, price = @price, floor = @floor "
                + " WHERE(shop_center_id = '" + selected_row + "') ";

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                
                SqlParameter shop_param = new SqlParameter("@shop_name", shop_name);
                command.Parameters.Add(shop_param);

                SqlParameter status_param = new SqlParameter("@status", status);
                command.Parameters.Add(status_param);

                SqlParameter count_pav_param = new SqlParameter("@count_pav", count_pavilions);
                command.Parameters.Add(count_pav_param);

                SqlParameter city_param = new SqlParameter("@city", city);
                command.Parameters.Add(city_param);

                SqlParameter price_param = new SqlParameter("@price", price);
                command.Parameters.Add(price_param);

                SqlParameter floor_param = new SqlParameter("@floor", floor);
                command.Parameters.Add(floor_param);

                //SqlParameter var_param = new SqlParameter("@var_cof", var_cof);
                //command.Parameters.Add(var_param);

                command.ExecuteNonQuery();
                ((ManagerC)parent).update_sell_car();
                MessageBox.Show("Данные обновлены!");
            }

            catch (SqlException en)
            {
                MessageBox.Show((en.Number).ToString() + " " + en.Message);
            }


        }
    }
}
