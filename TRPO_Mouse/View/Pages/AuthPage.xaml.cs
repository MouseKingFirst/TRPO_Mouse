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
using System.Security.Cryptography;

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
        public AuthPage(string login, string pass)
        {
            InitializeComponent();

            if (login != "")
            {
                loginBox.Text = login;
            }
            if (pass != "")
            {
                passwordBox.Password = pass;
            }
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
                try
                {

                    string passTmp = GetPasswordHash(passwordBox.Password);
                    var user = db.library_users
                            .AsNoTracking()
                            .FirstOrDefault(u => u.login == loginBox.Text && u.password == passTmp);

                    if (user == null)
                    {
                        MessageBox.Show("Пользователь с такими данными не найдён!");
                        return;
                    }

                    MessageBox.Show("Пользователь успешно найден!");

                    switch (user.role)
                    {
                        case "Admin":
                            NavigationService?.Navigate(new AdminMenu());
                            break;
                        case "Reader":
                            NavigationService?.Navigate(new UserMenu());
                            break;
                        case "Librarian":
                            NavigationService?.Navigate(new LibrarianMenu());
                            break;
                        default:
                            MessageBox.Show("Ошибка! По каким-то причинам нам не удалось понять какая у вас роль... Пожалуйста, обратитесь к администратору");
                            break;
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения к БД!");
                    return;
                }

            }
        }
        public string GetPasswordHash(string pswrd)
        {
            byte[] data = Encoding.Default.GetBytes(pswrd);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegPage());
        }

        private void loginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintLogin.Visibility = Visibility.Visible;
            loginBox.Background = Brushes.Transparent;
            if (loginBox.Text.Length > 0)
            {
                txtHintLogin.Visibility = Visibility.Hidden;
                loginBox.Background = Brushes.White;
            }
        }
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtHintPass.Visibility = Visibility.Visible;
            passwordBox.Background = Brushes.Transparent;

            if (passwordBox.Password.Length > 0)
            {
                txtHintPass.Visibility = Visibility.Hidden;
                passwordBox.Background = Brushes.White;
            }
        }
    }
}
