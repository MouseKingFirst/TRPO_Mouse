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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(loginBox.Text) || string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }
            using (var db = new Entities())
            {
                var user = db.library_users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.login == loginBox.Text && u.password == passwordBox.Password); // md5
                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найдён!");
                    return;
                }

                MessageBox.Show("Пользователь успешно найден!");

                switch(user.role)
                {
                    case "Admin":
                        NavigationService?.Navigate(new Menu());
                        break;
                    case "librarian":
                        NavigationService?.Navigate(new Menu());
                        break;
                    default:
                        NavigationService?.Navigate(new Menu());
                        break;
                }
            }
        }
    }
}
