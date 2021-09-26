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
using System.Text.RegularExpressions;

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
            MessageBoxResult result = MessageBox.Show(
                "Вывести в файл согласно заданию?",
                "Сообщение",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Import(true);
            }
            else
            {
                Import(false);
            }

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


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            //string path = "export.txt";
            //StreamWriter sw = new StreamWriter(path);


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

                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string filename = saveFileDialog1.FileName;
                        foreach (var str in JsonQuery)
                        {
                            File.AppendAllText(filename, str.ToString() + '\n');


                            //sw.WriteLine(str);
                        }
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
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string filename = saveFileDialog1.FileName;
                        File.WriteAllText(filename, IDline + '\n' + login + '\n' + password + '\n' + email + '\n' + role + '\n' + FullName + '\n');



                        //System.IO.File.WriteAllText(filename, login);
                        //System.IO.File.WriteAllText(filename, password);
                        //System.IO.File.WriteAllText(filename, email);
                        //System.IO.File.WriteAllText(filename, role);
                        //System.IO.File.WriteAllText(filename, FullName);
                    }

                    //sw.WriteLine(IDline);
                    //sw.WriteLine(login);
                    //sw.WriteLine(password);
                    //sw.WriteLine(email);
                    //sw.WriteLine(role);
                    //sw.WriteLine(FullName);
                }
            }


            //sw.Close();

            //Process.Start("notepad.exe", path);
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
                    
                    string line = sr.ReadLine(); // читаем строку  
                    string[] data = line.Split();
                    if (data.Length >= 1) // проверяем на целостность данных  
                    {
                        if (type)
                        {
                            data = line.Split(' ');

                            MessageBox.Show("Данные в файле: \nIDline: " + data[0] + "\nlogin: " + data[1] + "\npassword: " + data[2] + "\nemail: " + data[3] + "\nrole: " + data[4] + "\nFullName: " + data[5] + ' ' + data[6] + ' ' + data[7]);


                        }
                        else
                        {
                            line = line.Replace(" ", "");
                            data = line.Split(',');
                            string[] temp = Regex.Split(data[5], @"(?<!^)(?=[А-Я]|[A-Z])");
                            MessageBox.Show(String.Format("Данные в файле:\n{0}\n{1}\n{2}\n{3}\n{4}\n{5}", data[0], data[1], data[2], data[3], data[4],temp[0] + ' ' +temp[1]+' '+temp[2]+' '+temp[3]));
                        }
                    }

                }


            }
            else
            {
                MessageBox.Show("Файл для импорта не выбран!");
            }

        }

    }
}
