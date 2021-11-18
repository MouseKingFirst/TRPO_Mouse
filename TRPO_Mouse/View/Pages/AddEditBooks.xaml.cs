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

        private void ISBNChanged(object sender, TextChangedEventArgs e)
        {
            txtHintISBN.Visibility = Visibility.Visible;
            ISBNBox.Background = Brushes.Transparent;

            if (ISBNBox.Text.Length > 0)
            {
                txtHintISBN.Visibility = Visibility.Hidden;
                ISBNBox.Background = Brushes.White;
            }
        }

        private void TitleChanged(object sender, TextChangedEventArgs e)
        {
            txtHintTitle.Visibility = Visibility.Visible;
            TitleBox.Background = Brushes.Transparent;

            if (TitleBox.Text.Length > 0)
            {
                txtHintTitle.Visibility = Visibility.Hidden;
                TitleBox.Background = Brushes.White;
            }
        }


        private void GenreChanged(object sender, TextChangedEventArgs e)
        {
            txtHintGenre.Visibility = Visibility.Visible;
            GenreBox.Background = Brushes.Transparent;

            if (GenreBox.Text.Length > 0)
            {
                txtHintGenre.Visibility = Visibility.Hidden;
                GenreBox.Background = Brushes.White;
            }
        }
        private void PublisherChanged(object sender, TextChangedEventArgs e)
        {
            txtHintPublisher.Visibility = Visibility.Visible;
            PublisherBox.Background = Brushes.Transparent;

            if (PublisherBox.Text.Length > 0)
            {
                txtHintPublisher.Visibility = Visibility.Hidden;
                PublisherBox.Background = Brushes.White;
            }
        }

        private void PubPlaceChanged(object sender, TextChangedEventArgs e)
        {
            txtHintPubPlace.Visibility = Visibility.Visible;
            PubPlaceBox.Background = Brushes.Transparent;

            if (PubPlaceBox.Text.Length > 0)
            {
                txtHintPubPlace.Visibility = Visibility.Hidden;
                PubPlaceBox.Background = Brushes.White;
            }
        }

        private void PubYearChanged(object sender, TextChangedEventArgs e)
        {
            txtHintPubYear.Visibility = Visibility.Visible;
            PubYearBox.Background = Brushes.Transparent;

            if (PubYearBox.Text.Length > 0)
            {
                txtHintPubYear.Visibility = Visibility.Hidden;
                PubYearBox.Background = Brushes.White;
            }
        }

        private void PagesChanged(object sender, TextChangedEventArgs e)
        {
            txtHintPages.Visibility = Visibility.Visible;
            PagesBox.Background = Brushes.Transparent;

            if (PagesBox.Text.Length > 0)
            {
                txtHintPages.Visibility = Visibility.Hidden;
                PagesBox.Background = Brushes.White;
            }
        }

        private void PriceChanged(object sender, TextChangedEventArgs e)
        {
            txtHintPrice.Visibility = Visibility.Visible;
            PriceBox.Background = Brushes.Transparent;

            if (PriceBox.Text.Length > 0)
            {
                txtHintPrice.Visibility = Visibility.Hidden;
                PriceBox.Background = Brushes.White;
            }
        }

    }
}
