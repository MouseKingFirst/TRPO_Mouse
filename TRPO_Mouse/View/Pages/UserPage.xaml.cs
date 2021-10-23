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

            ListUser.ItemsSource = currentUsers;

        }
    }
}
