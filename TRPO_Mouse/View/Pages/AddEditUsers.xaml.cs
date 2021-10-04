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

            }

            DataContext = cur_user;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
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
            StringBuilder errors = new StringBuilder();
            if (cur_user.login == null)
                errors.AppendLine("LOGIN!");
            if (string.IsNullOrWhiteSpace(cur_user.password))
                errors.AppendLine("PASSWORD!");
            if (string.IsNullOrWhiteSpace(cur_user.email))
                errors.AppendLine("E-MAIL!!!!");
            if (cur_user.role == null)
                errors.AppendLine("ROLE");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (cur_user.id == 0)
            {
                cur_user.password = RegPage.GetPasswordHash(passwordBox.Text);
                Util.db.library_users.Add(cur_user);
            }
            try
            {
                Util.db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }
    }
}
