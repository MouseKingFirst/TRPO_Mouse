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
    }
}
