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

namespace Documents_Kylosov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public List<Classes.DocumentContext> AllDocuments = new Classes.DocumentContext().AllDocuments();
        public List<string> AllUser = new List<string>();
        public enum pages
        {
            main, add
        }

        public void OpenPages(pages _pages)
        {
            if (_pages == pages.main)
                frame.Navigate(new Pages.Main());
            else if (_pages == pages.add)
                frame.Navigate(new Pages.Add());

        }
        public MainWindow()
        {
            InitializeComponent();
            foreach (Classes.DocumentContext context in AllDocuments)
                 if (AllUser.Count == 0 || AllUser.Find(x=> context.user != x).Length == 0)
                    AllUser.Add(context.user);
      
            init = this;
            OpenPages(pages.main);
        }
    }
}
