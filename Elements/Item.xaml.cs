using Documents_Kylosov.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Xml.Linq;

namespace Documents_Kylosov.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Classes.DocumentContext Document;
        public Item(Classes.DocumentContext Document)
        {
            InitializeComponent();

            img.Source = new BitmapImage(new Uri(Document.src));
            // Выводим данные
            IName.Content = Document.name;
            IUser.Content = $"Ответственный: {Document.user}";
            ICode.Content = $"Код документа: {Document.id_document}";
            IDate.Content = $"Дата поступления {Document.date.ToString("dd.MM.yyyy")}";
            IStatus.Content = Document.status == 0 ? $"Статус: входящий" : $"Статус: исходящий";
            IDirect.Content = "Направление: " + Document.vector;
            // Сохраняем документ для изменения или удаления
            this.Document = Document;
        }

        private void EditDocument(object sender, RoutedEventArgs e)
        {
            MainWindow.init.frame.Navigate(new Pages.Add(Document));
        }

        private void DeleteDocument(object sender, RoutedEventArgs e)
        {
            Document.Delete();
            MainWindow.init.AllDocuments = new Classes.DocumentContext().AllDocuments();
            MainWindow.init.frame.Navigate(MainWindow.pages.main);
        }
    }
}
