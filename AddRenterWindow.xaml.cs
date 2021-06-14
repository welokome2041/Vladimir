using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace Pavilions_program
{
    public partial class AddRenterWindow : Window
    {
        SqlConnection connection;
        Window parent;

        public AddRenterWindow(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            parent.Visibility = Visibility.Hidden;


        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void accept_click(object sender, RoutedEventArgs e)
        {
             
        }
    }
}
