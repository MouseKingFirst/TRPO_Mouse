using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    /// 
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            var currentUsers = Util.db.library_users.ToList();

            //Т.к в базе хранятся только названия фотографий, а не полный путь
            //то необходимо указать путь до ресурса
            foreach (var user in currentUsers)
            {
                if (user.photo == null)
                    continue;
                user.photo = "pack://application:,,,/Resources/" + user.photo;
            }


            //Для фильтров
            SortType.SelectedIndex = 0;

            ListUser.ItemsSource = currentUsers.OrderBy(x => (x.library_users_data.last_name + " " + x.library_users_data.first_name + " " + x.library_users_data.middle_name)).ToList();
            
            RoleNotSelected.IsChecked = true;
            RBReaders.IsChecked = false;
            RBLibrarians.IsChecked = false;
            RBAdmins.IsChecked = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchText.Text = "";
            SortType.SelectedIndex = 0;
            RBReaders.IsChecked = false;
            RBLibrarians.IsChecked = false;
            RBAdmins.IsChecked = false;
            UpdateUsers();
        }
        private void UpdateUsers()
        {
            //загружаем всех пользователей в список
            var currentUsers = Util.db.library_users.ToList();
            //осуществляем поиск по Ф.И.О. без учета регистра букв

            currentUsers = currentUsers.Where(x => (x.library_users_data.last_name + " " + x.library_users_data.first_name + " " + x.library_users_data.middle_name).ToLower().Contains(SearchText.Text.ToLower())).ToList();

            //обрабатываем фильтр по одной роли пользователей
            
            if (!RoleNotSelected.IsChecked.Value)
            {
                if (RBReaders.IsChecked.Value)
                    currentUsers = currentUsers.Where(x =>
                    x.role.Contains("Reader")).ToList();

                if (RBLibrarians.IsChecked.Value)
                    currentUsers = currentUsers.Where(x =>
                    x.role.Contains("Librarian")).ToList();

                if (RBAdmins.IsChecked.Value)
                    currentUsers = currentUsers.Where(x =>
                    x.role.Contains("Admin")).ToList();
            }

            //осуществляем сортировку в зависимости от выбора пользователя
            if (SortType.SelectedIndex == 0)
                ListUser.ItemsSource = currentUsers.OrderBy(x => (x.library_users_data.last_name + " " + x.library_users_data.first_name + " " + x.library_users_data.middle_name)).ToList();
            else ListUser.ItemsSource = currentUsers.OrderByDescending(x => (x.library_users_data.last_name + " " + x.library_users_data.first_name + " " + x.library_users_data.middle_name)).ToList();

        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }
        private void RB_Checked(object sender, RoutedEventArgs e)
        {
            UpdateUsers();
        }

        private void SortType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }
    }
}
