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
    /// Логика взаимодействия для AddEditReader.xaml
    /// </summary>

    public partial class AddEditReader : Page
    {
        private library_users_data cur_user = new library_users_data();
        StringBuilder errors = new StringBuilder();
        public AddEditReader(library_users_data cu = null)
        {
            InitializeComponent();
            if(cu !=null)
            {
                cur_user = cu;
            }
            DataContext = cur_user;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            errors.Clear();
            if (string.IsNullOrWhiteSpace(cur_user.last_name))
                errors.AppendLine("Фамилию!");
            if (string.IsNullOrWhiteSpace(cur_user.first_name))
                errors.AppendLine("Имя!");
            if (string.IsNullOrWhiteSpace(cur_user.phone_number))
                errors.AppendLine("Телефон!!!!");
            if (string.IsNullOrWhiteSpace(cur_user.city))
                errors.AppendLine("Город!!!!");
            if (string.IsNullOrWhiteSpace(cur_user.street))
                errors.AppendLine("Улица!!!!");
            if (string.IsNullOrWhiteSpace(cur_user.passport_series))
                errors.AppendLine("Серия паспорта!!!!");
            if (string.IsNullOrWhiteSpace(cur_user.passport_number))
                errors.AppendLine("Номер паспорта!!!!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (cur_user.user_id == 0)
            {

                Util.db.library_users_data.Add(cur_user);
            }

            try
            {
                Util.db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService?.Navigate(new ViewReaders());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

    }
}
