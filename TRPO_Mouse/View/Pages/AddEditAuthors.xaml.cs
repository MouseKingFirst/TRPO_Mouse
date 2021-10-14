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
    /// Логика взаимодействия для AddEditAuthors.xaml
    /// </summary>
    public partial class AddEditAuthors : Page
    {
        private library_authors ca = new library_authors();
        public AddEditAuthors(library_authors sa = null)
        {
            InitializeComponent();
            if(sa != null)
            {
                ca = sa;
            }
            DataContext = ca;
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ca.author_FullName))
            {
                MessageBox.Show("ФИО автора");
                return;
            }
            if (ca.author_code == 0)
            {

                Util.db.library_authors.Add(ca);
            }

            try
            {
                Util.db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService?.Navigate(new ViewAuthors());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
