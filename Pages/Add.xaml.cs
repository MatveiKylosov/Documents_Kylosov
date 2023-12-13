using Documents_Kylosov.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public string s_src = "";
        public Model.Document Document;
        public Add(Model.Document Document = null)
        {
            InitializeComponent();

            if (Document != null)
            {
                this.Document = Document;

                if (File.Exists(Document.src))
                {
                    s_src = Document.src;
                    src.Source = new BitmapImage(new Uri(s_src));
                }

                tb_name.Text = this.Document.name;
                tb_id.Text = $"{this.Document.id_document}";
                tb_date.Text = this.Document.date.ToString("dd.MM.yyyy");
                tb_user.Text = this.Document.user;
                
                if(MainWindow.init.AllUsers.Find(x => x.user == this.Document.user) == null)
                {
                    cb_user.Items.Add(this.Document.user);
                    cb_user.SelectedIndex = 0;
                }

                tb_status.SelectedIndex = this.Document.status;
                tb_vector.Text = this.Document.vector;
                bthAdd.Content = "Изменить";
            }

            foreach (var x in MainWindow.init.AllUsers)
                cb_user.Items.Add(x.user);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }

        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "PNG (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                src.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                s_src = openFileDialog.FileName;
            }
        }

        private void AddDocument(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> fields = new Dictionary<int, string>
                {
                    { s_src.Length, "Выберите изображение" },
                    { tb_name.Text.Length, "Укажите наименование" },
                    { tb_user.Text.Length, "Укажите ответственного" },
                    { tb_id.Text.Length, "Укажите код документа" },
                    { tb_date.Text.Length, "Укажите дату поступления" },
                    { tb_status.Text.Length, "Укажите статус" },
                    { tb_vector.Text.Length, "Укажите направление" }
                };

            foreach (var field in fields)
            {
                if (field.Key == 0)
                {
                    MessageBox.Show(field.Value);
                    return;
                }
            }

            DocumentContext newDocument = new DocumentContext();
            DateTime newDate = new DateTime();
            DateTime.TryParse(tb_date.Text, out newDate);

            newDocument.id = Document == null ? newDocument.id : Document.id;
            newDocument.src = s_src;
            newDocument.name = tb_name.Text;
            newDocument.user = tb_user.Text;
            newDocument.id_document = (tb_id.Text);
            newDocument.date = newDate;
            newDocument.status = tb_status.SelectedIndex;
            newDocument.vector = tb_vector.Text;

            newDocument.Save(!(Document == null));
            MessageBox.Show((Document == null ? "Документ добавлен" : "Документ изменён"));

            MainWindow.init.AllDocuments = new DocumentContext().AllDocuments();
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }

        private void cb_user_SelectionChanged(object sender, SelectionChangedEventArgs e) => tb_user.Text = cb_user.SelectedItem.ToString();
    }
}
