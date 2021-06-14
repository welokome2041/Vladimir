using System;
using System.Data.SqlClient;
using System.Windows;


namespace Pavilions_program
{

    public partial class MainWindow : Window
    {
        private SqlConnection connection;
        private int countErrors = 0;
        public MainWindow()
        {

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = @"DESKTOP-ARHG322\SQLEXPRESS",
                    InitialCatalog = "PAVILIONS",
                    IntegratedSecurity = true
                };
                connection = new SqlConnection(builder.ConnectionString);
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
            connection.Open();
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if (countErrors != 2)
            {
                if (login_text.Text.Length > 0 && password_text.Password.Length > 0)
                {
                    try
                    {
                        SqlParameter data_user_par = new SqlParameter("@login_user", login_text.Text);
                        SqlParameter pass_word_par = new SqlParameter("@password_user", password_text.Password);

                        string sqlExpression = "SELECT id_employee, role FROM dbo.EMPLOYEES WHERE(login = @login_user AND password COLLATE Cyrillic_General_CS_AS = @password_user AND role <> 'Удален')";

                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.Add(data_user_par);
                        command.Parameters.Add(pass_word_par);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string id_employee = reader.GetValue(0).ToString();
                            string name_role = reader.GetValue(1).ToString();

                            password_text.Password = "";
                            er_mes.Content = "";

                            new MainMenu(connection, id_employee, name_role, this);
                            reader.Close();
                        }
                        else
                        {
                            reader.Close();
                            countErrors++;
                            er_mes.Content = "Неверный логин или пароль!";
                            er_mes.Visibility = Visibility.Visible;
                        }
                    }
                    catch (SqlException er)
                    {
                        MessageBox.Show(er.Number + "." + er.Message);

                    }
                }
                else
                {
                    countErrors++;
                    er_mes.Content = "Поля не заполнены!";
                    er_mes.Visibility = Visibility.Visible;

                }
            }
            else
            {
                er_mes.Content = "Введите код с картинки!";
                er_mes.Visibility = Visibility.Visible;
                string captcha = NewCaptcha();
                captcha_field.Content = captcha;
                captcha_field.Visibility = Visibility.Visible;
                captcha_input.Visibility = Visibility.Visible;
                CaptchaEnter.Visibility = Visibility.Visible;
            }
        }
        private string NewCaptcha()
        {
            Random random = new Random();
            string str = "";
            for (int i = 0; i < 5; i++) str += (char)random.Next((int)'A', (int)'Z');
            return str;
        }

        private void CaptchaEnter_Click(object sender, RoutedEventArgs e)
        {
            string second_parameter = (captcha_field.Content).ToString();
            string first_parameter = captcha_input.Text;
            if (first_parameter == second_parameter)
            {
                countErrors = 1;
                captcha_input.Text = "";
                er_mes.Content = "";
                captcha_field.Visibility = Visibility.Hidden;
                captcha_input.Visibility = Visibility.Hidden;
                CaptchaEnter.Visibility = Visibility.Hidden;
            }
            else {
                string captcha = NewCaptcha();
                captcha_input.Text = "";
                captcha_field.Content = captcha;
            }
        }
    }
}
