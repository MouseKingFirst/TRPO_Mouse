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

namespace TRPO_Mouse.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorsPage.xaml
    /// </summary>
    public partial class AuthorsPage : Page
    {
        public AuthorsPage()
        {
            InitializeComponent();

            var currentAuthors = Util.db.library_authors.ToList();

            //Т.к в базе хранятся только названия фотографий, а не полный путь
            //то необходимо указать путь до ресурса
            foreach (var author in currentAuthors)
            {
                if (author.photo == null)
                    continue;
                author.photo = "pack://application:,,,/Resources/authors/" + author.photo;
            }

            ListAuthors.ItemsSource = currentAuthors;
            SortType.SelectedIndex = 0;
            WithPhoto.IsChecked = false;
            ListAuthors.ItemsSource = currentAuthors.OrderBy(x => x.author_FullName).ToList();
        }

        private void UpdateAuthors()
        {
            
            var currentAuthors = Util.db.library_authors.ToList();
            //осуществляем поиск по Ф.И.О. без учета регистра букв

            currentAuthors = currentAuthors.Where(x => x.author_FullName.ToLower().Contains(SearchText.Text.ToLower())).ToList();

            if (WithPhoto.IsChecked.Value)
            {
                currentAuthors = currentAuthors.Where(x => x.photo != null).ToList();
            }

            //осуществляем сортировку в зависимости от выбора пользователя
            if (SortType.SelectedIndex == 0)
                ListAuthors.ItemsSource = currentAuthors.OrderBy(x => x.author_FullName).ToList();
            else ListAuthors.ItemsSource = currentAuthors.OrderByDescending(x => x.author_FullName).ToList();
        }


        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAuthors();
        }



        private void SortType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAuthors();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchText.Text = "";
            SortType.SelectedIndex = 0;
            WithPhoto.IsChecked = false;
            UpdateAuthors();
        }

        private void WithPhoto_Checked(object sender, RoutedEventArgs e)
        {
            UpdateAuthors();
        }
    }
}
