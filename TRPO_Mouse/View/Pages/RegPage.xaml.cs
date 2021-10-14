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


using System.Security.Cryptography;
using TRPO_Mouse.Model;

namespace TRPO_Mouse.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {

        StringBuilder errors = new StringBuilder();
        public RegPage()
        {
            InitializeComponent();
            roleList.SelectedIndex = 0; //Стандартный выбор
        }
        

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            //Обнулим все ошибки
            errors.Clear();

            /* Проверки для логина */
            if (loginBox.Text.Length < 6 || loginBox.Text.Length > 16 || string.IsNullOrEmpty(loginBox.Text) || string.IsNullOrWhiteSpace(loginBox.Text))
            {
                errors.AppendLine("Логин должен быть от 6 до 16 символов и без пробелов!");
            }

            for (int i = 0; i < loginBox.Text.Length; i++)
            {
                if (loginBox.Text[i] >= 'А' && loginBox.Text[i] <= 'Я' || loginBox.Text[i] >= 'а' && loginBox.Text[i] <= 'я')
                {
                    errors.AppendLine("Логин должен содержать английскую раскладку!");
                    break; //Нет смысла дальше смотреть
                }
            }


            /* Проверки для пароля */
            if (userPassword.Password.Length < 6 || string.IsNullOrEmpty(userPassword.Password) || string.IsNullOrWhiteSpace(userPassword.Password))
            {
                errors.AppendLine("Пароль должен быть от 6 и более символов и без пробелов!");
            }
            
            if (userPassword.Password != passwordRepeat.Password)
            {
                errors.AppendLine("Пароли должны совпадать!");
            }

            bool isHaveNum = false;
            for (int i = 0; i < userPassword.Password.Length; i++) // перебираем символы
            {
                if (userPassword.Password[i] >= 'А' && userPassword.Password[i] <= 'Я' || userPassword.Password[i] >= 'а' && userPassword.Password[i] <= 'я')
                {
                    errors.AppendLine("Пароль должен содержать английскую раскладку!");
                    break; //Нет смысла дальше смотреть
                }
                
                if (userPassword.Password[i] >= '0' && userPassword.Password[i] <= '9')
                {
                    isHaveNum = true;
                    break;
                }
            }

            if (!isHaveNum)
            {
                errors.AppendLine("Пароль должен содержать хотя бы одну цифру!");
            }                   

            /* Проверка ФИО */
            string[] FIO = FIOBox.Text.Split(new char[] { ' ' }); // Разделили строку по пробелам: Фамилия, Имя, Отчество
            
            //Тут переделать и сделать отдельный ввод для каждого реквизита
            if (FIO.Length < 2 || FIO.Length > 3)
            {
                errors.AppendLine("Введите ФИО (отчество не является обязательным)!");
            }
            // Сразу выкидываем ошибку, иначе на следующем этапе выйдем за пределы массива, а это не хорошо
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (FIO.Length == 2)
            {
                Array.Resize(ref FIO, 3); //Надо изменить размер массива, а то проблема...: !!!!!!!! КОСТЫЛЬ!!!!! ПЕРЕДУМАТЬ ЛОГИКУ!!!!!
                FIO[2] = "";
            }
            
            string roleSelected = ""; //по умолчанию

            //Выбор роли на инглише, ибо в базе всё записывается на инглише
            switch (roleList.Text)
            {
                case "Админ":
                    roleSelected = "Admin";
                    break;
                case "Библиотекарь":
                    roleSelected = "Librarian";
                    break;
                case "Читатель":
                    roleSelected = "Reader";
                    break;
                default:
                    errors.AppendLine("Выбрана некорректная роль!");
                    break;
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //Все проверки пройдены успешно, добавляем в базу пользователя
            using (var db = new Entities())
            {
                try
                {

                    library_users User = new library_users
                    {
                        login = loginBox.Text,
                        password = GetPasswordHash(userPassword.Password), //Тут сделаем магию
                        role = roleSelected,
                        email = "test@local.host" //Сделаем e-mail одинаковый
                    };

                    //Нужно проверить... А то вдруг есть такой логин
                    var user = db.library_users
                                .AsNoTracking()
                                .FirstOrDefault(u => u.login == loginBox.Text);


                    if (user != null)
                    {
                        errors.AppendLine(String.Format("Пользователь с логином {0} уже существует!", loginBox.Text));
                    }

                    //Вывод ошибок, вдруг есть
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }


                    library_users_data UserData = new library_users_data
                    {
                        user_id = User.id,
                        last_name = FIO[0],
                        first_name = FIO[1],
                        middle_name = FIO[2],
                        birth_date = DateTime.UtcNow,
                        phone_number = "12345678910",
                        city = "None",
                        street = "None",
                        house_number = 0,
                        apartment_number = 0,
                        passport_series = "1234",
                        passport_number = "567890"
                    };

                    db.library_users.Add(User);
                    db.library_users_data.Add(UserData);

                    db.SaveChanges();

                    MessageBox.Show("УРА! Пользователь успешно зарегистрирован!");


                    //Теперь перекинем пользователя на страницу входа и введём за него все данные
                    NavigationService?.Navigate(new AuthPage(loginBox.Text, userPassword.Password));

                    //Сотрём всё, а то вдруг что
                    ResetAllText();
                }
                catch
                {
                    //Вдруг что-то сломается, а мы тут же ошибку пишем :)
                    MessageBox.Show("ОШИБКА! Что-то пошло не так на этапе добавления пользователя в БД!");
                    ResetAllText();
                    return;
                }

            }



        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetAllText();

            NavigationService?.Navigate(new AuthPage());
        }

        private void ResetAllText()
        {
            /*
             * Сбрасывает всё введенное в поля
             */
            loginBox.Text = "";
            userPassword.Password = "";
            passwordRepeat.Password = "";
            roleList.SelectedIndex = 0;
            FIOBox.Text = "";
        }

        public static string GetPasswordHash(string pswrd)
        {
            /* Хэш для пароля... */
            byte[] data = Encoding.Default.GetBytes(pswrd);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
