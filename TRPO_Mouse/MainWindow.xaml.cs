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
            Import();

        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {

            Export();
        }

        private void Export()
        {


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";


            using (var db = new Entities())
            {

                string saveText;

                if (saveFileDialog1.ShowDialog() == true)
                {


                    string filename = saveFileDialog1.FileName;

                    MessageBoxResult result = MessageBox.Show(
                                            "Очистить файл?",
                                            "Сообщение",
                                            MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        File.WriteAllText(filename, "");
                    }


                    foreach (var x in db.library_users)
                    {
                        saveText = String.Join(":", x.id, x.login, x.password, x.email, x.role, x.library_users_data.last_name, x.library_users_data.first_name, x.library_users_data.middle_name);
                        File.AppendAllText(filename, saveText + '\n');
                    }
                    Process.Start("notepad.exe", saveFileDialog1.FileName);
                }
            }
        }

        private void Import()
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
                        data = line.Split(':'); // Разделение по заданию, можно задать любое в зависимости от формата файла
                                                // Можно поставить ':', но тогда файл должен иметь вид:
                                                // 2:admin:83455DGDhdg4:3434@mail.ru:admin:Pavel PP
                                                // и так каждая строка
                        try
                        {
                            MessageBox.Show("Данные в файле: \nID: " + data[0] + "\nЛогин: " + data[1] + "\nПароль(хэшированный): " + data[2] + "\nE-mail: " + data[3] + "\nРоль: " + data[4] + "\nФИО: " + data[5] + ' ' + data[6] + ' ' + data[7]);
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка при импорте данных,\nпроверьте формат txt файла");
                            break;
                        }

                    }

                }
            }
            else
            {
                MessageBox.Show("Файл для импорта не выбран!");
            }

        }

        private void HelpBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] exeBytes = Properties.Resources.help;
                
                
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "help.chm");
                File.WriteAllBytes(path, TRPO_Mouse.Properties.Resources.help);
                Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Ошибка! Не удалось открыть справку!\nВозможно файл справки отсутствует!");
            }
        }
    }
}
