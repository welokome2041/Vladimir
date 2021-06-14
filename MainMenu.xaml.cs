using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{

    public partial class MainMenu : Window
    {
        SqlConnection connection;
        Window parent;
        string id_employee;
        string name_role;
        public MainMenu(SqlConnection connection, string id_employee, string name_role, Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.connection = connection;
            this.id_employee = id_employee;
            this.name_role = name_role;
            this.Show();
            parent.Visibility = Visibility.Hidden;
            

            if (name_role == "Администратор")
            {
                rentors.Visibility = Visibility.Visible;
            }
            else if (name_role == "Менеджер А")
            {
            

            }
            else if (name_role == "Менеджер С")
            {
                shoping_center.Visibility = Visibility.Visible;
            }
            else 
            {
                MessageBox.Show("Произошла неизвестная ошибка! Перезагрузите систему.");
                parent.Visibility = Visibility.Visible;
                this.Close();

            }
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void shoping_center_Click(object sender, RoutedEventArgs e)
        {
            new ManagerC(connection, this, id_employee);
        }

        private void rentors_Click(object sender, RoutedEventArgs e)
        {
            new RentorsAdmin(connection, this);
        }
    }
}


