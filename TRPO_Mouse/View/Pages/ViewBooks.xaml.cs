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
    /// Логика взаимодействия для ViewBooks.xaml
    /// </summary>
    public partial class ViewBooks : Page
    {
        public ViewBooks()
        {
            InitializeComponent();
        }

        private void gridBooksList_Loaded(object sender, RoutedEventArgs e)
        {
            gridBooksList.ItemsSource = Util.db.library_books.ToList();
        }
        private void gridBooksList_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Util.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                gridBooksList.ItemsSource = Util.db.library_books.ToList();
            }
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditBooks());
        }
    }
}
