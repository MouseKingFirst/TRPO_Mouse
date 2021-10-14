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
using TRPO_Mouse.Model;

namespace TRPO_Mouse.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditBooks.xaml
    /// </summary>
    public partial class AddEditBooks : Page
    {
        private library_books cb = new library_books();
        StringBuilder errors = new StringBuilder();
        public AddEditBooks()
        {
            InitializeComponent();
            authorsC.ItemsSource = Util.db.library_authors.ToList();
        }
        public AddEditBooks(library_books sb)
        {
            InitializeComponent();
            authorsC.ItemsSource = Util.db.library_authors.ToList();
            cb = sb;
            DataContext = cb;
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            errors.Clear();
            cb.author = Util.db.library_authors.Where(x => x.author_FullName == authorsC.Text).ToList()[0].author_code;
            if (string.IsNullOrWhiteSpace(cb.ISBN))
            {
               errors.AppendLine("ISBN книги!");
            }
            if (string.IsNullOrWhiteSpace(cb.title))
            {
                errors.AppendLine("Название книги!");
            }
            if (string.IsNullOrWhiteSpace(cb.genre))
            {
                errors.AppendLine("Жанр книги!");
            }
            if (string.IsNullOrWhiteSpace(cb.author.ToString()))
            {
                errors.AppendLine("Автор книги!");
            }
            if (string.IsNullOrWhiteSpace(cb.publisher))
            {
                errors.AppendLine("Название редакции!");
            }
            if (string.IsNullOrWhiteSpace(cb.publication_place))
            {
                errors.AppendLine("Место публикации!");
            }
            if (string.IsNullOrWhiteSpace(cb.pages.ToString()))
            {
                errors.AppendLine("Кол-во страниц!");
            }
            if (string.IsNullOrWhiteSpace((cb.price).ToString()))
            {
                errors.AppendLine("Цена за книгу!");
            }

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (cb.id == 0)
            {

                Util.db.library_books.Add(cb);
            }

            try
            {
                Util.db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService?.Navigate(new ViewBooks());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
