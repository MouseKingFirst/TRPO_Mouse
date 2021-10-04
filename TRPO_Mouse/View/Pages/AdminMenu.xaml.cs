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

namespace TRPO_Mouse.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Page
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegPage());
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AuthPage());
        }
        private void Button_ViewUsers(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ViewUsers());
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CalcPage());
        }

        private void Button_ViewReaders(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ViewReaders());
        }
        private void Button_ViewBooks(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ViewBooks());
        }
        private void Button_ViewAuthors(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ViewAuthors());
        }
    }
}
