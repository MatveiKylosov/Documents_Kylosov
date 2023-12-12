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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            CreatedUI();
        }

        public void CreatedUI()
        {
            parent.Children.Clear();
            foreach (Classes.DocumentContext document in MainWindow.init.AllDocuments)
                parent.Children.Add(new Elements.Item(document));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.init.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.add);
        }
    }
}
