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

namespace personal_accounting
{
    /// <summary>
    /// Логика взаимодействия для DepartamentPageAdmin.xaml
    /// </summary>
    public partial class DepartamentPageAdmin : Page
    {
        public DepartamentPageAdmin()
        {
            InitializeComponent();
            DGridDepartament.ItemsSource = personal_accounting_DBEntities.GetContext().Departaments.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDepartamentPage(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDepartamentPage((sender as Button).DataContext as Departaments));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var depForRemoving = DGridDepartament.SelectedItems.Cast<Departaments>().ToList();
            string message = $"Вы точно хотите удалить данные в количестве {depForRemoving.Count} шт?";
            string caption = "Внимание";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    personal_accounting_DBEntities.GetContext().Departaments.RemoveRange(depForRemoving);
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridDepartament.ItemsSource = personal_accounting_DBEntities.GetContext().Departaments.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_is_visibled(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DGridDepartament.ItemsSource = personal_accounting_DBEntities.GetContext().Departaments.ToList();
            }
        }
    }
}
