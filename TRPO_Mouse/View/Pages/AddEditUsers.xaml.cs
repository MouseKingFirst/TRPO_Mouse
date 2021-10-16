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
    /// Логика взаимодействия для AddEditUsers.xaml
    /// </summary>
    public partial class AddEditUsers : Page
    {
        private library_users cur_user = new library_users();
        private string passwordTemp = "";
        StringBuilder errors = new StringBuilder();
        public AddEditUsers(library_users us = null)
        {
            InitializeComponent();
            if (us != null)
            {
                cur_user = us;
                if (us.role.ToString() == "Reader")
                {
                    roleList.SelectedIndex = 0;
                }
                else if (us.role.ToString() == "Librarian")
                {
                    roleList.SelectedIndex = 1;
                }
                else if (us.role.ToString() == "Admin")
                {
                    roleList.SelectedIndex = 2;
                }
                passwordTemp = us.password;
                MessageBox.Show("Если не собираетись менять пароль, то оставьте поля: Пароль не тронутым");
            }
            else
            {
                roleList.SelectedIndex = 0;
            }
            DataContext = cur_user;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            errors.Clear();
            switch (roleList.Text)
            {
                case "Администратор":
                    cur_user.role = "Admin";
                    break;
                case "Библиотекарь":
                    cur_user.role = "Librarian";
                    break;
                case "Читатель":
                    cur_user.role = "Reader";
                    break;
                default:
                    break;
            }
           
            if (loginBox.Text.Length < 6 || loginBox.Text.Length > 16 || string.IsNullOrEmpty(loginBox.Text) || string.IsNullOrWhiteSpace(loginBox.Text))
                errors.AppendLine("Логин должен быть от 6 до 16 символов и без пробелов!");
            for (int i = 0; i < loginBox.Text.Length; i++)
            {
                if (loginBox.Text[i] >= 'А' && loginBox.Text[i] <= 'Я' || loginBox.Text[i] >= 'а' && loginBox.Text[i] <= 'я')
                {
                    errors.AppendLine("Логин должен содержать английскую раскладку!");
                    break; //Нет смысла дальше смотреть
                }
            }
            if (passwordBox.Text.Length < 6 || string.IsNullOrEmpty(passwordBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Text))
            {
                errors.AppendLine("Пароль должен быть от 6 и более символов и без пробелов!");
            }
            bool isHaveNum = false;
            for (int i = 0; i < passwordBox.Text.Length; i++) // перебираем символы
            {
                if (passwordBox.Text[i] >= 'А' && passwordBox.Text[i] <= 'Я' || passwordBox.Text[i] >= 'а' && passwordBox.Text[i] <= 'я')
                {
                    errors.AppendLine("Пароль должен содержать английскую раскладку!");
                    break; //Нет смысла дальше смотреть
                }

                if (passwordBox.Text[i] >= '0' && passwordBox.Text[i] <= '9')
                {
                    isHaveNum = true;
                    break;
                }
            }

            if (!isHaveNum)
            {
                errors.AppendLine("Пароль должен содержать хотя бы одну цифру!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (passwordBox.Text != passwordTemp)
            {
                cur_user.password = RegPage.GetPasswordHash(passwordBox.Text);
            }

            if (cur_user.id == 0 && cur_user != null)
            {
                /*
                 * Добавление пользователя
                 * И проверка, существует ли такой пользователь или нет?
                 */


                var user = Util.db.library_users
                                .AsNoTracking()
                                .FirstOrDefault(u => u.login == loginBox.Text);
                //Если пользователь с таким логином существует
                if (user != null)
                {
                    errors.AppendLine(String.Format("Пользователь с логином {0} уже существует!", loginBox.Text));
                    MessageBox.Show(errors.ToString());
                    return;
                }
                Util.db.library_users.Add(cur_user);
            }

            try
            {
                Util.db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService?.Navigate(new ViewUsers());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
