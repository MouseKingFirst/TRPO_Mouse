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
    /// Логика взаимодействия для ViewAuthors.xaml
    /// </summary>
    public partial class ViewAuthors : Page
    {
        public ViewAuthors()
        {
            InitializeComponent();
        }

        private void gridAuthorsList_Loaded(object sender, RoutedEventArgs e)
        {
            gridAuthorsList.ItemsSource = Util.db.library_authors.ToList();
        }
        private void gridAuthorsList_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Util.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                gridAuthorsList.ItemsSource = Util.db.library_authors.ToList();
            }
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditAuthors());
        }

    }
}
