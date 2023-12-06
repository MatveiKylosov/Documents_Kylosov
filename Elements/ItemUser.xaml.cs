using Documents_Kylosov.Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace Documents_Kylosov.Elements
{
    /// <summary>
    /// Логика взаимодействия для ItemUser.xaml
    /// </summary>
    public partial class ItemUser : UserControl
    {
        string user;
        public ItemUser(string user)
        {
            InitializeComponent();

            IUser.Content = $"{user}";
        }

        private void EditDocument(object sender, RoutedEventArgs e)
        {
            MainWindow.init.frame.Navigate(new Pages.AddUser(user));
        }

        private void DeleteDocument(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            foreach (var item in MainWindow.init.AllUser)
                if(user != item)
                    list.Add(item);

            OleDbConnection connection = Classes.Common.DBConnection.Connection();
            Classes.Common.DBConnection.Query($"DELETE FROM [Ответственные] WHERE [ФИО] = {user}", connection);
            Classes.Common.DBConnection.CloseConnection(connection);

            user = "";

            MainWindow.init.frame.Navigate(MainWindow.pages.main);
        }
    }
}
