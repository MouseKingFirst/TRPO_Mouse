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


using TRPO_Mouse.View.Pages;

using System.IO; //для осуществления чтения/записи в файл
using System.Diagnostics; //для запуска Блокнота
using Microsoft.Win32; //для работы диалоговых окон открытия / сохранения файла

using TRPO_Mouse.Model;

namespace TRPO_Mouse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = $"Урок - {page.Title}";

            if (page is AuthPage)
            {
                backBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                backBtn.Visibility = Visibility.Visible;
            }
            if (page is CalcPage)
            {
                // определяем путь к файлу ресурсов
                var uri = new Uri("View/DictionaryCalc.xaml", UriKind.Relative);
                // загружаем словарь ресурсов
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.Clear();
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);

                //Кнопка перехода на калькулятор
                CalcBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                var uri = new Uri("View/Dictionary.xaml", UriKind.Relative);
                // загружаем словарь ресурсов
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.Clear();
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);

                //Кнопка перехода на калькулятор
                CalcBtn.Visibility = Visibility.Visible;
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.CanGoBack)
            {
                mainFrame.GoBack();
            }
        }

        private void CalcBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new CalcPage());
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            //D:\Programming\VSProjects\TRPO_Mouse\TRPO_Mouse\bin\Debug
            Import(true);

        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show(
                "Вывести в файл согласно заданию?",
                "Сообщение",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Export(true);
            }
            else
            {
                Export(false);
            }

        }

        private void Export(bool type)
        {

            string path = "export.txt";
            StreamWriter sw = new StreamWriter(path);
            using (var db = new Entities())
            {

                if (!type)
                {
                    //чем-то похоже на json
                    var JsonQuery = db.library_users.Select(x => new
                    {
                        x.id,
                        x.login,
                        x.password,
                        x.email,
                        x.role,
                        Fio = x.library_users_data.last_name + " " + x.library_users_data.first_name + " " + x.library_users_data.middle_name
                    }
                    );
                    foreach (var str in JsonQuery)
                    {
                        sw.WriteLine(str);
                    }

                }
                else
                {

                    string IDline = String.Join(":", db.library_users.Select(x => x.id));
                    string login = String.Join(":", db.library_users.Select(x => x.login));
                    string password = String.Join(":", db.library_users.Select(x => x.password));
                    string email = String.Join(":", db.library_users.Select(x => x.email));
                    string role = String.Join(":", db.library_users.Select(x => x.role));
                    //Теперь надо получить ФИО

                    string FullName = String.Join(":", db.library_users.Select(x => x.library_users_data.last_name + " " + x.library_users_data.first_name + " " + x.library_users_data.middle_name));


                    sw.WriteLine(IDline);
                    sw.WriteLine(login);
                    sw.WriteLine(password);
                    sw.WriteLine(email);
                    sw.WriteLine(role);
                    sw.WriteLine(FullName);
                }
            }


            sw.Close();

            Process.Start("notepad.exe", path);
        }

        private void Import(bool type)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != "") // проверка на выбор файла
            {
                StreamReader sr = new StreamReader(ofd.FileName); // открываем файл
                while (!sr.EndOfStream) // перебираем строки, пока они не закончены
                {

                    
                }

            }
            else
            {
                MessageBox.Show("Файл для импорта не выбран!");
            }

        }
    }
}
