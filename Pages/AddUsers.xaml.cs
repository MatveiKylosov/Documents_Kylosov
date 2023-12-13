using Documents_Kylosov.Classes;
using Documents_Kylosov.Model;
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

namespace Documents_Kylosov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Page
    {
        public Model.Document Document;
        public AddUsers(Model.Document Document = null)
        {
            InitializeComponent();

            if (Document != null)
            {
                this.Document = Document;
                tb_user.Text = this.Document.user;
                bthAdd.Content = "Изменить";
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }

        private void AddDocument(object sender, RoutedEventArgs e)
        {
            DocumentContext newDocument = new DocumentContext();

            newDocument.id = Document == null ? newDocument.id : Document.id;
            newDocument.user = tb_user.Text;

            newDocument.Save(!(Document == null), true);
            MessageBox.Show((Document == null ? "Ответственный добавлен" : "Ответственный изменён"));

            MainWindow.init.AllUsers = new DocumentContext().AllUsers();
            MainWindow.init.OpenPages(MainWindow.pages.users);
        }
    }
}
