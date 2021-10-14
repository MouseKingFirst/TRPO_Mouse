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
            NavigationService?.Navigate(new AddEditAuthors((sender as Button).DataContext as library_authors));
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditAuthors());
        }
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var RemovingAuthors = gridAuthorsList.SelectedItems.Cast<library_authors>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {RemovingAuthors.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Util.db.library_authors.RemoveRange(RemovingAuthors);
                    Util.db.SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    gridAuthorsList.ItemsSource = Util.db.library_authors.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }


        }

    }
}
