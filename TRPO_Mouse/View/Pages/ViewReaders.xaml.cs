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
    /// Логика взаимодействия для ViewReaders.xaml
    /// </summary>
    public partial class ViewReaders : Page
    {
        public ViewReaders()
        {
            InitializeComponent();
        }

        private void gridReadersList_Loaded(object sender, RoutedEventArgs e)
        {
            gridReadersList.ItemsSource = Util.db.library_users_data.ToList();
        }
        private void gridReadersList_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Util.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                gridReadersList.ItemsSource = Util.db.library_users_data.ToList();
            }
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditReader((sender as Button).DataContext as library_users_data));
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditReader());
        }
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var RemovingUser = gridReadersList.SelectedItems.Cast<library_users_data>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {RemovingUser.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Util.db.library_users_data.RemoveRange(RemovingUser);
                    Util.db.SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    gridReadersList.ItemsSource = Util.db.library_users_data.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }


        }
    }
}
