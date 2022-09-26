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
    /// Логика взаимодействия для VacancyPageAdmin.xaml
    /// </summary>
    public partial class VacancyPageAdmin : Page
    {
        public VacancyPageAdmin()
        {
            InitializeComponent();
            DGridVacancy.ItemsSource = personal_accounting_DBEntities.GetContext().Vacancies.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddVacancyPage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var VForRemoving = DGridVacancy.SelectedItems.Cast<Vacancies>().ToList();
            string message = $"Вы точно хотите удалить данные в количестве {VForRemoving.Count} шт?";
            string caption = "Внимание";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    personal_accounting_DBEntities.GetContext().Vacancies.RemoveRange(VForRemoving);
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridVacancy.ItemsSource = personal_accounting_DBEntities.GetContext().Vacancies.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddVacancyPage((sender as Button).DataContext as Vacancies));
        }

        private void Page_is_visibled(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DGridVacancy.ItemsSource = personal_accounting_DBEntities.GetContext().Vacancies.ToList();
            }
        }
    }
}
