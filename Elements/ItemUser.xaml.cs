using System;
using System.Collections.Generic;
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

namespace Documents_Kylosov.Elements
{
    /// <summary>
    /// Логика взаимодействия для ItemUser.xaml
    /// </summary>
    public partial class ItemUser : UserControl
    {
        Classes.DocumentContext Document;

        public ItemUser(Classes.DocumentContext Document)
        {
            InitializeComponent();
            IUser.Content = Document.user;
            this.Document = Document;
        }

        private void EditDocument(object sender, RoutedEventArgs e)
        {
            MainWindow.init.frame.Navigate(new Pages.AddUsers(Document));
        }

        private void DeleteDocument(object sender, RoutedEventArgs e)
        {
            Document.Delete(true);
            MainWindow.init.AllUsers = new Classes.DocumentContext().AllUsers();
            MainWindow.init.OpenPages(MainWindow.pages.users);
        }
    }
}
