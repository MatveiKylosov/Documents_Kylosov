using Documents_Kylosov.Classes;
using Documents_Kylosov.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Documents_Kylosov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        string user;
        public AddUser(string str = null)
        {
            InitializeComponent();

           if(str != null)
           {
                user = str;
                tb_user.Text = user;
                bthAdd.Content = "Изменить";
           }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }

        private void AddDocument(object sender, RoutedEventArgs e)
        {

            if (tb_user.Text.Length == 0)
            {
                MessageBox.Show("Укажите ответственного");
                return;
            }

            if (user == null)
            {

                MessageBox.Show($"set {tb_user.Text}");
                OleDbConnection connection = Classes.Common.DBConnection.Connection();
                Classes.Common.DBConnection.Query("INSERT INTO [Ответственные]" +
                                                 $"[ФИО] VALUES (" +
                                                 $"'{tb_user.Text}')"
                                        , connection);

                Classes.Common.DBConnection.CloseConnection(connection);
                MessageBox.Show("ответственный добавлен");
            }
            else
            {
                MessageBox.Show($"up {this.user}");
                DocumentContext newDocument = new DocumentContext();
                OleDbConnection connection = Classes.Common.DBConnection.Connection();
                Classes.Common.DBConnection.Query("UPDATE [Ответственные] " +
                        "SET " +
                        $"[ФИО] = '{tb_user.Text}' " +
                        $"WHERE [ФИО] = {this.user}", connection);
                Classes.Common.DBConnection.CloseConnection(connection);
                MessageBox.Show("ответственный изменён");
            }
            MainWindow.init.AllDocuments = new DocumentContext().AllDocuments();
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }
    }
}
