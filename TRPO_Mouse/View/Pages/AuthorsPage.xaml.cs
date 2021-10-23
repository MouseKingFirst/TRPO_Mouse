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
        }
    }
}
