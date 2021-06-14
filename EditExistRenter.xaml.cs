using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{

    public partial class EditExistRenter : Window
    {
        SqlConnection connection;
        Window parent;
        string id_renter;

        public EditExistRenter(SqlConnection connection, Window parent, string id_renter)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            parent.Visibility = Visibility.Hidden;
            this.id_renter = id_renter;

            try
            {
                string sqlExpression = "SELECT * FROM RENTORS WHERE renter_id = @id_value";
                SqlParameter par_id = new SqlParameter("@id_value", id_renter);
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(par_id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    name_text.Text = reader.GetValue(1).ToString();
                    phone_text.Text = reader.GetValue(2).ToString();
                    city_text.Text = reader.GetValue(3).ToString();
                    street_text.Text = reader.GetValue(4).ToString();

                    reader.Close();

                }

                else
                {
                    reader.Close();

                }
            }

            catch (SqlException er)
            {
                MessageBox.Show("Произошла ошибка: " + er.Number + "." + er.Message);
                this.Close();
            }


        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void accept_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlexpression = "UPDATE RENTORS SET Name = @Name_value, Phone = @Phone_value, City = @City_value, Street = @Street_value WHERE renter_id = @renter";
                SqlCommand command = new SqlCommand(sqlexpression, connection);
                SqlParameter par_renter = new SqlParameter("@renter", id_renter);
                command.Parameters.Add(par_renter);
                SqlParameter par_name = new SqlParameter("@Name_value", name_text.Text);
                command.Parameters.Add(par_name);
                SqlParameter par_phone = new SqlParameter("@Phone_value", phone_text.Text);
                command.Parameters.Add(par_phone);
                SqlParameter par_city = new SqlParameter("@City_value", city_text.Text);
                command.Parameters.Add(par_city);
                SqlParameter par_street = new SqlParameter("@Street_value", street_text.Text);
                command.Parameters.Add(par_street);

                command.ExecuteNonQuery();
                ((RentorsAdmin)parent).function_show();
                MessageBox.Show("Запись была изменена! ");
               
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
