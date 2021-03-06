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
    /// Логика взаимодействия для ViewUsers.xaml
    /// </summary>
    public partial class ViewUsers : Page
    {
        public ViewUsers()
        {
            InitializeComponent();
        }

        private void gridUsersList_Loaded(object sender, RoutedEventArgs e)
        {
            gridUsersList.ItemsSource = Util.db.library_users.ToList();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditUsers((sender as Button).DataContext as library_users));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditUsers());
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var RemovingUser = gridUsersList.SelectedItems.Cast<library_users>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {RemovingUser.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Util.db.library_users.RemoveRange(RemovingUser);
                    Util.db.SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    gridUsersList.ItemsSource = Util.db.library_users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }


        }

        private void gridUsersList_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Util.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());


                gridUsersList.ItemsSource = Util.db.library_users.ToList();
            }
        }
    }
}
