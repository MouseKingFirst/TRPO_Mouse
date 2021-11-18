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

        private void LastNameChanged(object sender, TextChangedEventArgs e)
        {
            txtHintLastName.Visibility = Visibility.Visible;
            LastNameBox.Background = Brushes.Transparent;

            if (LastNameBox.Text.Length > 0)
            {
                txtHintLastName.Visibility = Visibility.Hidden;
                LastNameBox.Background = Brushes.White;
            }
        }

        private void NameChanged(object sender, TextChangedEventArgs e)
        {
            txtHintName.Visibility = Visibility.Visible;
            NameBox.Background = Brushes.Transparent;

            if (NameBox.Text.Length > 0)
            {
                txtHintName.Visibility = Visibility.Hidden;
                NameBox.Background = Brushes.White;
            }
        }

        private void MiddleNameChanged(object sender, TextChangedEventArgs e)
        {
            txtHintMiddleName.Visibility = Visibility.Visible;
            MiddleNameBox.Background = Brushes.Transparent;

            if (MiddleNameBox.Text.Length > 0)
            {
                txtHintMiddleName.Visibility = Visibility.Hidden;
                MiddleNameBox.Background = Brushes.White;
            }
        }

        private void BirthDateChanged(object sender, TextChangedEventArgs e)
        {
            txtHintBirthDate.Visibility = Visibility.Visible;
            BirthDateBox.Background = Brushes.Transparent;

            if (BirthDateBox.Text.Length > 0)
            {
                txtHintBirthDate.Visibility = Visibility.Hidden;
                BirthDateBox.Background = Brushes.White;
            }
        }

        private void PhoneNumChanged(object sender, TextChangedEventArgs e)
        {
            txtHintPhoneNumber.Visibility = Visibility.Visible;
            PhoneNumBox.Background = Brushes.Transparent;

            if (PhoneNumBox.Text.Length > 0)
            {
                txtHintPhoneNumber.Visibility = Visibility.Hidden;
                PhoneNumBox.Background = Brushes.White;
            }
        }

        private void CityChanged(object sender, TextChangedEventArgs e)
        {
            txtHintCity.Visibility = Visibility.Visible;
            CityBox.Background = Brushes.Transparent;

            if (CityBox.Text.Length > 0)
            {
                txtHintCity.Visibility = Visibility.Hidden;
                CityBox.Background = Brushes.White;
            }
        }

        private void StreetChanged(object sender, TextChangedEventArgs e)
        {
            txtHintStreet.Visibility = Visibility.Visible;
            StreetBox.Background = Brushes.Transparent;

            if (StreetBox.Text.Length > 0)
            {
                txtHintStreet.Visibility = Visibility.Hidden;
                StreetBox.Background = Brushes.White;
            }
        }

        private void HouseNumChanged(object sender, TextChangedEventArgs e)
        {
            txtHintHouseNum.Visibility = Visibility.Visible;
            HouseNumBox.Background = Brushes.Transparent;

            if (HouseNumBox.Text.Length > 0)
            {
                txtHintHouseNum.Visibility = Visibility.Hidden;
                HouseNumBox.Background = Brushes.White;
            }
        }

        private void ApNumChanged(object sender, TextChangedEventArgs e)
        {
            txtHintApNum.Visibility = Visibility.Visible;
            ApNumBox.Background = Brushes.Transparent;

            if (ApNumBox.Text.Length > 0)
            {
                txtHintApNum.Visibility = Visibility.Hidden;
                ApNumBox.Background = Brushes.White;
            }
        }

        private void PassSerChanged(object sender, TextChangedEventArgs e)
        {
            txtHintPassSer.Visibility = Visibility.Visible;
            PassSerBox.Background = Brushes.Transparent;

            if (PassSerBox.Text.Length > 0)
            {
                txtHintPassSer.Visibility = Visibility.Hidden;
                PassSerBox.Background = Brushes.White;
            }
        }

        private void PassNumChanged(object sender, TextChangedEventArgs e)
        {
            txtHintPassNum.Visibility = Visibility.Visible;
            PassNumBox.Background = Brushes.Transparent;

            if (PassNumBox.Text.Length > 0)
            {
                txtHintPassNum.Visibility = Visibility.Hidden;
                PassNumBox.Background = Brushes.White;
            }
        }

    }
}
